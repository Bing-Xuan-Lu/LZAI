using LZAI.Service.IService;
using LZAIMonitor.Areas.Cle.Models.AuditEvent;
using LZAIMonitor.Controllers;
using OfficeOpenXml;
using PSTFrame.MVC;
using PSTFrame.MVC.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static LZAI.Model.DB.Vw_AuditEvent;

namespace LZAIMonitor.Areas.Cle.Controllers
{
    public class AuditEventController : WebBaseController
    {
        protected IAuditEventService _auditEventService;



        public AuditEventController(IAuditEventService auditEventService)
        {
            _auditEventService = auditEventService;


        }
        // GET: Cle/CarWatchRecord
        public ActionResult AuditEvent10100()
        {
            AuditEventViewModel model = new AuditEventViewModel();
            SetSelectListToViewBag();
            model.QueryFilter.SDate = DateTime.Now.ToString("yyyy-MM-dd");
            model.QueryFilter.EDate = DateTime.Now.ToString("yyyy-MM-dd");

            return View(model);
        }
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="Filter">The query filter.</param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult Query(AuditEventViewModel.Filter Filter)
        {
            AuditEventViewModel model = new AuditEventViewModel();


            var datas = _auditEventService.GetSelectVw_AuditEvent(Filter.Name,Filter.SDate,Filter.EDate);
            model.DataList = Filter.Length > 0
             ? datas.Skip(Filter.Start).Take(Filter.Length).ToList()
             : datas.ToList();
            Filter.TotalCount = datas.Count();
            model.QueryFilter = Filter;
            return DataTablesJson(model.QueryFilter.Draw, model.QueryFilter.TotalCount,
                model.QueryFilter.TotalCount, model.DataList, JsonRequestBehavior.AllowGet);
        }
        #region 檔案下載
        /// <summary>
        /// 檔案下載
        /// </summary>
        /// <param name="Filter"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult Download(AuditEventViewModel.Filter Filter)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            MessageModel ResultMessage = new MessageModel();
            using (var package = new ExcelPackage())
            {
                try
                {
                    var data = _auditEventService.GetSelectVw_AuditEvent(Filter.Name, Filter.SDate, Filter.EDate).Select(x =>
                       new AuditEvent_download()
                       {
                           AuditEventId = x.AuditEventId,     
                           Account = x.Account,
                           UserName = x.UserName,
                           ClientIPAddress = x.ClientIPAddress,
                           FuncName = x.FuncName,
                           EventDateTime = x.EventDateTime,
                           ActionType = x.ActionType,
                           Action = x.Action,
                       });

                    var worksheet = package.Workbook.Worksheets.Add("稽核記錄");
                    worksheet.Cells["A1"].LoadFromCollection(data, true);
                    worksheet.Cells.AutoFitColumns();
                    var ms = new System.IO.MemoryStream(package.GetAsByteArray());
                    var arrBites = ms.ToArray();
                    //檔案產生path
                string path = Server.MapPath("~/Export/Excel/AuditEvent");
                    string fileName = $"稽核記錄{DateTime.Now.ToString("yyyyMMddhhmm")}.xlsx";
                    if (!System.IO.Directory.Exists(path))    //資料夾若不存在，Created it.
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    else
                    {
                        //下載前刪除殘存檔案
                        var FileDir = System.IO.Directory.EnumerateFileSystemEntries(path);
                        foreach (var File in FileDir)
                        {
                            System.IO.File.Delete(File);
                        }
                    }
                    using (System.IO.FileStream fs = new System.IO.FileStream(Path.Combine(path, fileName), FileMode.OpenOrCreate))
                    {
                        //ms.CopyTo(fs);
                        fs.Write(arrBites, 0, arrBites.Length);
                        fs.Flush();
                    }
                    ResultMessage.Status = true;
                    ResultMessage.Message = "請下載";
                    ResultMessage.FileDownload = new List<FileUrlModel>();
                    ResultMessage.FileDownload.Add(new FileUrlModel { FileName = fileName, FileUrl = Url.Content($"~/Export/Excel/AuditEvent/{fileName}") });
                }
                catch (Exception ex)
                {
                    ResultMessage.Status = false;
                    ResultMessage.Message = "發生錯誤：" + ex.Message;
                }
            }
            return Json(ResultMessage);
        }
        #endregion

        private void SetSelectListToViewBag()
        {



            //使用者姓名
            var Name = _auditEventService.GetVw_AuditEvent().GroupBy(t => t.UserName)
          .Select(s => new { UserNameList = s.OrderByDescending(t => t.AuditEventId).First() }).Select(z => new SelectListItem
          {
              Text = z.UserNameList.UserName,
              Value = z.UserNameList.UserName
          }).ToList();
            ViewBag.Name = Name;



        }
    }
}