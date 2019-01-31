using System.Threading.Tasks;
using SignalRLibrary.Models;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreSignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(MyMessage myMessage) => await Clients.All.SendAsync("ReceiveMessage", myMessage);        
    }
}