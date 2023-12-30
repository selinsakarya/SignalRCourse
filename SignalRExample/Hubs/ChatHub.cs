using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRExample.Data;

namespace SignalRExample.Hubs;

public class ChatHub : Hub
{
    private readonly ApplicationDbContext _dbContext;

    public ChatHub(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task SendMessageToAll(string sender, string message)
    {
        await Clients.All.SendAsync("newMessageReceived", sender, message);
    }
    
    [Authorize]
    public async Task SendPrivateMessage(string sender, string receiver, string message)
    {
        IdentityUser? receiverUser = await _dbContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Email.ToLowerInvariant(), receiver.ToLowerInvariant(), StringComparison.InvariantCulture));

        if (receiverUser != null)
        {
            await Clients.User(receiverUser.Id).SendAsync("newMessageReceived", sender, message);
        }
    }
}