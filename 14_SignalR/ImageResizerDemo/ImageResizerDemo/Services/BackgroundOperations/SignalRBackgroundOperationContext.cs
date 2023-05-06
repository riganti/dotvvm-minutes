using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ImageResizerDemo.Services.BackgroundOperations;

public class SignalRBackgroundOperationContext : IBackgroundOperationContext
{
    private readonly IHubContext<SignalRBackgroundOperationsHub> hubContext;

    public Guid OperationId { get; }

    public SignalRBackgroundOperationContext(Guid operationId, IHubContext<SignalRBackgroundOperationsHub> hubContext)
    {
        this.hubContext = hubContext;
        OperationId = operationId;
    }

    public async Task ReportProgress(int itemsProcessed, int itemsTotal, string description)
    {
        if (itemsTotal < 0)
        {
            throw new ArgumentOutOfRangeException($"The argument {nameof(itemsTotal)} must be greater or equal to zero!");
        }
        if (itemsProcessed > itemsTotal)
        {
            throw new ArgumentOutOfRangeException($"The argument {nameof(itemsProcessed)} must be greater or equal to {nameof(itemsTotal)}!");
        }

        var progress = new BackgroundOperationProgress()
        {
            OperationId = OperationId,
            Percent = itemsTotal == 0 ? 0 : (double)itemsProcessed / itemsTotal,
            IsCompleted = itemsProcessed == itemsTotal,
            IsError = false,
            Description = description
        };
        await ReportProgressCore(progress);
    }

    public async Task ReportError(string description)
    {
        var progress = new BackgroundOperationProgress()
        {
            OperationId = OperationId,
            Percent = 1,
            IsCompleted = true,
            IsError = true,
            Description = description
        };
        await ReportProgressCore(progress);
    }

    public async Task ReportSuccess(string description)
    {
        var progress = new BackgroundOperationProgress()
        {
            OperationId = OperationId,
            Percent = 1,
            IsCompleted = true,
            IsError = false,
            Description = description
        };
        await ReportProgressCore(progress);
    }

    private async Task ReportProgressCore(BackgroundOperationProgress progress)
    {
        await hubContext.Clients.Group(OperationId.ToString().ToLower())
            .SendAsync("reportProgress", new object[] { progress });
    }
}