using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace VlogRoom.Web.Hubs
{
    public class Notifications : Hub
    {
        public void AddNotification(string userId)
        {
            var notification = $"{this.Context.User.Identity.Name} subscribed to you!";
            Clients.Group(userId).receveNotification(notification);
            this.Clients.All.receveNotification(notification);
        }

        public override Task OnConnected()
        {
            var name = this.Context.User.Identity.Name;
            Groups.Add(this.Context.ConnectionId, name);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var name = this.Context.User.Identity.Name;
            Groups.Remove(this.Context.ConnectionId, name);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            var name = this.Context.User.Identity.Name;
            Groups.Add(this.Context.ConnectionId, name);

            return base.OnReconnected();
        }
    }
}