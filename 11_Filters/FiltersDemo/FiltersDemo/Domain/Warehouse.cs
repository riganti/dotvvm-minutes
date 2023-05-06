using FiltersDemo.Domain.Exceptions;
using FiltersDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FiltersDemo.Domain
{
    public class Warehouse
    {
        private object locker = new object();


        public Guid Id { get; }
        public string Name { get; }
        public string Location { get; }

        public bool Operational { get; set; }


        public Warehouse(Guid id, string name, string location, bool operational, Dictionary<int, int> itemQuantities)
        {
            Id = id;
            Name = name;
            Location = location;
            Operational = operational;
            this.itemQuantities = itemQuantities;
        }


        private Dictionary<int, int> itemQuantities = new Dictionary<int, int>();

        private int GetQuantityNoLocking(int id)
        {
            return itemQuantities.TryGetValue(id, out var result) ? result : 0;
        }

        public void ReserveQuantities(List<WarehouseItemQuantityModel> reservedQuantities)
        {
            if (!Operational)
            {
                throw new WarehouseNotOperationalException();
            }

            lock (locker)
            {
                // check that we have everything available
                foreach (var reservedQuantity in reservedQuantities)
                {
                    var currentQuantity = GetQuantityNoLocking(reservedQuantity.Id);
                    if (currentQuantity < reservedQuantity.Quantity)
                    {
                        throw new WarehouseInsufficientQuantityException(reservedQuantity.Id, currentQuantity, reservedQuantity.Quantity);
                    }
                }

                // reserve the quantities
                foreach (var reservedQuantity in reservedQuantities)
                {
                    itemQuantities[reservedQuantity.Id] -= reservedQuantity.Quantity;
                }
            }
        }

        public List<WarehouseItemQuantityModel> GetAvailableQuantities()
        {
            if (!Operational)
            {
                throw new WarehouseNotOperationalException();
            }

            lock (locker)
            {
                return itemQuantities
                    .Select(i => new WarehouseItemQuantityModel()
                    {
                        Id = i.Key,
                        Name = WarehouseItem.GetName(i.Key),
                        Quantity = i.Value
                    })
                    .ToList();
            }
        }
    }
}
