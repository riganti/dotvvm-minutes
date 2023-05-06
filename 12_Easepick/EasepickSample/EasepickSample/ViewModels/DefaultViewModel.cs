using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace EasepickSample.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {
        public DateTime Date { get; set; } = new DateTime(2023, 3, 25);

        public DateTime RangeStart { get; set; } = new DateTime(2023, 3, 20);

        public DateTime RangeEnd { get; set; } = new DateTime(2023, 3, 23);

        public int Changes { get; set; }
    }
}
