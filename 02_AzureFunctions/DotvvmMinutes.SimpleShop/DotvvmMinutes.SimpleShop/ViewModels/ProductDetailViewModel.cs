using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotvvmMinutes.SimpleShop.Model;
using DotVVM.Framework.ViewModel;

namespace DotvvmMinutes.SimpleShop.ViewModels
{
    public class ProductDetailViewModel : MasterPageViewModel
    {

        [FromRoute("Id")]
        public int ProductId { get; set; }

        public ProductDTO Product => Model.Products.GetById(ProductId);

        public List<string> Colors => new List<string>() { "White", "Brown", "Black", "Gray" };

        public decimal Quantity { get; set; } = 1;

        public string Color { get; set; }

        public void BuyNow()
        {
            throw new NotImplementedException();
        }
    }
}

