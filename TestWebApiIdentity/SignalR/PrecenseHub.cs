using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TestWebApiIdentity.Extensions;

namespace TestWebApiIdentity.SignalR
{
    [Authorize]
    public class PrecenseHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("UserIsOnline", Context.User.GetUsername());
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.Others.SendAsync("UserIsOffline", Context.User.GetUsername());
            await base.OnDisconnectedAsync(exception);
        }
    }
}
