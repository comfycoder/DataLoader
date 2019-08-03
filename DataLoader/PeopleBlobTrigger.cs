using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using DataLoader.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace DataLoader
{
    public static class PeopleBlobTrigger
    {
        [FunctionName("PeopleBlobTrigger")]
        public static void Run(
            [BlobTrigger("people-upload/{name}", Connection = "AzureWebJobsStorage")]Stream blob,
            string name,
            ILogger log,
            [Queue("people-batch", Connection = "AzureWebJobsStorage")] ICollector<PeopleMessage> queueCollector)
        {
            log.LogInformation($"INFO: PeopleBlobTrigger: Received uploaded blob\n Name:{name} \n Size: {blob.Length} Bytes");

            var startTime = DateTime.UtcNow;

            log.LogInformation($"INFO: PeopleBlobTrigger: Started processing {name} at {DateTime.UtcNow.ToString("MM-dd-yyy HH:mm:ss")}");

            using (var sr = new StreamReader(blob))
            using (var csvReader = new CsvReader(sr))
            {
                // Configure CsvReader options
                csvReader.Configuration.TrimOptions = TrimOptions.Trim; // Trim all whitespaces from fields
                csvReader.Configuration.Comment = '@';                  // Set comment start character. Default is '#'
                csvReader.Configuration.AllowComments = true;           // Allow comments in file
                csvReader.Configuration.IgnoreBlankLines = true;        // Ignore blank lines in file
                csvReader.Configuration.Delimiter = ",";                // Set the field delimiter character
                csvReader.Configuration.HasHeaderRecord = true;         // Ensure a header row exists

                IEnumerable<Person> people = null;

                try
                {
                    people = csvReader.GetRecords<Person>();
                }
                catch (Exception ex)
                {
                    log.LogError("ERROR: PeopleBlobTrigger: Unable to read input blob", ex);

                    return;
                }

                int count = 0; // Current counter increment
                int max = 100; // Maximum number of person records to process in a batch
                int batch = 0; // Current batch increment number

                var peopleMessage = new PeopleMessage();

                foreach (var person in people)
                {
                    count++;

                    // Add person to PeopleMessage
                    peopleMessage.People.Add(person);

                    if (count >= max)
                    {
                        count = 0;

                        // Add the current (fully populated) people message to a Storage Queue
                        batch = QueueBatch(name, queueCollector, batch, peopleMessage, log);
                    }
                }

                if (count > 0)
                {
                    count = 0;

                    // Add the final (partially populated) people message to a Storage Queue
                    batch = QueueBatch(name, queueCollector, batch, peopleMessage, log);
                }
            }

            log.LogInformation($"INFO: PeopleBlobTrigger: Finished processing {name} at {DateTime.UtcNow.ToString("MM-dd-yyy HH:mm:ss")}");
        }

        private static int QueueBatch(string name, ICollector<PeopleMessage> queueCollector, int batch, PeopleMessage peopleMessage, ILogger log)
        {
            batch++;

            // Create Batch ID based on blob filename plus batch increment number
            var batchId = $"{name}-{batch}";

            try
            {
                // Set people message Batch ID
                peopleMessage.BatchId = batchId;

                // Add people message to Storage Queue
                queueCollector.Add(peopleMessage);

                // Reset message properties
                peopleMessage.BatchId = string.Empty;
                peopleMessage.People.Clear();
            }
            catch (Exception ex)
            {
                log.LogError($"ERROR: PeopleBlobTrigger: Unable to queue Batch ID: {batchId}", ex);

                throw;
            }

            return batch;
        }
    }
}
