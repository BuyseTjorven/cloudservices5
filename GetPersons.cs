using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;

namespace MCT.Function
{
    public static class GetPersons
    {
        [FunctionName("GetPersons")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var cosmosClientOptions = new CosmosClientOptions()
            {
                ConnectionMode = ConnectionMode.Gateway,
            };



            log.LogInformation("C# HTTP trigger function processed a request.");
            string connectionString = Environment.GetEnvironmentVariable("CosmosDBConnection");
            CosmosClient client = new CosmosClient(connectionString, cosmosClientOptions);
            Container container = client.GetContainer("firstdatabase", "persons");

            string sql = "SELECT * FROM c";
            var iterator = container.GetItemQueryIterator<Person>(sql);

            List<Person> results = new List<Person>();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                foreach (var item in response)
                {
                    results.Add(item);
                }
            }



            return new OkObjectResult(results);
        }
    }
}
