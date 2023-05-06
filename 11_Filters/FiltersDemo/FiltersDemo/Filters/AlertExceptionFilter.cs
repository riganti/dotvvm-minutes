using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime.Filters;
using FiltersDemo.Domain.Exceptions;
using FiltersDemo.ViewModels.Infrastructure;
using System;
using System.Threading.Tasks;

namespace FiltersDemo.Filters
{
    public class AlertExceptionFilter : ExceptionFilterAttribute
    {

        protected override Task OnCommandExceptionAsync(IDotvvmRequestContext context, ActionInfo actionInfo, Exception ex)
        {
            // show error message only for domain exceptions
            var message = ex is DomainException domainException
                ? domainException.Message
                : "We are sorry, an unexpected error occurred.";

            // try to find the viewmodel of the active dialog
            var viewModel = context.ViewModel;
            while (viewModel is IViewModelWithDialog viewModelWithDialog
                && viewModelWithDialog.TryGetOpenDialogViewModel() is { } dialogViewModel)
            {
                viewModel = dialogViewModel;
            }

            // if it supports alert, show the alert
            if (viewModel is IViewModelWithAlert viewModelWithAlert)
            {
                viewModelWithAlert.AlertType = DotVVM.BusinessPack.Controls.AlertType.Danger;
                viewModelWithAlert.AlertText = message;
                context.IsCommandExceptionHandled = true;
            }

            return base.OnCommandExceptionAsync(context, actionInfo, ex);
        }

    }
}
