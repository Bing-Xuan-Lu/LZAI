using LZAI.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LZAI.Model.QueryFillter;

namespace LZAI.Service.IService
{
    public interface IGPSDataService
    {
        IEnumerable<Vw_gps_realtime> GetRealTimeCars(string plateNo);
        IEnumerable<gps_realtime> GetRealTimeAllCars();
        VwCleWasteCar GetCleWasteCar(TrackHistoryFillter queryFillter);
        IEnumerable<VwCleWasteCar> GetCleWasteCar();
        string GetCarHistoryFromWasteGps(TrackHistoryFillter queryFillter);
        string GetCarLargeHistory(string dateStart,string dateEnd);
        VwCleWasteCar FindCarNearToLizer(string plate_no);
        string GetCarRealTimeFromWasteGps();
        int Insert_gps_realtime(gps_realtime realtime);
        int Update_gps_realtime(gps_realtime realtime);

    }
}
