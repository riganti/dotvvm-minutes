using System.ComponentModel.DataAnnotations;

namespace FiltersDemo.Models
{
    public class WarehouseItemQuantityModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        [Range(1, 999, ErrorMessage = "The quantity must be between 1 and 999!")]
        public int Quantity { get; set; }

    }
}
