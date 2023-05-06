using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ImageResizerDemo.Services.BackgroundOperations;

public class SignalRBackgroundOperationFactory : IBackgroundOperationFactory
{
    private readonly IHubContext<SignalRBackgroundOperationsHub> hubContext;

    public SignalRBackgroundOperationFactory(IHubContext<SignalRBackgroundOperationsHub> hubContext)
    {
        this.hubContext = hubContext;
    }

    public BackgroundOperationProgress Start(Func<IBackgroundOperationContext, Task> action)
    {
        var operationContext = new SignalRBackgroundOperationContext(Guid.NewGuid(), hubContext);

        Task.Factory.StartNew(async () =>
        {
            try
            {
                await action(operationContext);
            }
            catch (Exception ex)
            {
                await operationContext.ReportError("Unknown error occurred when processing the operation.");
            }
        });

        return new BackgroundOperationProgress()
        {
            OperationId = operationContext.OperationId
        };
    }
}