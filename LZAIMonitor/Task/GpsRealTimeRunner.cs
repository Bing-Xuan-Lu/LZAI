using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire;
using LZAI.Model.DB;
using LZAI.Service.IService;
using Newtonsoft.Json.Linq;
using NLog;

namespace LZAIMonitor.Task
{
    /// <summary>
    /// 即時軌跡排程
    /// </summary>
    public class GpsRealTimeRunner
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        protected readonly IGPSDataService GpsDataService;

        public GpsRealTimeRunner(IGPSDataService gpsDataService)
        {
            GpsDataService = gpsDataService;
        }

        /// <summary>
        /// 每分鐘介接事業廢棄物即時軌跡
        /// </summary>
        internal static void RunRealTime()
        {
            RecurringJob.AddOrUpdate<GpsRealTimeRunner>(
                x => x.PerformRealTimeJob()
                ,Cron.Minutely());
        }

        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public void PerformRealTimeJob()
        {
            var wasteRealTimeData = GpsDataService.GetCarRealTimeFromWasteGps();
            var data = !string.IsNullOrWhiteSpace(wasteRealTimeData)
                ? JArray.Parse(wasteRealTimeData)
                : null ;
            if (data != null)
            {
                var realTimeList = data.ToObject<List<gps_realtime>>().AsQueryable();
                var realTimeLocalList = GpsDataService.GetRealTimeAllCars().AsQueryable();
                //找到就修改，找不到就新增
                var prepareUpdateData = realTimeList.Join(realTimeLocalList,
                    m => m.Plate_no,
                    n => n.Plate_no,
                    (m, n) => m);

                foreach (var gpsRealtime in prepareUpdateData)
                {
                    GpsDataService.Update_gps_realtime(gpsRealtime);
                }

                var prepareInsertData = realTimeList.GroupJoin(realTimeLocalList,
                    m => m.Plate_no, n => n.Plate_no,
                    (m, n) => new
                    {
                        Left = m,
                        Right = n
                    }).SelectMany(x =>
                        x.Right.DefaultIfEmpty(),
                    (l, r) =>
                        new { l.Left, Right = r }
                ).Where(x => x.Right == null)
                    .Select(r => r.Left);

                foreach (var gpsRealtime in prepareInsertData.ToList())
                {
                    GpsDataService.Insert_gps_realtime(gpsRealtime);
                }
            }
        }
    }
}