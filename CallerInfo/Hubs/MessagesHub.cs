using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using CallerInfo.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CallerInfo.Hubs
{
    public class TestHub : Hub
    {
        private static string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public void Hello()
        {
            Clients.All.hello();
        }

        [HubMethodName("sendMessages")]
        public static void SendMessages()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<TestHub>();
            context.Clients.All.updateMessages();
        }
    }
}