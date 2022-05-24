using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LZAI.Model.DB;
using LZAI.Model.QueryFillter;
using LZAI.Service.IService;
using LZAIMonitor.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PSTFrame.MVC.Filters;

namespace LZAIMonitor.Controllers
{
    /// <summary>
    /// 即時、歷史軌跡介接事廢API
    /// </summary>
    [RoutePrefix("api/GPSData")]
    public class GpsDataController : ApiController
    {
        protected readonly IGPSDataService GpsDataService;
        protected readonly ICleGrantInfoService CleGrantInfoService;

        public GpsDataController(IGPSDataService gpsDataService, ICleGrantInfoService cleGrantInfoService)
        {
            System.Net.ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 |
                SecurityProtocolType.Tls13 |
                SecurityProtocolType.Ssl3;
            GpsDataService = gpsDataService;
            CleGrantInfoService = cleGrantInfoService;
        }

        /// <summary>
        /// 取得即時軌跡資訊
        /// </summary>
        /// <param name="plateNo"></param>
        /// <returns></returns>
        [HttpGet, HttpPost]
        [Route("GpsRealTimeData")]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public IHttpActionResult GpsRealTimeData(string plateNo)
        {
            var result = GpsDataService.GetRealTimeCars(plateNo);
            return Json(result);
        }

        /// <summary>
        /// 取得車輛選單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCleWasteCar")]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public IHttpActionResult GetCleWasteCar()
        {
            var wasteCarList = GpsDataService.GetCleWasteCar()
                .Select(x => new
                {
                    text = $"{(!string.IsNullOrWhiteSpace(x.Head_No) ? x.Head_No : "無")}/{(!string.IsNullOrWhiteSpace(x.Plate_No) ? x.Plate_No : "無")}",
                    value = x.CarMachine == 1 ? x.Head_No : x.Plate_No
                });
            return Json(wasteCarList);
        }

        /// <summary>
        /// 取得事業縣市樹狀圖
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCleInfoTree")]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public IHttpActionResult GetCleInfoTree()
        {
            var data = CleGrantInfoService.GetVwCleGrantInfo();
            var cleGrantInfos = data
                .GroupBy(x => x.CityId,
                    (CityId, District) =>
                         new
                         {
                             id = District.First().CityName,
                             label = District.First().CityName,
                             IsCity = true,
                             children = District.GroupBy(x => x.DistrictId,
                                 (DistrictId, CleInfo) => new
                                 {
                                     id = CleInfo.First().DistrictName,
                                     label = CleInfo.First().DistrictName,
                                     parentId = CityId,
                                     IsCity = false
                                 })
                         });
            return Json(cleGrantInfos);
        }

        /// <summary>
        /// 事業展點
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowCleInfoExhibition")]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public IHttpActionResult ShowCleInfoExhibition(List<JObject> cleInfoFilter)
        {
            var cityParam = cleInfoFilter.Where(x => bool.Parse(x["IsCity"]?.ToString() ?? "false"))
                .Select(x => x["id"]?.ToString()).ToList();
            var directParam = cleInfoFilter.Where(x => !bool.Parse(x["IsCity"]?.ToString() ?? "false"))
                .Select(x => x["id"]?.ToString()).ToList();

            var cleInfoData = CleGrantInfoService.GetVwCleGrantInfo().AsQueryable();
            var cityData = cityParam.Any() ? cleInfoData.Where(x => cityParam.Contains(x.CityName)).ToList() : new List<Vw_CleGrantInfo>(); 
            var directData = directParam.Any() ? cleInfoData.Where(x => directParam.Contains(x.DistrictName)).ToList() : new List<Vw_CleGrantInfo>();
            var result = cityData.Union(directData).ToList();
            return Json(result);
        }

        /// <summary>
        /// 取得歷史軌跡資訊
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, HttpPost]
        [Route("GpsHistoryData")]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public IHttpActionResult GpsHistoryData(TrackHistoryFillter filter)
        {
            var model = new TrackViewModel
            {
                WasteCarInfo = GpsDataService.GetCleWasteCar(filter),
                HistoryJsonData = JsonConvert.DeserializeObject(GpsDataService.GetCarHistoryFromWasteGps(filter))
            };
#if DEBUG
            //model.HistoryJsonData = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/01-TP-20220419.json"));
#endif

            return Json(model);
        }

    }
}
