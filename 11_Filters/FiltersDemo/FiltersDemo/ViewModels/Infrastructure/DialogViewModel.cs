using DotVVM.Framework.ViewModel;
using System;

namespace FiltersDemo.ViewModels.Infrastructure
{
    public class DialogViewModel : DotvvmViewModelBase
    {

        public bool IsOpen { get; set; }

        public event Action Closed;

        public void Open()
        {
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
            Closed?.Invoke();
        }

    }
}
