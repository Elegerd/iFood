using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lifestyle.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Lifestyle.SignalR
{
    public class NotificationsHub : Hub
    {
        public void SendNotification(string message)
        {
            string name;
            using (UserContext db = new UserContext())
            {
                name = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name).Name;
            }
            message = name + " добавил себе в дневник " + message;
            Clients.Others.sendNotification(message);
        }
    }
}