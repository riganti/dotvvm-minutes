using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotvvmMinutes.SimpleShop.Model;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;

namespace DotvvmMinutes.SimpleShop.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {
        public List<ProductDTO> Products => Model.Products.All;
    }
}
