using FiltersDemo.Domain;
using FiltersDemo.Domain.Exceptions;
using FiltersDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FiltersDemo.Services
{
    public class WarehouseService
    {
        public List<WarehouseModel> GetWarehouses()
        {
            return warehouses
                .Select(w => new WarehouseModel()
                {
                    Id = w.Id,
                    Name = w.Name,
                    Location = w.Location
                })
                .ToList();
        }

        public List<WarehouseItemQuantityModel> GetAvailableQuantities(Guid warehouseId)
        {
            return warehouses
                .Single(w => w.Id == warehouseId)
                .GetAvailableQuantities();
        }

        public void ReserveQuantities(Guid warehouseId, List<WarehouseItemQuantityModel> quantities)
        {
            if (quantities.Count != quantities.DistinctBy(q => q.Id).Count())
            {
                throw new DomainException("Some item was requested twice!");                
            }

            warehouses
                .Single(w => w.Id == warehouseId)
                .ReserveQuantities(quantities);
        }


        private static readonly List<Warehouse> warehouses;
        private static readonly Random random = new();
        
        static WarehouseService()
        {
            warehouses = new List<Warehouse>()
            {
                new Warehouse(Guid.NewGuid(), "CZ West", "Pilsen", true, GetRandomItems()),
                new Warehouse(Guid.NewGuid(), "CZ East", "Ostrava", true, GetRandomItems()),
                new Warehouse(Guid.NewGuid(), "DE South", "Munich", false, GetRandomItems()),
            };
        }

        private static Dictionary<int, int> GetRandomItems()
        {
            return WarehouseItem.AllItems.ToDictionary(i => i.Id, i => random.Next(500));
        }

    }
}
