using System;
using System.Collections.Generic;
using System.Linq;

namespace FiltersDemo.Domain
{
    public class WarehouseItem
    {

        public int Id { get; }

        public string Name { get; }

        public WarehouseItem(int id, string name)
        {
            Id = id;
            Name = name;
        }


        public static readonly List<WarehouseItem> AllItems = new List<WarehouseItem>()
        {
            new WarehouseItem(1, "Wheat bun"),
            new WarehouseItem(2, "100% ground beef"),
            new WarehouseItem(3, "Mayo"),
            new WarehouseItem(4, "Organic tomato"),
            new WarehouseItem(5, "Cheddar"),
            new WarehouseItem(6, "Bacon"),
            new WarehouseItem(7, "Lettuce")
        };

        public static string GetName(int warehouseItemId)
        {
            return AllItems.Single(i => i.Id == warehouseItemId).Name;
        }
    }

}
