using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ImageResizerDemo.Services.BackgroundOperations;

public class SignalRBackgroundOperationsHub : Hub
{

    public async Task Subscribe(Guid operationId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, operationId.ToString().ToLower());
    }

    public async Task Unsubscribe(Guid operationId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, operationId.ToString().ToLower());
    }

}