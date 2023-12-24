using Microsoft.AspNetCore.SignalR;

namespace SignalRExample.Hubs;

public class UserHub : Hub
{
    private static int TotalViews { get; set; }

    public async Task NewWindowLoaded()
    {
        TotalViews++;
        
        await Clients.All.SendAsync("updateTotalViews", TotalViews);
    }
}