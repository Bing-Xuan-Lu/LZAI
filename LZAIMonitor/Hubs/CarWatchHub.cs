using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LZAI.Model.DB;
using LZAI.Model.QueryFillter;
using LZAI.Service.IService;
using LZAIMonitor.Models;

using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using TableDependency.SqlClient.Base.Enums;

namespace LZAIMonitor.Hubs
{
    /// <summary>
    /// 車牌辨識監聽器
    /// </summary>
    [HubName("CarWatchHub")]
    public class CarWatchHub : Hub
    {
        public CarWatchHub()
        {

        }

        [HubMethodName("CarWatchListenIng")]
        public void Hello()
        {
            GlobalHost.ConnectionManager.GetHubContext<CarWatchHub>().Clients.All.hello();
            Clients.All.hello();
        }

        public void SendMessage(string message)
        {
            Clients.Caller.sendMessage(message);
        }

        public void SendMessage(string connectionId, string message)
        {
            Clients.Client(connectionId).sendMessage(message);
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}