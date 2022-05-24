using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LZAI.MgrLib.Model;
using LZAI.MgrLib.Model.DB;
using LZAI.Model.DB;
using LZAIMonitor.Fillters;

namespace LZAIMonitor.Controllers
{
    /// <summary>
    /// 監控平台
    /// </summary>
    [Authorize]
    public class MonitorController : WebBaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CleWasteCarService"></param>
        public MonitorController()
        {

        }

        /// <summary>
        /// 車道監視
        /// </summary>
        /// <returns></returns>
        public ActionResult CarWatch()
        {
            return View();
        }

        /// <summary>
        /// 即時歷史軌跡
        /// </summary>
        /// <returns></returns>
        public ActionResult Track()
        {
            return View();
        }

    }
}