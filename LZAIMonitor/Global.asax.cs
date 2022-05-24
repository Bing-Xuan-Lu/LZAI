using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Hangfire;
using LZAI.Infrastructure.Ioc;
using LZAI.Model.DB;
using LZAIMonitor.Hubs;
using Microsoft.AspNet.SignalR;
using PSTFrame.MVC;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using GlobalConfiguration = System.Web.Http.GlobalConfiguration;

namespace LZAIMonitor
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IContainer container;
        protected SqlTableDependency<CarWatchEvent> carWatchEventDependency { get; set; }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //注入所有組件
            System.Web.Http.HttpConfiguration config = System.Web.Http.GlobalConfiguration.Configuration;
            container = MvcContainer.Init(config);
            //啟動車牌辨識SQL監視器
            
            carWatchEventDependency = new SqlTableDependency<CarWatchEvent>(
                System.Configuration.ConfigurationManager.ConnectionStrings["LZAI"].ConnectionString);
            carWatchEventDependency.OnChanged += (o, args) =>
                 BackgroundJob.Enqueue<CarWatchHubHelper>(x => x.SendCarWatchEventAsync(args.ChangeType, args.Entity));
            carWatchEventDependency.OnError += CarWatchEventDependencyOnOnError;
            carWatchEventDependency.Start();
        }

        private void CarWatchEventDependencyOnOnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            throw e.Error;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //寫到Log回傳的LogId
            int ErrorID = 0;

            String Errormsg = String.Empty;
            Exception unhandledException = Server.GetLastError();
            Response.Clear();
            HttpException httpException = unhandledException as HttpException;
            Errormsg = "發生例外網頁:{0}錯誤訊息:{1}堆疊內容:{2}";
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            if (httpException != null)
            {
                Errormsg = String.Format(Errormsg, Request.Path + Environment.NewLine,
                    unhandledException.GetBaseException().Message + Environment.NewLine,
                    unhandledException.StackTrace + Environment.NewLine);
                //寫入事件撿視器  
                //EventLog.WriteEntry("WebError", Errormsg, EventLogEntryType.Error);
                //寫入文字檔
                //System.IO.File.AppendAllText(Server.MapPath(String.Format("WebLog\\{0}.txt",
                //   DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss"))), Errormsg);

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found                        
                        routeData.Values.Add("action", "StatusCode404");
                        break;
                    case 500:
                        // server error                     
                        routeData.Values.Add("action", "StatusCode500");
                        break;
                    default:
                        routeData.Values.Add("action", "General");
                        break;
                }

            }
            else
            {
                routeData.Values.Add("action", "General");
            }

            #region 寫Log
            //// 在發生未處理的錯誤時執行的程式碼
            string logAppName = "LZAIMonitor";
            bool logIt = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["LogErrors"]);
            if (logIt)
            {
                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["LZAI"].ConnectionString;

                PSTCOM.Util.LogException exc = new PSTCOM.Util.LogException();
                ErrorID = exc.HandleException(logAppName, connStr, unhandledException.GetBaseException());

                Server.ClearError();
                //string transferUrl;
                //transferUrl = System.Configuration.ConfigurationManager.AppSettings["LogErrors_detailURL"] + "?ErrorID=" + ErrorID.ToString();
                //Server.Transfer(transferUrl);
            }
            #endregion

            // Pass exception details to the target error View.
            //routeData.Values.Add("Error", unhandledException);
            routeData.Values.Add("Error", $"錯誤代碼：{ErrorID}");

            // clear error on server 
            Server.ClearError();
            // Call target Controller and pass the routeData.
            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(
                new HttpContextWrapper(Context), routeData));
        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (carWatchEventDependency.Status == TableDependencyStatus.Started)
            {
                carWatchEventDependency.Stop();
            }
        }
    }
}
