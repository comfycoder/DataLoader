using DataLoader.Models;
using DataLoader.Rules;
using DataLoader.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DataLoader
{
    public class CompanyQueueTrigger
    {
        private readonly ICompanyRules _companyRules;
        private readonly ICompanyRepository _companyRepository;

        public CompanyQueueTrigger(ICompanyRules companyRules, ICompanyRepository companyRepository)
        {
            _companyRules = companyRules;
            _companyRepository = companyRepository;
        }

        [FunctionName("CompanyQueueTrigger")]
        public async Task Run(
            [QueueTrigger("companies-batch", Connection = "AzureWebJobsStorage")]CompaniesMessage queueItem,
            IBinder binder,
            ILogger log)
        {
            // Get the Batch ID from the incoming people message (from Storage Queue)
            var batchId = queueItem.BatchId;

            log.LogInformation($"INFO: CompanyQueueTrigger: Received people message with Batch ID: {batchId}");

            // Iterate over all person records in the incoming people message queue item
            foreach (var company in queueItem.Companies)
            {
                try
                {
                    // Apply busines rules to validate or enhance data
                    _companyRules.ApplyBusinessRules(company);

                    // Add the person record to a data set to be batch processed
                    _companyRepository.UpsertCompany(company);
                }
                catch (Exception ex)
                {
                    log.LogError($"ERROR: CompanyQueueTrigger: Unable to upsert person record with Batch ID = {company.CompanyId}.", ex);

                    // Rethrow error to ensure message gets moved to poison queue
                    throw;
                }
            }

            try
            {
                // Save all person records to the SQL Server database
                await _companyRepository.SaveChangesAsync();

                log.LogInformation($"INFO: CompanyQueueTrigger: Successfully saved person records for Batch ID: {batchId}");
            }
            catch (Exception ex)
            {
                log.LogError($"ERROR: CompanyQueueTrigger: Unable to save person records for Batch ID: {batchId}", ex);

                // Rethrow error to ensure message gets moved to poison queue
                throw;
            }
        }
    }
}
