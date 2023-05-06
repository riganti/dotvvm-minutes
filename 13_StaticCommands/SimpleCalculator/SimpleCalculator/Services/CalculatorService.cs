using DotVVM.Framework.ViewModel;
using System;

namespace SimpleCalculator.Services
{
    public class CalculatorService
    {
        // don't forget to register the service in Startup.cs

        public CalculatorService( /* you can request dependencies here */ )
        {
        }

        [AllowStaticCommand]
        public double CalculateTotal(string operation, double firstValue, double value)
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
