using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreSignalR.Hubs;
using SignalRLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreSignalR.Pages
{
    public class SendModel : PageModel
    {
        IHubContext<ChatHub> hub;
        public SendModel(IHubContext<ChatHub> hub)
        {
            this.hub = hub;
        }

        public async void OnGet(string user, string message)
        {
            var myMessage = new MyMessage 
            {
                User = user,
                Message = message
            };
            await hub.Clients.All.SendAsync("ReceiveMessage", myMessage);
        }
    }
}