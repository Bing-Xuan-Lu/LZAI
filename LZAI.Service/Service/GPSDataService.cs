using LZAI.Model.DB;
using LZAI.Repository.IRepository;
using LZAI.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LZAI.Model.QueryFillter;
using LZAI.Service.LZGPS;
using PSTFrame.MVC;

namespace LZAI.Service.Service
{
    /// <summary>
    /// GPS軌跡取得服務
    /// </summary>
    public class GpsDataService : IGPSDataService
    {
        protected readonly IBaseRepository<Vw_gps_realtime, int> Vw_GpsRealTimeRepo;
        protected readonly IBaseRepository<gps_realtime, int> GpsRealTimeRepo;
        protected readonly IBaseRepository<VwCleWasteCar, int> VwCleWasteCarRepo;
        protected readonly IBaseRepository<gps_history, int> GpsHistoryRepo;
        public GpsDataService(IBaseRepository<gps_realtime, int> gpsRealTimeRepo
            , IBaseRepository<VwCleWasteCar, int> vwCleWasteCarRepo
            , IBaseRepository<gps_history, int> gpsHistoryRepo
            , IBaseRepository<Vw_gps_realtime, int> vwGpsRealTimeRepo)
        {
            GpsRealTimeRepo = gpsRealTimeRepo;
            VwCleWasteCarRepo = vwCleWasteCarRepo;
            GpsHistoryRepo = gpsHistoryRepo;
            Vw_GpsRealTimeRepo = vwGpsRealTimeRepo;
        }

        public GpsDataService(IBaseRepository<Vw_gps_realtime, int> vwGpsRealTimeRepo)
        {
            Vw_GpsRealTimeRepo = vwGpsRealTimeRepo;
            //Unit Test Use
        }

        /// <summary>
        /// 取得車頭所有對應車尾的即時軌跡
        /// </summary>
        /// <param name="plateNo"></param>
        /// <returns></returns>
        public IEnumerable<Vw_gps_realtime> GetRealTimeCars(string plateNo)
        {
            var realTimeList = Vw_GpsRealTimeRepo.GetList("where CleHead_no=@Plate_no"
                , new { @Plate_no = plateNo });
            return realTimeList;
        }

        /// <summary>
        /// 取得所有即時軌跡
        /// </summary>
        /// <param name="plateNo"></param>
        /// <returns></returns>
        public IEnumerable<gps_realtime> GetRealTimeAllCars()
        {
            var realTimeList = GpsRealTimeRepo.GetList();
            return realTimeList;
        }

        /// <summary>
        /// 取得單一台車輛
        /// </summary>
        /// <param name="queryFillter"></param>
        /// <returns></returns>
        public VwCleWasteCar GetCleWasteCar(TrackHistoryFillter queryFillter)
        {
            var wasteCar = VwCleWasteCarRepo.GetList("where  case CarMachine when 1 then Head_No when 2 then Plate_No end = @Plate_No "
                , new { @Plate_no = queryFillter.PlateNo })
                .FirstOrDefault();
            return wasteCar;
        }

        /// <summary>
        /// 取得宜蘭車輛
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VwCleWasteCar> GetCleWasteCar()
        {
            var wasteCar = VwCleWasteCarRepo.GetList();
            return wasteCar;
        }

        /// <summary>
        /// 找哪一台車靠近利澤，自動映射頭尾車資訊
        /// </summary>
        /// <param name="plate_no"></param>
        /// <returns></returns>
        public VwCleWasteCar FindCarNearToLizer(string plate_no)
        {
            var datas = VwCleWasteCarRepo.ExecNonQuery("select * from dbo.fn_FindCarNearToLizer(@plate_no)",new {@plate_no = plate_no });
            return datas.FirstOrDefault();
        }

        /// <summary>
        /// 介接事廢即時軌跡API取得Json
        /// </summary>
        /// <returns></returns>
        public string GetCarRealTimeFromWasteGps()
        {
            LZGPSDataServiceSoapClient soapClient = new LZGPSDataServiceSoapClient();
            string msg = "";
            var response = soapClient.GetRealTimeGPS("", "PsTcom", out msg);
            if (msg != "呼叫成功")
                response = "";
            return response;
        }

        /// <summary>
        /// 新增即時軌跡
        /// </summary>
        /// <param name="realtime"></param>
        /// <returns></returns>
        public int Insert_gps_realtime(gps_realtime realtime)
        {
            return GpsRealTimeRepo.Insert(realtime);
        }

        /// <summary>
        /// 修改即時軌跡
        /// </summary>
        /// <param name="realtime"></param>
        /// <returns></returns>
        public int Update_gps_realtime(gps_realtime realtime)
        {
            return GpsRealTimeRepo.Update(realtime);
        }

        /// <summary>
        /// 介接事廢歷史軌跡API取得Json
        /// </summary>
        /// <param name="queryFillter"></param>
        /// <returns></returns>
        public string GetCarHistoryFromWasteGps(TrackHistoryFillter queryFillter)
        {
            LZGPSDataServiceSoapClient soapClient = new LZGPSDataServiceSoapClient();
            string msg = "";
            var response = soapClient.GetCarGPS(queryFillter.PlateNo, queryFillter.CleDate, "00:00", "23:59", "", "PsTcom",out msg);
            if (msg != "呼叫成功")
                response = "";
            return response;
        }

        /// <summary>
        /// 取得多日歷史軌跡資料(WKT版)
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public string GetCarLargeHistory(string dateStart, string dateEnd)
        {
            //TODO:毛線球功能
            //TODO:前面補上介接事廢系統API
            var carHistoryWkt = GpsHistoryRepo.ExecStoredProcedure("sp_GPSHistoryToWKT", new { @json = "" });
            return carHistoryWkt;
        }
    }
}
