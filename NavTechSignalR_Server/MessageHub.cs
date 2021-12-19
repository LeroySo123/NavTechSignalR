using Microsoft.AspNetCore.SignalR;

namespace NavTechSignalR_Server
{
    public class MessageHub :Hub
    {
        //for signalR connect to blazor
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        //for receive meassage to send to blazor
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("message", message);
        }
    }
}
