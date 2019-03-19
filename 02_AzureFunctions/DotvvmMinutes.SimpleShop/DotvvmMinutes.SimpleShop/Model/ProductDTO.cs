using System;

namespace DotvvmMinutes.SimpleShop.Model
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl => $"/products/{Id}.jpg";
    }
}