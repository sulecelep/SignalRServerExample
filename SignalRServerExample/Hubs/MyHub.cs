using Microsoft.AspNetCore.SignalR;
using SignalRServerExample.Interfaces;

namespace SignalRServerExample.Hubs
{
    public class MyHub :Hub<IMessageClient>
    {
        static List<string> clients =new List<string>();
        //public async Task SendMessageAsync(string message)
        //{
        //    //Ekstra işlemler.....

        //    await Clients.All.SendAsync("receiveMessage", message); //client tarafında receiveMessage adında metot bekliyor
            
        //}
        public async override Task OnConnectedAsync()
        {
            //ConnectionId Nedir?
            //Hub'a bağlantı gerçekleştiren clientlara sistem tarafından verilen unique bir değerdir.
            //Amacı clientları birbirinden ayırmaktır.
            //var id=Context.ConnectionId; //bağlanan kullanıcının connectionId'si

            clients.Add(Context.ConnectionId);
            //await Clients.All.SendAsync("clients", clients);
            //await Clients.All.SendAsync("userJoined",Context.ConnectionId);
            await Clients.All.Clients(clients); //Strongly Type Hubs'dan IMessageClient'ın fonksiyonlarıyla sonra bu şekilde düzenledik 
            await Clients.All.UserJoined(Context.ConnectionId);
            
        }
        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            //önce bağlantı kopar sonra bağlantı yenilenir

            clients.Remove(Context.ConnectionId);
            await Clients.All.Clients(clients);
            await Clients.All.UserLeaved(Context.ConnectionId);
        }
    }
}
