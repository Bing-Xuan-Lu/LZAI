using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using LZAI.Model.DB;
using LZAI.Model.QueryFillter;
using LZAI.Service.IService;
using LZAI.Service.Service;
using LZAIMonitor.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PSTFrame.MVC.Filters;

namespace LZAIMonitor.Controllers
{
    /// <summary>
    /// 車牌辨識API
    /// </summary>
    [RoutePrefix("api/CarWatch")]
    public class CarWatchController : ApiController
    {
        protected readonly ICarWatchEventService _carWatchEventService;
        protected readonly IGPSDataService GpsDataService;
        protected readonly IStopRegionService _istopRegionService;

        /// <summary>
        /// 車牌辨識API
        /// </summary>
        /// <param name="carWatchEventService"></param>
        public CarWatchController(ICarWatchEventService carWatchEventService,
            IGPSDataService gpsDataService,
            IStopRegionService istopRegionService)
        {
            //TODO:因為需求違反了職責
            _carWatchEventService = carWatchEventService;
            GpsDataService = gpsDataService;
            _istopRegionService = istopRegionService;
            //_configuration = configuration;

        }

        /// <summary>
        /// 取得車牌辨識結果
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpPost]
        [Route("CarWatchEvent")]
        public IHttpActionResult GetLastCarWatchEvent(string Lane)
        {
            var model = new CarWatchViewModel();
            model.CarWatchEvent = _carWatchEventService.GetLastCarWatchEvent(int.Parse(Lane));
            model.CarWatchCleGrantInfos = _carWatchEventService.GetCarWatchCleGrantInfos(model.CarWatchEvent.EventId);
            model.CarWatchStopRegionFiles = _carWatchEventService.GetCarWatchStopRegions(model.CarWatchEvent.EventId);
            model.Status = _carWatchEventService.GetStatus(model.CarWatchEvent);
            return Json(model);
        }

        /// <summary>
        /// 取得車牌辨識結果(歷史事件)
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpPost]
        [Route("CarWatchHisTroyEvent")]
        public IHttpActionResult GetHisTroyCarWatchEvent(string eventId)
        {
            var model = new CarWatchViewModel();
            model.CarWatchEvent = _carWatchEventService.GetHisTroyCarWatchEvent(int.Parse(eventId));
            model.CarWatchCleGrantInfos = _carWatchEventService.GetCarWatchCleGrantInfos(model.CarWatchEvent.EventId);
            model.CarWatchStopRegionFiles = _carWatchEventService.GetCarWatchStopRegions(model.CarWatchEvent.EventId);
            model.Status = _carWatchEventService.GetStatus(model.CarWatchEvent);
            return Json(model);
        }

        /// <summary>
        /// 取得最近利澤的歷史軌跡
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GpsHistoryData")]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public IHttpActionResult GpsHistoryData(CarWatchEvent carWatchEvent)
        {
            TrackHistoryFillter filter = new TrackHistoryFillter();
            var nearResult = GpsDataService.FindCarNearToLizer(carWatchEvent.Head_No);
            filter.PlateNo = nearResult == null ? carWatchEvent.Head_No : nearResult.Plate_No;
            filter.CleDate = carWatchEvent.InsTime.ToString(CultureInfo.InvariantCulture);
            filter.StartTime = carWatchEvent.LastWatchTime == null ? "00:00"
                : ((DateTime)carWatchEvent.LastWatchTime).ToString("HH:mm");
            return Json(new TrackViewModel
            {
                WasteCarInfo = GpsDataService.GetCleWasteCar(filter),
                HistoryJsonData = JsonConvert.DeserializeObject(GpsDataService.GetCarHistoryFromWasteGps(filter))
            });
        }

        /// <summary>
        /// 取得警示區資訊
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStopRegionLocation")]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public IHttpActionResult Get_Stop_Region_Location([FromBody]string stopRegionId)
        {
            var data = _istopRegionService.GetStopRegionPoints(int.Parse(stopRegionId));
            return Json(data);
        }

        /// <summary>
        /// 取得最後一張車牌辨識圖片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetLastCarImg")]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public IHttpActionResult GetLastCarImg()
        {
            var lastCarImg = _carWatchEventService.GetLastCarImg();
            return Json(lastCarImg);
        }

        /// <summary>
        /// 手動新增車牌辨識事件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ManualEvent")]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public IHttpActionResult ManualEvent(CarWatchEvent carWatchEvent)
        {
            if (!ModelState.IsValid)
                return BadRequest("參數遺漏");
            var resultMessage = _carWatchEventService.InsCarWatchEvent(carWatchEvent);
            return Json(resultMessage);
        }


    }
}
