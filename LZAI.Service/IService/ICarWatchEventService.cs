using LZAI.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSTFrame.MVC;

namespace LZAI.Service.IService
{
    public interface ICarWatchEventService
    {
        #region 前台車牌辨識
        CarWatchEvent GetLastCarWatchEvent(int lane);
        CarWatchEvent GetHisTroyCarWatchEvent(int eventId);
        IEnumerable<CleGrantInfo> CalcCleGrantInfos(int eventId, string json);
        IEnumerable<Stop_Region_File> CalcStopRegions(int eventId, string json);
        IEnumerable<CleGrantInfo> GetCarWatchCleGrantInfos(int eventId);
        IEnumerable<Stop_Region_File> GetCarWatchStopRegions(int eventId);
        int GetStatus(CarWatchEvent carWatchEvent);
        string UpdateEvent(CarWatchEvent carWatchEvent);
        LPRLog GetLastCarImg();
        MessageModel InsCarWatchEvent(CarWatchEvent carWatchEvent);
        #endregion
        #region 車牌辨識紀錄

        /// <summary>
        /// 查詢條件
        /// </summary>
        /// <param name="IsSetGPS"></param>
        /// <param name="SDate"></param>
        /// <param name="EDate"></param>
        /// <param name="CARNum"></param>
        /// <param name="Fac_No"></param>
        /// <param name="Fac_Name"></param>
        /// <returns></returns>
        IEnumerable<Vw_CarWatchEvent> GetSelectVwCarWatchEvent(string IsSetGPS, string SDate, string EDate, string CARNum, string Fac_No, string Fac_Name, string RegionId);

        /// <summary>
        /// 清除機構&車
        /// </summary>
        /// <returns></returns>
        IEnumerable<CleWasteCar> GetCleWasteCar();
        /// <summary>
        /// 警示區下拉
        /// </summary>
        /// <returns></returns>
        IEnumerable<Stop_Region_File> GetStop_Region();
        #endregion
    }
}
