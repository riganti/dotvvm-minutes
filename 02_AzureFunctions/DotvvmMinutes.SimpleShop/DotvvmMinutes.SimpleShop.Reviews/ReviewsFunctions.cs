using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace DotvvmMinutes.SimpleShop.Reviews
{
    public static class ReviewsFunctions
    {
        // provides shared access to the Azure Table
        private static Lazy<Task<CloudTable>> reviewsTable = new Lazy<Task<CloudTable>>(GetOrCreateTable, LazyThreadSafetyMode.ExecutionAndPublication);

        private static async Task<CloudTable> GetOrCreateTable()
        {
            // load configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            // create Azure Table Storage Client
            var storageConnectionString = config["StorageConnectionString"];
            var account = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = account.CreateCloudTableClient();

            // make sure the table exists
            var table = tableClient.GetTableReference("productreviews");
            await table.CreateIfNotExistsAsync();
            return table;
        }



        [FunctionName("GetRatingForProduct")]
        public static async Task<IActionResult> GetRatingForProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "rating/{productId}")]HttpRequest req,
            int productId,
            TraceWriter log
        )
        {
            // get rating of the product
            var rating = await GetRatingForProductCore(productId);

            if (rating == null)
            {
                // no ratings so far
                return new OkObjectResult(0);
            }
            else
            {
                // calculate the average rating
                return new OkObjectResult((decimal)rating.TotalRating / rating.NumberOfRatings);
            }
        }

        [FunctionName("RateProduct")]
        public static async Task<IActionResult> RateProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "rating/{productId}")]HttpRequest req,
            int productId,
            TraceWriter log
        )
        {
            var ratingValue = JsonConvert.DeserializeObject<int>(await req.ReadAsStringAsync());

            // validate
            if (ratingValue < 1 || ratingValue > 5)
            {
                return new BadRequestResult();
            }

            // get rating of the product
            var rating = await GetRatingForProductCore(productId);

            if (rating == null)
            {
                // create a new rating if needed
                rating = new Rating()
                {
                    RowKey = productId.ToString(),
                    PartitionKey = string.Empty
                };
            }
            
            // add the user rating
            rating.NumberOfRatings++;
            rating.TotalRating += ratingValue;

            // save
            var table = await reviewsTable.Value;
            await table.ExecuteAsync(TableOperation.InsertOrReplace(rating));

            // TODO: we should add retry logic to prevent conflicts

            return new OkResult();
        }


        private static async Task<Rating> GetRatingForProductCore(int productId)
        {
            // get table
            var table = await reviewsTable.Value;

            // run query
            var query = new TableQuery<Rating>()
                .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, productId.ToString()));
            var ratingResult = await table.ExecuteQuerySegmentedAsync(query, null);

            // return the result (there will be 0 or 1 rows)
            return ratingResult.Results.FirstOrDefault();
        }
    }
}
