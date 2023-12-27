using Microsoft.AspNetCore.SignalR;
using SignalRExample.Constants;

namespace SignalRExample.Hubs;

public class DeathlyHallowsHub : Hub
{
    public Dictionary<string, int> GetRaceStatus()
    {
        return StaticData.DeathlyHallowRace;
    }
}