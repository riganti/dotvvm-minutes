using DotVVM.BusinessPack.Controls;

namespace FiltersDemo.ViewModels.Infrastructure
{
    public interface IViewModelWithAlert
    {

        AlertType AlertType { get; set; }

        string AlertText { get; set; }
    }
}
