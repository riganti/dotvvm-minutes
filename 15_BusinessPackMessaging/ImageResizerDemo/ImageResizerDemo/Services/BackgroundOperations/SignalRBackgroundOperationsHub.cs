using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ImageResizerDemo.Services.BackgroundOperations;

public class SignalRBackgroundOperationsHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var operationId = Guid.Parse(Context.GetHttpContext()!.Request.Query["operationId"]);
        await Groups.AddToGroupAsync(Context.ConnectionId, operationId.ToString().ToLower());

        await base.OnConnectedAsync();
    }
    
}