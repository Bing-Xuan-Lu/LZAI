using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LZAI.Service.IService;
using LZAIMonitor.Areas.Cle.Models.StopRegion;
using LZAIMonitor.Controllers;
using Newtonsoft.Json;
using PSTFrame.MVC.Filters;

namespace LZAIMonitor.Areas.Cle.Controllers
{
    /// <summary>
    /// 警示區
    /// </summary>
    public class StopRegionController : WebBaseController
    {
        private readonly IStopRegionService _istopRegionService;

        /// <summary>
        /// 警示區
        /// </summary>
        /// <param name="istopRegionService"></param>
        public StopRegionController(IStopRegionService istopRegionService)
        {
            _istopRegionService = istopRegionService;
        }

        /// <summary>
        /// 警示區建置
        /// </summary>
        /// <returns></returns>
        public ActionResult BuildRegion()
        {
            var model = new StopRegionViewModel();
            ViewBag.StopTypeList = _istopRegionService.GetRegionType().Select(x =>
                new SelectListItem
                {
                    Text = x.CodeNameCH,
                    Value = x.Sno.ToString()
                }).ToList(); ;
            return View(model);
        }

        /// <summary>
        /// 新增警示區
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult InsRegion(StopRegionDocModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(GetModelError());
            }

            model.StopRegionFile.InsertUnitId = LoginUser.UnitId;
            model.StopRegionFile.InsertUserId = LoginUser.UserId;
            model.StopRegionFile.UpdateUnitId = LoginUser.UnitId;
            model.StopRegionFile.UpdateUserId = LoginUser.UserId;
            ResultMessage = _istopRegionService.InsertRegion(model.StopRegionFile, model.RegionPoints);
            return Json(ResultMessage);
        }

        /// <summary>
        /// 刪除警示區
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult DelRegion(string stopRegionId)
        {
            var StopRegionFile = _istopRegionService.GetStopRegionFile(int.Parse(stopRegionId));
            StopRegionFile.UpdateDateTime = DateTime.Now;
            StopRegionFile.UpdateUnitId = LoginUser.UnitId;
            StopRegionFile.UpdateUserId = LoginUser.UserId;
            StopRegionFile.IsDelete = true;
            ResultMessage = _istopRegionService.DeleteRegion(StopRegionFile);
            return Json(ResultMessage);
        }

        /// <summary>
        /// 取得警示區資訊
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult Get_Stop_Region_Location(string stopRegionId)
        {
            var data = _istopRegionService.GetStopRegionPoints(int.Parse(stopRegionId));
            return Json(data);
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="Filter">The query filter.</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult Query(StopRegionViewModel Filter = null)
        {
            //TODO:暫時用前端查詢條件
            var data = _istopRegionService.GetStopRegionFiles();
            var result = new { data };
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        /// <summary>
        /// 警示區環域分析
        /// </summary>
        /// <returns></returns>
        public ActionResult AnalysisRegion()
        {
            //TODO:石頭：暫時沒有要做環域分析
            return View();
        }
    }
}