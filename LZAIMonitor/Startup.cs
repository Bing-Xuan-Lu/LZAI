using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Hangfire;
using Owin;
using Hangfire.MemoryStorage;
using LZAI.Infrastructure.Ioc;
using Microsoft.Owin;
using LZAIMonitor.Fillters;
using Microsoft.AspNet.SignalR;
using GlobalConfiguration = Hangfire.GlobalConfiguration;
using Autofac.Integration.SignalR;
using LZAIMonitor.Hubs;
using LZAIMonitor.Task;
using Microsoft.AspNet.SignalR.Infrastructure;

[assembly: OwinStartup(typeof(LZAIMonitor.Startup))]
namespace LZAIMonitor
{
    public class Startup : MvcApplication
    {
        public void Configuration(IAppBuilder app)
        {
            // 指定Hangfire使用記憶體儲存任務
            GlobalConfiguration.Configuration
                .UseMemoryStorage()
                .UseNLogLogProvider()
                .UseAutofacActivator(container);
            // 啟用HanfireServer
            app.UseHangfireServer();
            // 啟用Hangfire的Dashboard
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = new[] { new CustomAuthorizeFilter() }
            });

            //預設先開警示排程
//#if !DEBUG
            GpsRealTimeRunner.RunRealTime();
//#endif
            
            // Get your HubConfiguration. In OWIN, you'll create one
            // OWIN SIGNALR SETUP:
            IDependencyResolver resolver = new AutofacDependencyResolver(container);
            IHubContext hubContext = resolver.Resolve<IConnectionManager>().GetHubContext<CarWatchHub>();
            app.MapSignalR();

        }
    }
}