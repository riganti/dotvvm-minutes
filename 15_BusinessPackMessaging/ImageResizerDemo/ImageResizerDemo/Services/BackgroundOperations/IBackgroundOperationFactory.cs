using System;
using System.Threading.Tasks;

namespace ImageResizerDemo.Services.BackgroundOperations;

public interface IBackgroundOperationFactory
{

    BackgroundOperationProgress Start(Func<IBackgroundOperationContext, Task> action);

}