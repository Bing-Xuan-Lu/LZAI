using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LZAI.Model.DB;
using LZAI.Model.QueryFillter;
using LZAI.Service.IService;
using LZAIMonitor.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Newtonsoft.Json;
using TableDependency.SqlClient.Base.Enums;

namespace LZAIMonitor.Hubs
{
    /// <summary>
    /// 
    /// </summary>
    public class CarWatchHubHelper : ICarWatchHubHelper
    {
        protected readonly IGPSDataService GpsDataService;
        protected readonly ICarWatchEventService _carWatchEventService;
        protected IHubContext Context { get; } = GlobalHost.ConnectionManager.GetHubContext<CarWatchHub>();

        public CarWatchHubHelper(
            ICarWatchEventService carWatchEventService,
            IGPSDataService gpsDataService)
        {
            _carWatchEventService = carWatchEventService;
            GpsDataService = gpsDataService;
        }

        public void Hello()
        {
            Context.Clients.All.hello();
        }
        /// <summary>
        /// 傳送車牌辨識結果至圖台
        /// </summary>
        /// <param name="changeType"></param>
        /// <param name="entity"></param>
        public async System.Threading.Tasks.Task SendCarWatchEventAsync(ChangeType changeType, CarWatchEvent entity)
        {
            if (changeType == ChangeType.Insert)
            {
                Context.Clients.All.updateMessages(entity);
                if (entity.IsSetGPS)
                {
                    var gpsData = await GetCarHistroy(entity);
                    Context.Clients.All.GetGpsData(gpsData);
                    if (gpsData.HistoryJsonData != null)
                    {
                        entity.IsGPSHistory = true;
                        //向下分析事業點、警示區
                        var cleInfoResult = CalcCleGrantInfos(entity.EventId, gpsData.HistoryJsonData.ToString());
                        var stopRegionResult = CalcStopRegions(entity.EventId, gpsData.HistoryJsonData.ToString());
                        entity.IsStopRegion = await stopRegionResult;
                        entity.IsCleGrant = await cleInfoResult;
                    }
                }
                await UpdateCarStatus(entity);
            }
        }


        /// <summary>
        /// 取得歷史軌跡回饋給使用者
        /// </summary>
        /// <param name="entity"></param>
        public async Task<TrackViewModel> GetCarHistroy(CarWatchEvent entity)
        {
            TrackHistoryFillter filter = new TrackHistoryFillter();
            var nearResult = GpsDataService.FindCarNearToLizer(entity.Head_No);
            filter.PlateNo = nearResult?.Plate_No;
            filter.CleDate = entity.InsTime.ToString(CultureInfo.InvariantCulture);
            filter.StartTime = entity.LastWatchTime == null ? "00:00"
                : ((DateTime)entity.LastWatchTime).ToString("HH:mm");
            return new TrackViewModel
            {
                WasteCarInfo = GpsDataService.GetCleWasteCar(filter),
                HistoryJsonData = JsonConvert.DeserializeObject(GpsDataService.GetCarHistoryFromWasteGps(filter))
            };
        }

        public async Task<bool> CalcCleGrantInfos(int eventId, string json)
        {
            var model = _carWatchEventService.CalcCleGrantInfos(eventId, json);
            await Context.Clients.All.GetCleGrantInfo(model);
            return model.Any();
        }

        public async Task<bool> CalcStopRegions(int eventId, string json)
        {
            var model = _carWatchEventService.CalcStopRegions(eventId, json);
            await Context.Clients.All.GetStopRegion(model);
            return model.Any();
        }

        /// <summary>
        /// 計算完成，更新事件
        /// </summary>
        /// <returns></returns>
        protected async System.Threading.Tasks.Task UpdateCarStatus(CarWatchEvent carWatchEvent)
        {
            int status = _carWatchEventService.GetStatus(carWatchEvent);
            await Context.Clients.All.UpdateCarStatus(status);
            //SQLDepend會TriggerChange事件，先送訊息給前台在執行後台Update
            _carWatchEventService.UpdateEvent(carWatchEvent);
        }
    }

    public interface ICarWatchHubHelper
    {
        void Hello();
        System.Threading.Tasks.Task SendCarWatchEventAsync(ChangeType changeType, CarWatchEvent entity);
    }

}