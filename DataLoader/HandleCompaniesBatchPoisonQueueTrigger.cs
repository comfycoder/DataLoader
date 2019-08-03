using System;
using DataLoader.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Queue;

namespace DataLoader
{
    public static class HandleCompaniesBatchPoisonQueueTrigger
    {
        [FunctionName("HandleCompaniesBatchPoisonQueueTrigger")]
        [return: Table("ManualInterventionRequired")]
        public static PoisonMessageDetails Run(
            [QueueTrigger("companies-batch-poison", Connection = "AzureWebJobsStorage")]CloudQueueMessage poisonMessage, 
            ILogger log)
        {
            log.LogInformation($"Processing poison message {poisonMessage.Id}");

            return new PoisonMessageDetails
            {
                RowKey = Guid.NewGuid().ToString(),
                PartitionKey = "companies-batch",
                OriginalMessageContent = poisonMessage.AsString,
                OriginalMessageId = poisonMessage.Id
            };
        }
    }
}
