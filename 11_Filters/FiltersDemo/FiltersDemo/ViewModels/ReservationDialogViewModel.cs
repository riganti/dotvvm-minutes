using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.ViewModel;
using FiltersDemo.Models;
using FiltersDemo.Services;
using FiltersDemo.ViewModels.Infrastructure;
using System;
using System.Collections.Generic;

namespace FiltersDemo.ViewModels
{
    public class ReservationDialogViewModel : DialogViewModel, IViewModelWithAlert
    {
        private readonly WarehouseService warehouseService;

        [Bind(Direction.ServerToClient)]
        public AlertType AlertType { get; set; }

        [Bind(Direction.ServerToClient)]
        public string AlertText { get; set; }


        public Guid WarehouseId { get; set; }

        public List<WarehouseItemQuantityModel> Quantities { get; set; }
        
        public ReservationDialogViewModel(WarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;
        }

        public void LoadQuantities(Guid warehouseId)
        {
            WarehouseId = warehouseId;
            Quantities = warehouseService.GetAvailableQuantities(warehouseId);
        }

        public void Reserve()
        {
            warehouseService.ReserveQuantities(WarehouseId, Quantities);
            Close();
        }
    }
}