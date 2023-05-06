using System;

namespace ImageResizerDemo.Services.BackgroundOperations;

public class BackgroundOperationProgress
{
    public Guid OperationId { get; set; }

    public string Description { get; set; }

    public bool IsCompleted { get; set; }

    public bool IsError { get; set; }

    public double Percent { get; set; }

}