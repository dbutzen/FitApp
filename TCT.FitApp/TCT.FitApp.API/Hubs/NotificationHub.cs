using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TCT.FitApp.API.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(Guid userId, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", userId, message);
        }
    }
}
