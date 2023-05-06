using FiltersDemo.Models;
using System;
using System.Linq;

namespace FiltersDemo.Domain.Exceptions
{
    public class WarehouseInsufficientQuantityException : DomainException
    {

        public WarehouseInsufficientQuantityException(int warehouseItemId, int availableQuantity, int requestedQuantity)
            : base($"Cannot reserve item '{WarehouseItem.GetName(warehouseItemId)}'. Available quantity {availableQuantity}, requested quantity {requestedQuantity}.")
        {
        }

    }
}
