using Microsoft.AspNetCore.SignalR;

namespace BlazorAppIdentityDotNet8.Components.Hubs
{
    public class ChatHub : Hub<IChatHubClient>
    {
        public override async Task OnConnectedAsync()
        {
            Clients.Client(Context.ConnectionId).ReceiveNotification(
                $"Thanks for connecting{Context.User?.Identity?.Name}");

            await base.OnConnectedAsync();
        }
        public async Task SendMessage(string automessage, string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }

    public interface IChatHubClient
    {
        Task ReceiveNotification(string message);
        Task SendAsync(string automessage, string user, string message);
    }
}
