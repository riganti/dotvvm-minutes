using Microsoft.WindowsAzure.Storage.Table;

namespace DotvvmMinutes.SimpleShop.Reviews
{
    public class Rating : TableEntity
    {

        public int NumberOfRatings { get; set; }

        public int TotalRating { get; set; }

    }
}
