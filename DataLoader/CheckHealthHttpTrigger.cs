using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using DataLoader.Services;
using DataLoader.Models;

namespace DataLoader
{
    public class CheckHealthHttpTrigger
    {
        private readonly IConfiguration _config;
        private readonly IKeyVaultService _keyVaultService;
        private readonly DataContext _dbContext;

        public CheckHealthHttpTrigger(IConfiguration config, IKeyVaultService keyVaultService, DataContext dbContext)
        {
            _config = config;
            _keyVaultService = keyVaultService;
            _dbContext = dbContext;
        }

        [FunctionName("CheckHealth")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "checkhealth")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("CheckHealth method called.");

            var keyVaultName = _config["KeyVaultName"];

            if (string.IsNullOrWhiteSpace(keyVaultName))
            {
                return new BadRequestObjectResult("Missing KeyVaultName setting");
            }

            var keyVaultSecret = await _keyVaultService.GetSecretAsync("Secret1");

            if (string.IsNullOrWhiteSpace(keyVaultSecret))
            {
                return new BadRequestObjectResult("Missing Secret1 setting");
            }

            var aisKey = await _keyVaultService.GetSecretAsync("APPINSIGHTS_INSTRUMENTATIONKEY");

            if (string.IsNullOrWhiteSpace(aisKey))
            {
                return new BadRequestObjectResult("Missing APPINSIGHTS_INSTRUMENTATIONKEY setting");
            }

            var canConnect = await _dbContext.Database.CanConnectAsync();

            if (canConnect == false)
            {
                return new BadRequestObjectResult("Cannot connect to database");
            }

            return new OkObjectResult("SUCCESS");
        }
    }
}
