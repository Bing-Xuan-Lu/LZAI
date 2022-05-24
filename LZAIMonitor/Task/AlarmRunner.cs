using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire;
using NLog;
//using TCSensor.Service;

namespace LZAIMonitor.Task
{
    public class AlarmRunner
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 執行排程
        /// </summary>
        internal static void RunAlarm()
        {
            RecurringJob.AddOrUpdate(
                () => PerformAlarmJob(),
                "*/10 * * * *");
        }

        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public static void PerformAlarmJob()
        {
            // AlarmService service = new AlarmService(logger);
            logger.Info($"開始警示功能");
            //service.DetectSensor(DateTime.Now.AddMinutes(-15));
        }
    }
}