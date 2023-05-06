using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.ViewModel;
using FiltersDemo.ViewModels.Infrastructure;

namespace FiltersDemo.ViewModels
{
    public class MasterPageViewModel : DotvvmViewModelBase, IViewModelWithAlert
    {
        [Bind(Direction.ServerToClient)]
        public AlertType AlertType { get; set; }

        [Bind(Direction.ServerToClient)]
        public string AlertText { get; set; }

    }
}
