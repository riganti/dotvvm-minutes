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

        public OrderDTO Order { get; set; }

        public CartDTO Cart { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<string> Countries { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<string> States { get; set; }

        public override Task Init()
        {
            if (!Context.IsPostBack)
            {
                Cart = new CartDTO()
                {
                    CartItems = new List<CartItemDTO>()
                    {
                        new CartItemDTO()
                        {
                            Name = "DotVVM for Visual Studio",
                            Description = "Best experience for developers",
                            Price = 149
                        },
                        new CartItemDTO()
                        {
                            Name = "DotVVM Business Pack",
                            Description = "25+ enteprise controls for LOB apps",
                            Price = 199
                        },
                        new CartItemDTO()
                        {
                            Name = "Bootstrap for DotVVM",
                            Description = "Clean and short syntax for Bootstrap controls",
                            Price = 49
                        },
                        new CartItemDTO()
                        {
                            Name = "Special discount",
                            Description = "DOTVVM-MINUTES",
                            Price = -35
                        }
                    },
                    Promo = new CartPromoDTO()
                };

                Order = new OrderDTO()
                {
                    BillingAddress = new AddressDTO(),
                    Preferences = new PreferencesDTO()
                };

                Countries = new List<string>()
                {
                    "United States"
                };
                States = new List<string>()
                {
                    "California"
                };
            }

            return base.Init();
        }

        public void Checkout()
        {
            throw new NotImplementedException();
        }
    }
}
