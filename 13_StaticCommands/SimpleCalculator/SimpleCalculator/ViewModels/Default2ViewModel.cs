using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace SimpleCalculator.ViewModels
{
    public class Default2ViewModel : MasterPageViewModel
    {
        public string Operation { get; set; }

        public double Value { get; set; }

        public double FirstValue { get; set; }


        [AllowStaticCommand]
        public static double CalculateTotal(string operation, double firstValue, double value)
        {
            return operation switch
            {
                "+" => firstValue + value,
                "-" => firstValue - value,
                "*" => firstValue * value,
                "/" => firstValue / value,
                _ => throw new NotSupportedException()
            };
        }

    }
}
