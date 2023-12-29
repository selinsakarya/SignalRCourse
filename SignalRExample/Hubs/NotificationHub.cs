using Microsoft.AspNetCore.SignalR;

namespace SignalRExample.Hubs;

public class NotificationHub : Hub
{
    private static List<string> _messages = new();

    public async Task SendMessage(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            _messages.Add(message);
            
            await Clients.All.SendAsync("NewMessageReceived", message, _messages.Count);
        }
    }

    public List<string> GetMessages()
    {
        return _messages;
    }
}