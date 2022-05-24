using LZAI.Model.DB;
using LZAI.Service.IService;
using LZAIMonitor.Areas.Cle.Models.GrantInfo;
using LZAIMonitor.Areas.Cle.Models.WasteCar;
using LZAIMonitor.Controllers;
using Newtonsoft.Json;
using OfficeOpenXml;
using PSTFrame.MVC;
using PSTFrame.MVC.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LZAIMonitor.Fillters;

namespace LZAIMonitor.Areas.Cle.Controllers
{
    public class WasteCarController : WebBaseController
    {
        protected ICleWasteCarService _CleWasteCarService;

        public WasteCarController(ICleWasteCarService CleWasteCarService)
        {
            _CleWasteCarService = CleWasteCarService;
        }




        /// <summary>
        /// 車輛列表
        /// </summary>
        /// <returns></returns>
        [AuditEventFilter(DataType.CleWasteCar, DataProcessType.Select)]
        public ActionResult WasteCar10100()
        {
            WasteCarViewModel wasteCarViewModel = new WasteCarViewModel();
            SetSelectListToViewBag();

            return View(wasteCarViewModel);
        }
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="Filter">The query filter.</param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult Query(WasteCarViewModel.Filter Filter)
        {

            WasteCarViewModel model = new WasteCarViewModel();


            var datas = _CleWasteCarService.SerachCleWasteCar(Filter.Fac_Name, Filter.Fac_No, Filter.CARNum);
            model.DataList = Filter.Length > 0
             ? datas.Skip(Filter.Start).Take(Filter.Length).ToList()
             : datas.ToList();
            Filter.TotalCount = datas.Count();
            model.QueryFilter = Filter;
            return DataTablesJson(model.QueryFilter.Draw, model.QueryFilter.TotalCount,
                model.QueryFilter.TotalCount, model.DataList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增列表
        /// </summary>
        /// <returns></returns>
        public ActionResult WasteCar201A0()
        {
            var model = new CleWasteCar();
            SetSelectListToViewBag();

            return View(model);

        }

        /// <summary>
        /// 編輯列表
        /// </summary>
        /// <returns></returns>
        public ActionResult WasteCar201E0(int SerNo)
        {
            SetSelectListToViewBag();
            var model = _CleWasteCarService.GetById(SerNo);
            return View(model);
        }

        #region 新增
        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="cleWasteCar"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.CleWasteCar, DataProcessType.Insert)]
        public ActionResult Insert(CleWasteCar cleWasteCar)
        {
            if (!ModelState.IsValid)
            {
                return Json(GetModelError());
            }
            cleWasteCar.InsertDateTime = DateTime.Now;
            cleWasteCar.InsertUnitID = LoginUser.UnitId;
            cleWasteCar.InsertUserID = LoginUser.UserId;
            cleWasteCar.UpdateDateTime = DateTime.Now;
            cleWasteCar.UpdateUnitID = LoginUser.UnitId;
            cleWasteCar.UpdateUserID = LoginUser.UserId;

            ResultMessage = _CleWasteCarService.InsertData(cleWasteCar);
            dataKey = cleWasteCar.SerNo;
            if (ResultMessage.Status)
            {
                ResultMessage.Url = "";
            }
            return Json(ResultMessage);
        }
        #endregion
        #region 編輯
        /// <summary>
        /// Update
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.CleWasteCar, DataProcessType.Update)]
        public ActionResult Update(CleWasteCar cleWasteCar)
        {
            if (!ModelState.IsValid)
            {
                return Json(GetModelError());
            }
            cleWasteCar.UpdateDateTime = DateTime.Now;
            cleWasteCar.UpdateUnitID = LoginUser.UnitId;
            cleWasteCar.UpdateUserID = LoginUser.UserId;
            dataKey = cleWasteCar.SerNo;
            ResultMessage = _CleWasteCarService.UpdateData(cleWasteCar);
            if (ResultMessage.Status)
            {
                ResultMessage.Url = "";
            }

            return Json(ResultMessage);
        }
        #endregion
        #region 刪除
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="SerNo"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.CleWasteCar, DataProcessType.Delete)]
        public ActionResult Delete(int SerNo)
        {
            dataKey = SerNo;
            var ResultMessage = _CleWasteCarService.DeleteData(SerNo);
            if (ResultMessage.Status)
            {
                ResultMessage.Url = "";
            }
            return Json(ResultMessage);
        }
        #endregion

        #region 檔案下載
        /// <summary>
        /// 下載
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult Download(WasteCarViewModel.Filter Filter)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            MessageModel ResultMessage = new MessageModel();
            using (var package = new ExcelPackage())
            {
                try
                {
                    var data = _CleWasteCarService.SerachCleWasteCar(Filter.Fac_Name, Filter.Fac_No, Filter.CARNum).Select(x =>
                       new VwCleWasteCar_download()
                       {
                           Fac_No = x.Fac_No,
                           Fac_Name = x.Fac_Name,
                           Head_No = x.Head_No,
                           Plate_No = x.Plate_No,
                           CHCarMachine = x.CHCarMachine,
                           CHCarModel = x.CHCarModel,
                       });

                    var worksheet = package.Workbook.Worksheets.Add("車輛管理");
                    worksheet.Cells["A1"].LoadFromCollection(data, true);
                    worksheet.Cells.AutoFitColumns();
                    var ms = new System.IO.MemoryStream(package.GetAsByteArray());
                    var arrBites = ms.ToArray();
                    //檔案產生path
                    string path = Server.MapPath("~/Export/Excel/WasteCar");
                    string fileName = $"車輛管理_{DateTime.Now.ToString("yyyyMMddhhmm")}.xlsx";
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
                    ResultMessage.FileDownload.Add(new FileUrlModel { FileName = fileName, FileUrl = Url.Content($"~/Export/Excel/WasteCar/{fileName}") });
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
        {//清除管編
            var CleFac = _CleWasteCarService.GetCleWasteCar().GroupBy(t => t.Fac_No)
          .Select(s => new { Fac_NoList = s.OrderByDescending(t => t.SerNo).First() }).Select(z => new SelectListItem
          {
              Text = z.Fac_NoList.Fac_No,
              Value = z.Fac_NoList.Fac_No
          }).ToList();
            ViewBag.CleFac = CleFac;

            //清除事業名稱
            var CleFacN = _CleWasteCarService.GetCleWasteCar().GroupBy(t => t.Fac_Name)
          .Select(s => new { Fac_NoList = s.OrderByDescending(t => t.SerNo).First() }).Select(z => new SelectListItem
          {
              Text = z.Fac_NoList.Fac_Name,
              Value = z.Fac_NoList.Fac_Name
          }).ToList();
            ViewBag.CleFacN = CleFacN;


            //車種
            var CarModel = _CleWasteCarService.GetPublicCode("vehicle_model").Select(x =>
                 new SelectListItem
                 {
                     Text = x.CodeNameCH,
                     Value = x.CodeNo
                 }).ToList();
            ViewBag.CarModel = CarModel;

            #region 頭尾車查詢
            //頭車
            var CleCar = _CleWasteCarService.GetCleWasteCar().GroupBy(t => t.Head_No)
          .Select(s => new { Car_List = s.OrderByDescending(t => t.SerNo).First() }).Select(z => new SelectListItem
          {
              Text = z.Car_List.Head_No,
              Value = z.Car_List.Head_No
          }).ToList();
            //尾車
            var CleCarPlate_No = _CleWasteCarService.GetCleWasteCar().GroupBy(t => t.Plate_No)
          .Select(s => new { Car_List = s.OrderByDescending(t => t.SerNo).First() }).Select(z => new SelectListItem
          {
              Text = z.Car_List.Plate_No,
              Value = z.Car_List.Plate_No
          }).ToList();
            //尾車塞入
            CleCar.AddRange(CleCarPlate_No);
            ViewBag.CleCar = CleCar;
            #endregion
        }

    }
}
