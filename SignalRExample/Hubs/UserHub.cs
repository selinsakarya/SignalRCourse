using Microsoft.AspNetCore.SignalR;

namespace SignalRExample.Hubs;

public class UserHub : Hub
{
    private static int TotalViews { get; set; }
    
    private static int TotalActiveUsers { get; set; }

    public override async Task OnConnectedAsync()
    {
        TotalActiveUsers++;
        await Clients.All.SendAsync("updateTotalActiveUsers", TotalActiveUsers);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        TotalActiveUsers--;
        await Clients.All.SendAsync("updateTotalActiveUsers", TotalActiveUsers);
        await base.OnConnectedAsync();
    }

    public async Task NewWindowLoaded()
    {
        TotalViews++;
        
        await Clients.All.SendAsync("updateTotalViews", TotalViews);
    }
}