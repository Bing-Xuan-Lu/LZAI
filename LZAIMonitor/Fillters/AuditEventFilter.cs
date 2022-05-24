using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using LZAI.MgrLib.Model.DB;
using LZAI.Model.DB;
using LZAI.Service.IService;
using LZAIMonitor.Controllers;
using PSTFrame.MVC.Model;

namespace LZAIMonitor.Fillters
{
    /// <summary>
    /// 稽核紀錄Filter
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public class AuditEventFilter : ActionFilterAttribute
    {
        public IAuditEventService IauditEventService { get; set; }
        protected DataType dataType;
        protected DataProcessType dataProcessType;

        public AuditEventFilter(DataType dataType, DataProcessType dataProcessType)
        {
            this.dataType = dataType;
            this.dataProcessType = dataProcessType;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            WebBaseController controller = filterContext.Controller as WebBaseController;
            var audit = new AuditEvent()
            {
                EventType = "DataManage",
                ClientIPAddress = PSTFrame.MVC.Helper.HelperClass.GetIPHelper(),
                UserId = controller.LoginUser.UserId,
                UnitId = controller.LoginUser.UnitId,
                FuncId= (int)dataType,
                ActionType = Enum.GetName(typeof(DataProcessType), dataProcessType),
                TableName = Enum.GetName(typeof(DataType), dataType),
                DataProcessType = ((int)dataProcessType).ToString(),
                PrimaryData1 = "",
                PrimaryData2 = "",
                PrimaryData3 = "",
                PrimaryDataInt = controller.dataKey,
                SubDataType = controller.subDataType,
                SubDataPrimaryDataInt = controller.subDataKey,
                JsonData = ""
            };
            var resultModel = IauditEventService.InsertAuditEvent(audit);
            base.OnResultExecuted(filterContext);
        }
    }
}