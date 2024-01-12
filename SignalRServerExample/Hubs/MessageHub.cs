using Microsoft.AspNetCore.SignalR;

namespace SignalRServerExample.Hubs
{
    public class MessageHub : Hub
    {
        //Groupdan önce IEnumerable ile connectionIds getiriyorduk o yüzden bunu yorum satırına alıp parametre olarak group çağırdık
        //public async Task SendMessageAsync(string message, IEnumerable<string> connectionIds)
        //{
        //public async Task SendMessageAsync(string message, string groupName, IEnumerable<string> connectionIds)
        //{
        //public async Task SendMessageAsync(string message, IEnumerable<string> groups)
        //{
            public async Task SendMessageAsync(string message, string groupName)
        {
            //Client Türleri : All - Caller - Other 
            #region Caller
            //Sadece Server'a bildirim gönderen clientla iletişim kuran client türüdür.
            //await Clients.Caller.SendAsync("receiveMessage", message);
            #endregion
            #region All
            //Server'a bağlı olan tüm clientlarla iletişim kuran client türüdür.
            //await Clients.All.SendAsync("receiveMessage", message);
            #endregion
            #region Other
            //Sadece Server'a bildirim gönderen kişi hariç server'a bağlı olan tüm clientlara mesaj gönderen client türüdür.
            //await Clients.Others.SendAsync("receiveMessage", message);
            #endregion

            #region Hub Clients Metotları
            #region AllExcept
            //belirtilen clientlar hariç server'a bağlı olan tüm clientlara bildirimde bulunur.
            //await Clients.AllExcept(connectionIds).SendAsync("receiveMessage", message);
            #endregion
            #region Client
            //Server'a bağlı olan clientlar arasında sadece belirtilen clienta bildirimde bulunur.
            //await Clients.Client(connectionIds.First()).SendAsync("receiveMessage", message);
            #endregion
            #region Clients
            //Server'a bağlı olan clientlar arasında sadece belirtilen clientlara bildirimde bulunur.
            //await Clients.Clients(connectionIds).SendAsync("receiveMessage", message);
            #endregion



            #region Group
            //Belirtilen gruptaki tüm clientlara bildiride bulunur.
            //Önce gruplar oluşturulmalı ardından clientlar gruba subscribe olmalı
            //await Clients.Group(groupName).SendAsync("receiveMessage", message);
            #endregion
            #region GroupExcept
            //Belirtilen gruptaki belirtilen clientlar dışındaki tüm clientlara bildiride bulunur.
            //Önce gruplar oluşturulmalı ardından clientlar gruba subscribe olmalı
            //await Clients.GroupExcept(groupName,connectionIds).SendAsync("receiveMessage", message);
            #endregion
            #region Groups
            //Birden çok gruptaki clientlara bildiride bulunur.
            //await Clients.Groups(groups).SendAsync("receiveMessage", message);
            #endregion
            #region OthersInGroup
            //Bildiride bulunan client hariç gruptaki tüm clientlara bildiride bulunur.
            await Clients.OthersInGroup(groupName).SendAsync("receiveMessage", message);
            #endregion
            #region User
            //Authentication olan tek bir kullanıcıya erişmemizi sağlar.
            #endregion
            #region Users
            //Authentication olan tüm kullanıcılara erişmemizi sağlar.
            #endregion
            #endregion

        }

        public async override Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("getConnectionId", Context.ConnectionId);
        }
        public async Task addGroup(string connectionId, string groupName)
        {
            await Groups.AddToGroupAsync(connectionId, groupName);
        }
    }

}
