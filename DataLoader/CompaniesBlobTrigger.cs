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
    public static class CompaniesBlobTrigger
    {
        [FunctionName("CompaniesBlobTrigger")]
        public static void Run(
            [BlobTrigger("companies-upload/{name}", Connection = "AzureWebJobsStorage")]Stream blob,
            string name,
            ILogger log,
            [Queue("companies-batch", Connection = "AzureWebJobsStorage")] ICollector<CompaniesMessage> queueCollector)
        {
            log.LogInformation($"INFO: CompaniesBlobTrigger: Received uploaded blob\n Name:{name} \n Size: {blob.Length} Bytes");

            var startTime = DateTime.UtcNow;

            log.LogInformation($"INFO: CompaniesBlobTrigger: Started processing {name} at {DateTime.UtcNow.ToString("MM-dd-yyy HH:mm:ss")}");

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

                IEnumerable<Company> companies = null;

                try
                {
                    csvReader.Configuration.RegisterClassMap<CompanyMap>();

                    companies = csvReader.GetRecords<Company>();
                }
                catch (Exception ex)
                {
                    log.LogError("ERROR: CompaniesBlobTrigger: Unable to read input blob", ex);

                    return;
                }

                int count = 0; // Current counter increment
                int max = 25; // Maximum number of person records to process in a batch
                int batch = 0; // Current batch increment number

                var companiesMessage = new CompaniesMessage();

                foreach (var company in companies)
                {
                    count++;

                    // Add person to PeopleMessage
                    companiesMessage.Companies.Add(company);

                    if (count >= max)
                    {
                        count = 0;

                        // Add the current (fully populated) companies message to a Storage Queue
                        batch = QueueBatch(name, queueCollector, batch, companiesMessage, log);
                    }
                }

                if (count > 0)
                {
                    count = 0;

                    // Add the final (partially populated) companies message to a Storage Queue
                    batch = QueueBatch(name, queueCollector, batch, companiesMessage, log);
                }
            }

            log.LogInformation($"INFO: CompaniesBlobTrigger: Finished processing {name} at {DateTime.UtcNow.ToString("MM-dd-yyy HH:mm:ss")}");
        }

        private static int QueueBatch(string name, ICollector<CompaniesMessage> queueCollector, int batch, CompaniesMessage companiesMessage, ILogger log)
        {
            batch++;

            // Create Batch ID based on blob filename plus batch increment number
            var batchId = $"{name}-{batch}";

            try
            {
                // Set companies message Batch ID
                companiesMessage.BatchId = batchId;

                // Add companies message to Storage Queue
                queueCollector.Add(companiesMessage);

                // Reset message properties
                companiesMessage.BatchId = string.Empty;
                companiesMessage.Companies.Clear();
            }
            catch (Exception ex)
            {
                log.LogError($"ERROR: CompaniesBlobTrigger: Unable to queue Batch ID: {batchId}", ex);

                throw;
            }

            return batch;
        }
    }
}
