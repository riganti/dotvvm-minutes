using System;
using DotVVM.Framework.ViewModel;

namespace SimpleCalculator.ViewModels
{
    public class Default6ViewModel : MasterPageViewModel
    {
        public string Operation { get; set; }

        public double Value { get; set; }

        public double FirstValue { get; set; }


        public void CalculateTotal()
        {
            Value = Operation switch
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
