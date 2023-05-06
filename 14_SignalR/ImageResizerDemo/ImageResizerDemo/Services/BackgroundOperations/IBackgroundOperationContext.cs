using System;
using System.Threading.Tasks;

namespace ImageResizerDemo.Services.BackgroundOperations;

public interface IBackgroundOperationContext
{
    Guid OperationId { get; }

    Task ReportProgress(int itemsProcessed, int itemsTotal, string description);

    Task ReportError(string description);

    Task ReportSuccess(string description);
}