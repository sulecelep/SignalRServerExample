using Microsoft.AspNetCore.SignalR;
using SignalRServerExample.Hubs;

namespace SignalRServerExample.Business
{
    public class MyBusiness
    {
        //MyHub'ı partiallamış gibi olduk 
        readonly IHubContext<MyHub> _hubContext;

        public MyBusiness(IHubContext<MyHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendMessageAsync(string message)
        {
            //Ekstra işlemler.....

            await _hubContext.Clients.All.SendAsync("receiveMessage", message); //client tarafında receiveMessage adında metot bekliyor

        }
    }
}
