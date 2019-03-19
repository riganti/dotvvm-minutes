using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;

namespace DotvvmMinutes.LoadingAnimation.Controls
{
    public class BillingAddress : DotvvmMarkupControl
    {

        public List<string> Countries
        {
            get { return (List<string>)GetValue(CountriesProperty); }
            set { SetValue(CountriesProperty, value); }
        }
        public static readonly DotvvmProperty CountriesProperty
            = DotvvmProperty.Register<List<string>, BillingAddress>(c => c.Countries, null);


        public List<string> States
        {
            get { return (List<string>)GetValue(StatesProperty); }
            set { SetValue(StatesProperty, value); }
        }
        public static readonly DotvvmProperty StatesProperty
            = DotvvmProperty.Register<List<string>, BillingAddress>(c => c.States, null);


    }
}
