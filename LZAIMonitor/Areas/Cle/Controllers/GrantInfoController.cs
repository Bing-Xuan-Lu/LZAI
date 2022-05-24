using LZAI.Model.DB;
using LZAI.Service.IService;
using LZAIMonitor.Areas.Cle.Models.GrantInfo;
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
    public class GrantInfoController : WebBaseController
    {
        protected ICleGrantInfoService _IGrantInfoService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ICleWasteCarService"></param>
        public GrantInfoController(ICleGrantInfoService IGrantInfoService)
        {
            _IGrantInfoService = IGrantInfoService;
        }
        /// <summary>
        /// 事業列表
        /// </summary>
        /// <returns></returns>
        [AuditEventFilter(DataType.CleGrantInfo, DataProcessType.Select)]
        public ActionResult GrantInfo10100()
        {
            GrantInfoViewModel grantInfoViewModel = new GrantInfoViewModel();
            SetSelectListToViewBag();
            return View(grantInfoViewModel);
        }
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="Filter">The query filter.</param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult Query(GrantInfoViewModel.Filter Filter)
        {
            GrantInfoViewModel model = new GrantInfoViewModel();

            var datas = _IGrantInfoService.SerachCleGrantInfo(Filter.Fac_Name, Filter.Fac_Id, Filter.SelectCleNumber);
            model.Vw_DataList = Filter.Length > 0
             ? datas.Skip(Filter.Start).Take(Filter.Length).ToList()
             : datas.ToList();
            Filter.TotalCount = datas.Count();
            model.QueryFilter = Filter;
            return DataTablesJson(model.QueryFilter.Draw, model.QueryFilter.TotalCount,
                model.QueryFilter.TotalCount, model.Vw_DataList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GrantInfo201A0()
        {
            GrantInfoDocModel model = new GrantInfoDocModel();
            SetSelectListToViewBag();
            return View(model);
        }

        /// <summary>
        /// 編輯列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GrantInfo201E0(int CleId)
        {
            GrantInfoDocModel model = new GrantInfoDocModel();
            SetSelectListToViewBag();
            model.GetCleGrantInfo = _IGrantInfoService.GetById(CleId);
            return View(model);
        }


        #region 新增
        /// <summary>
        /// 新增功能
        /// </summary>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.CleGrantInfo, DataProcessType.Insert)]
        public ActionResult Insert(GrantInfoDocModel cleGrantInfo)
        {

            if (!ModelState.IsValid)
            {
                return Json(GetModelError());
            }


            cleGrantInfo.GetCleGrantInfo.InsertDateTime = DateTime.Now;
            cleGrantInfo.GetCleGrantInfo.InsertUnitId = LoginUser.UnitId;
            cleGrantInfo.GetCleGrantInfo.InsertUserId = LoginUser.UserId;
            cleGrantInfo.GetCleGrantInfo.UpdateDateTime = DateTime.Now;
            cleGrantInfo.GetCleGrantInfo.UpdateUnitId = LoginUser.UnitId;
            cleGrantInfo.GetCleGrantInfo.UpdateUserId = LoginUser.UserId;

            ResultMessage = _IGrantInfoService.InsertData(cleGrantInfo.GetCleGrantInfo, cleGrantInfo.CarSelect);
            dataKey = cleGrantInfo.GetCleGrantInfo.CleId;
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
        [AuditEventFilter(DataType.CleGrantInfo, DataProcessType.Update)]
        public ActionResult Update(GrantInfoDocModel cleGrantInfo)
        {
            if (!ModelState.IsValid)
            {
                return Json(GetModelError());
            }
            cleGrantInfo.GetCleGrantInfo.UpdateDateTime = DateTime.Now;
            cleGrantInfo.GetCleGrantInfo.UpdateUnitId = LoginUser.UnitId;
            cleGrantInfo.GetCleGrantInfo.UpdateUserId = LoginUser.UserId;
            dataKey = cleGrantInfo.GetCleGrantInfo.CleId;
            ResultMessage = _IGrantInfoService.UpdateData(cleGrantInfo.GetCleGrantInfo, cleGrantInfo.CarSelect);
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
        /// <param name="CleId"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.CleGrantInfo, DataProcessType.Delete)]
        public ActionResult Delete(int CleId)
        {
            dataKey = CleId;
            var ResultMessage = _IGrantInfoService.DeleteData(CleId);
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
        public ActionResult Download(GrantInfoViewModel.Filter Filter)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            MessageModel ResultMessage = new MessageModel();
            using (var package = new ExcelPackage())
            {
                try
                {
                    var data = _IGrantInfoService.SerachCleGrantInfo(Filter.Fac_Name, Filter.Fac_Id, Filter.SelectCleNumber).Select(x =>
                       new GrantInfo_download()
                       {
                           CleNumber = x.CleNumber,
                           Cle_No = x.Cle_No,
                           Cle_Name = x.Cle_Name,
                           CityName = x.CityName,
                           DistrictName = x.DistrictName,
                           Addr = x.Addr,
                           Gate_Long = x.Gate_Long,
                           Gate_Lat = x.Gate_Lat,
                           CarSelectGrantInfoCH = x.CarSelectGrantInfoCH,
                       }); 

                    var worksheet = package.Workbook.Worksheets.Add("事業單位管理");
                    worksheet.Cells["A1"].LoadFromCollection(data, true);
                    worksheet.Cells.AutoFitColumns();
                    var ms = new System.IO.MemoryStream(package.GetAsByteArray());
                    var arrBites = ms.ToArray();
                    //檔案產生path
                    string path = Server.MapPath("~/Export/Excel/GrantInfo");
                    string fileName = $"事業單位管理_{DateTime.Now.ToString("yyyyMMddhhmm")}.xlsx";
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
                    ResultMessage.FileDownload.Add(new FileUrlModel { FileName = fileName, FileUrl = Url.Content($"~/Export/Excel/GrantInfo/{fileName}") });
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
            #region 清運車車輛下拉
            var Car = _IGrantInfoService.GetCleWasteCar().Select(x =>
                  new SelectListItem
                  {
                      Text = "車頭[" + x.Head_No + "];" + (x.Plate_No == "" ? "車尾[空]" : "車尾[" + x.Plate_No + "]") + " / " + x.Fac_No + x.Fac_Name,
                      Value = x.SerNo.ToString()
                  }).ToList();
            ViewBag.SelectCar = Car;
            #endregion
            #region 編號下拉
            var CleNumber = _IGrantInfoService.GetCleGrantInfo().Select(x =>
                 new SelectListItem
                 {
                     Text = x.CleNumber.ToString(),
                     Value = x.CleNumber.ToString()
                 }).ToList();
            ViewBag.SelectCleNumber = CleNumber;
            #endregion
            #region 事業單位名稱下拉
            var Cle_Name = _IGrantInfoService.GetCleGrantInfo().Where(x => x.Cle_Name != null).Select(x =>
                 new SelectListItem
                 {
                     Text = x.Cle_Name,
                     Value = x.Cle_Name.ToString()
                 }).ToList();
            ViewBag.SelectCle_Name = Cle_Name;
            #endregion
            #region 管制編號
            var Cle_No = _IGrantInfoService.GetCleGrantInfo().Where(x => x.Cle_No != null).Select(x =>
                 new SelectListItem
                 {
                     Text = x.Cle_No,
                     Value = x.Cle_No.ToString()
                 }).ToList();
            ViewBag.SelectCle_No = Cle_No;
            #endregion
            #region 縣市下拉
            var City = _IGrantInfoService.GetAdd().GroupBy(t => t.CityName)
          .Select(x => new { LastTransaction = x.OrderByDescending(t => t.CityId).First(), })
            .Select(x =>
                 new SelectListItem
                 {
                     Text = x.LastTransaction.CityName,
                     Value = x.LastTransaction.CityId.ToString()
                 }).ToList();
            ViewBag.SelectCity = City;
            #endregion
            #region 行政區下拉
            var District = _IGrantInfoService.GetAdd().Select(x =>
                 new SelectListItem
                 {
                     Text = x.DistrictName,
                     Value = x.DistrictId.ToString()
                 }).ToList();
            ViewBag.SelectDistrict = District;
            #endregion
        }


        /// <summary>
        /// 運送車輛編號選單-編輯頁
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCarSelectedList(string CleId)
        {
            var result = _IGrantInfoService.GetById(int.Parse(CleId));
            var CarSplitResult = result.CarSelectGrantInfo != null ? result.CarSelectGrantInfo.Split(',') : null;
            return Json(CarSplitResult);
        }

        /// <summary>
        /// 區下拉單
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSelectedDistrictList(int CityId)
        {
            var result = _IGrantInfoService.GetSelectedDistrictList(CityId);

            return Json(result);
        }
    }
}