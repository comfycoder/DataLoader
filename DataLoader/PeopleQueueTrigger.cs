using DataLoader.Models;
using DataLoader.Rules;
using DataLoader.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DataLoader
{
    public class PeopleQueueTrigger
    {
        private readonly IPersonRules _personRules;
        private readonly IPersonRepository _personRepository;

        public PeopleQueueTrigger(IPersonRules personRules, IPersonRepository personRepository)
        {
            _personRules = personRules;
            _personRepository = personRepository;
        }

        [FunctionName("PeopleQueueTrigger")]
        public async Task Run(
            [QueueTrigger("people-batch", Connection = "AzureWebJobsStorage")]PeopleMessage queueItem,
            IBinder binder,
            ILogger log)
        {
            // Get the Batch ID from the incoming people message (from Storage Queue)
            var batchId = queueItem.BatchId;

            log.LogInformation($"INFO: PeopleQueueTrigger: Received people message with Batch ID: {batchId}");

            // Iterate over all person records in the incoming people message queue item
            foreach (var person in queueItem.People)
            {
                try
                {
                    // Apply busines rules to validate or enhance data
                    _personRules.ApplyBusinessRules(person);

                    // Add the person record to a data set to be batch processed
                    _personRepository.UpsertPerson(person);
                }
                catch (Exception ex)
                {
                    log.LogError($"ERROR: PeopleQueueTrigger: Unable to upsert person record with Batch ID = {person.PersonId}.", ex);

                    // Rethrow error to ensure message gets moved to poison queue
                    throw;
                }
            }

            try
            {
                // TODO: Remove the following three lines as
                // they are only meant to test the poison queue
                if (queueItem.BatchId.EndsWith("-3"))
                {
                    throw new ApplicationException($"Bad data in Batch ID: {queueItem.BatchId}");
                }

                // Save all person records to the SQL Server database
                await _personRepository.SaveChangesAsync();

                log.LogInformation($"INFO: PeopleQueueTrigger: Successfully saved person records for Batch ID: {batchId}");
            }
            catch (Exception ex)
            {
                log.LogError($"ERROR: PeopleQueueTrigger: Unable to save person records for Batch ID: {batchId}", ex);

                // Rethrow error to ensure message gets moved to poison queue
                throw;
            }
        }
    }
}
