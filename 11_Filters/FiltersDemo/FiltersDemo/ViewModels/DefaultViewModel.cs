using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.ViewModel;
using FiltersDemo.Models;
using FiltersDemo.Services;
using FiltersDemo.ViewModels.Infrastructure;

namespace FiltersDemo.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel, IViewModelWithDialog
    {
        private readonly WarehouseService warehouseService;


        public BusinessPackDataSet<WarehouseModel> Warehouses { get; set; } = new();

        public ReservationDialogViewModel ReservationDialog { get; set; }

        public DefaultViewModel(WarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;

            ReservationDialog = new ReservationDialogViewModel(warehouseService);
        }

        public override Task PreRender()
        {
            if (Warehouses.IsRefreshRequired)
            {
                Warehouses.Items = warehouseService.GetWarehouses();
            }

            return base.PreRender();
        }

        public void StartReservation(Guid warehouseId)
        {
            ReservationDialog.LoadQuantities(warehouseId);
            ReservationDialog.Open();
        }

        public DotvvmViewModelBase TryGetOpenDialogViewModel()
        {
            return ReservationDialog.IsOpen ? ReservationDialog : null;
        }
    }
}
