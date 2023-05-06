using System;
using DotVVM.Framework.ViewModel;

namespace SimpleCalculator.ViewModels
{
    public class Default3ViewModel : MasterPageViewModel
    {
        public string Operation { get; set; }

        public double Value { get; set; }

        public double FirstValue { get; set; }


        [AllowStaticCommand]
        public double CalculateTotal()
        {
            return Operation switch
            {
                "+" => FirstValue + Value,
                "-" => FirstValue - Value,
                "*" => FirstValue * Value,
                "/" => FirstValue / Value,
                _ => throw new NotSupportedException()
            };
        }

    }
}
