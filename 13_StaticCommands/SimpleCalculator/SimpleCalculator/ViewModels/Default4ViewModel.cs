﻿using System;
using DotVVM.Framework.ViewModel;

namespace SimpleCalculator.ViewModels
{
    public class Default5ViewModel : MasterPageViewModel
    {
        public string Operation { get; set; }

        public double Value { get; set; }

        public double FirstValue { get; set; }

    }
}
