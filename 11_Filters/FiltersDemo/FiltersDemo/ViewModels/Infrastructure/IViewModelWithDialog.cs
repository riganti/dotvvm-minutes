using DotVVM.Framework.ViewModel;

namespace FiltersDemo.ViewModels.Infrastructure
{
    public interface IViewModelWithDialog
    {

        DotvvmViewModelBase TryGetOpenDialogViewModel();

    }
}
