using Microsoft.AspNetCore.SignalR;

namespace SignalRExample.Hubs;

public class HarryPotterHouseGroupHub : Hub
{
    private static List<string> _groupsJoined = new();

    public async Task JoinHouse(string houseName)
    {
        string groupName = $"{houseName}:{Context.ConnectionId}";

        if (!_groupsJoined.Contains(groupName))
        {
            _groupsJoined.Add(groupName);
            
            string houseList = "";
            
            foreach (var group in _groupsJoined)
            {
                if (group.Contains(Context.ConnectionId))
                {
                    houseList += group.Split(':').First() + " ";
                }
            }

            await Clients.Caller.SendAsync("subscriptionStatus", houseList, houseName, true);
            
            await Clients.GroupExcept(houseName, Context.ConnectionId).SendAsync("memberJoinedToHouse", houseName);

            await Groups.AddToGroupAsync(Context.ConnectionId, houseName);
        }
    }

    public async Task LeaveHouse(string houseName)
    {
        string groupName = $"{houseName}:{Context.ConnectionId}";

        if (_groupsJoined.Contains(groupName))
        {
            _groupsJoined.Remove(groupName);
            
            string houseList = "";
            
            foreach (var group in _groupsJoined)
            {
                if (group.Contains(Context.ConnectionId))
                {
                    houseList += group.Split(':').First() + " ";
                }
            }
            
            await Clients.Caller.SendAsync("subscriptionStatus", houseList, houseName, false);

            await Clients.GroupExcept(houseName, Context.ConnectionId).SendAsync("memberLeftTheHouse", houseName);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, houseName);
        }
    }

    public async Task TriggerHouseNotification(string houseName)
    {
        await Clients.Group(houseName).SendAsync("notificationReceived", houseName);
    }
}