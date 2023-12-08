using Microsoft.AspNetCore.SignalR;

namespace DACN3.Hubs
{
    public class NotificationHubs:Hub
    {
        public async Task SendMessageToUser(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}
