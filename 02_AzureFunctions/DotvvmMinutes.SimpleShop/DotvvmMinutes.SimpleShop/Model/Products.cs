using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmMinutes.SimpleShop.Model
{
    public static class Products
    {

        public static List<ProductDTO> All { get; } = new List<ProductDTO>()
        {
            new ProductDTO() { Id = 10, Name = "Grand Teton Mountains" },
            new ProductDTO() { Id = 11, Name = "Grand Prismatic Spring" },
            new ProductDTO() { Id = 12, Name = "Yellowstone Bisons" }
        };

        public static ProductDTO GetById(int id)
        {
            return All.Single(p => p.Id == id);
        }

    }
}
