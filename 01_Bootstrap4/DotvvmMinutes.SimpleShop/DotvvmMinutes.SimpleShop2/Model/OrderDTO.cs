using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmMinutes.SimpleShop.Model
{
    public class OrderDTO
    {

        public AddressDTO BillingAddress { get; set; }

        public PreferencesDTO Preferences { get; set; }

    }
}
