using LZAI.Service.IService;
using LZAIMonitor.Areas.Cle.Models.CarWatchRecord;
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
using static LZAI.Model.DB.Vw_CarWatchEvent;

namespace LZAIMonitor.Areas.Cle.Controllers
{
    public class CarWatchRecordController : WebBaseController
    {
        protected ICarWatchEventService _carWatchEventService;

        public CarWatchRecordController(ICarWatchEventService carWatchEventService)
        {
            _carWatchEventService = carWatchEventService;
        }
        // GET: Cle/CarWatchRecord
        public ActionResult CarWatchRecord10100()
        {
            if (Request.QueryString["IsFront"] != null && Request.QueryString["IsFront"] == "1")
                TempData["IsFront"] = "1";

            CarWatchRecordModel model = new CarWatchRecordModel();
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
        public ActionResult Query(CarWatchRecordModel.Filter Filter)
        {
            CarWatchRecordModel model = new CarWatchRecordModel();

            var datas = _carWatchEventService.GetSelectVwCarWatchEvent(Filter.IsSetGPS, Filter.SDate, Filter.EDate, Filter.CARNum, Filter.Fac_No, Filter.Fac_Name, Filter.RegionId);
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
        /// 下載
        /// </summary>
        /// <param name="Filter"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult Download(CarWatchRecordModel.Filter Filter)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            MessageModel ResultMessage = new MessageModel();
            using (var package = new ExcelPackage())
            {
                try
                {
                    var data = _carWatchEventService.GetSelectVwCarWatchEvent(Filter.IsSetGPS, Filter.SDate, Filter.EDate, Filter.CARNum, Filter.Fac_No, Filter.Fac_Name, Filter.RegionId).Select(x =>
                       new CarWatch_download()
                       {
                           Head_No = x.Head_No,
                           Plate_No = x.Plate_No,
                           InsTime = x.InsTime,
                           Fac_No = x.Fac_No,
                           Fac_Name = x.Fac_Name,
                           CHIsSetGPS = x.CHIsSetGPS,
                           CHIsGPSHistory = x.CHIsGPSHistory,
                           RegionName = x.RegionName,

                       });

                    var worksheet = package.Workbook.Worksheets.Add("車牌辨識紀錄");
                    worksheet.Cells["A1"].LoadFromCollection(data, true);
                    worksheet.Cells.AutoFitColumns();
                    var ms = new System.IO.MemoryStream(package.GetAsByteArray());
                    var arrBites = ms.ToArray();
                    //檔案產生path
                    string path = Server.MapPath("~/Export/Excel/CarWatchRecord");
                    string fileName = $"車牌辨識紀錄_{DateTime.Now.ToString("yyyyMMddhhmm")}.xlsx";
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
                    ResultMessage.FileDownload.Add(new FileUrlModel { FileName = fileName, FileUrl = Url.Content($"~/Export/Excel/CarWatchRecord/{fileName}") });
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
            var cleWasteCar = _carWatchEventService.GetCleWasteCar();

            #region 頭尾車查詢
            //頭車
            var CleCar = cleWasteCar.GroupBy(t => t.Head_No)
          .Select(s => new { Car_List = s.OrderByDescending(t => t.SerNo).First() }).Select(z => new SelectListItem
          {
              Text = z.Car_List.Head_No,
              Value = z.Car_List.Head_No
          }).ToList();
            //尾車
            var CleCarPlate_No = cleWasteCar.GroupBy(t => t.Plate_No)
          .Select(s => new { Car_List = s.OrderByDescending(t => t.SerNo).First() }).Select(z => new SelectListItem
          {
              Text = z.Car_List.Plate_No,
              Value = z.Car_List.Plate_No
          }).ToList();
            //尾車塞入
            CleCar.AddRange(CleCarPlate_No);
            ViewBag.CleCar = CleCar;
            #endregion

            //清除管編
            var CleFac = cleWasteCar.GroupBy(t => t.Fac_No)
       .Select(s => new { Fac_NoList = s.OrderByDescending(t => t.SerNo).First() }).Select(z => new SelectListItem
       {
           Text = z.Fac_NoList.Fac_No,
           Value = z.Fac_NoList.Fac_No
       }).ToList();
            ViewBag.CleFac = CleFac;

            //清除事業名稱
            var CleFacN = cleWasteCar.GroupBy(t => t.Fac_Name)
          .Select(s => new { Fac_NoList = s.OrderByDescending(t => t.SerNo).First() }).Select(z => new SelectListItem
          {
              Text = z.Fac_NoList.Fac_Name,
              Value = z.Fac_NoList.Fac_Name
          }).ToList();
            ViewBag.CleFacN = CleFacN;

            var RegionId = _carWatchEventService.GetStop_Region().Select(x => new SelectListItem
            {
                Text = x.RegionName,
                Value = x.ID.ToString()
            }).ToList();
            ViewBag.RegionId = RegionId;

        }
    }
}