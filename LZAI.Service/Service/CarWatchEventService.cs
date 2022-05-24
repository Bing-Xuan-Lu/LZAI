using LZAI.Model.DB;
using LZAI.Repository.IRepository;
using LZAI.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSTFrame.MVC;

namespace LZAI.Service.Service
{
    public class CarWatchEventService : ICarWatchEventService
    {
        protected readonly IBaseRepository<CarWatchEvent, int> _iCarWatchEventRepo;
        protected readonly IBaseRepository<LPRLog, int> _iLPRLogRepo;
        protected readonly IBaseRepository<Vw_CarWatchEvent, int> _iVwCarWatchEventRepo;
        protected readonly IBaseRepository<CleWasteCar, int> _iCleWasteCarRepo;
        protected readonly IBaseRepository<CleGrantInfo, int> CleGrantInfoRepo;
        protected readonly IBaseRepository<Stop_Region_File, int> _iStop_Region_FileRepo;
        public CarWatchEventService(IBaseRepository<CarWatchEvent, int> _CarWatchEventRepo,
            IBaseRepository<Vw_CarWatchEvent, int> _VwCarWatchEventRepo,
            IBaseRepository<CleWasteCar, int> _CleWasteCarRepo,
            IBaseRepository<Stop_Region_File, int> _Stop_Region_FileRepo, IBaseRepository<CleGrantInfo, int> cleGrantInfoRepo,
            IBaseRepository<LPRLog, int> iLprLogRepo)
        {
            _iCarWatchEventRepo = _CarWatchEventRepo;
            _iVwCarWatchEventRepo = _VwCarWatchEventRepo;
            _iCleWasteCarRepo = _CleWasteCarRepo;
            _iStop_Region_FileRepo = _Stop_Region_FileRepo;
            CleGrantInfoRepo = cleGrantInfoRepo;
            _iLPRLogRepo = iLprLogRepo;
        }

        public CarWatchEvent GetLastCarWatchEvent(int lane)
        {
            var carWatchEvent = _iCarWatchEventRepo.GetList("where Lane = @Lane", new { @Lane = lane })
                .OrderByDescending(x => x.InsTime)
                .FirstOrDefault();
            return carWatchEvent;
        }

        public CarWatchEvent GetHisTroyCarWatchEvent(int eventId)
        {
            var carWatchEvent = _iCarWatchEventRepo.GetList("where EventId = @EventId", new { @EventId = eventId })
                .FirstOrDefault();
            return carWatchEvent;
        }

        /// <summary>
        /// 計算事業點
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public IEnumerable<CleGrantInfo> CalcCleGrantInfos(int eventId, string json)
        {
            return CleGrantInfoRepo.ExecStoredProcedureReturnList("sp_EnterGPSCleGrant",
                new { @eventId = eventId, @json = json });
        }

        /// <summary>
        /// 計算警示區
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public IEnumerable<Stop_Region_File> CalcStopRegions(int eventId, string json)
        {
            return _iStop_Region_FileRepo.ExecStoredProcedureReturnList("sp_EnterGPSStopRegion",
                new { @eventId = eventId, @json = json });
        }

        /// <summary>
        /// 取得歷史事件行經事業點
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public IEnumerable<CleGrantInfo> GetCarWatchCleGrantInfos(int eventId)
        {
            return CleGrantInfoRepo.ExecStoredProcedureReturnList("sp_CarWatchCleGrant",
                new { @eventId = eventId });
        }

        /// <summary>
        /// 取得歷史事件行經警示區
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public IEnumerable<Stop_Region_File> GetCarWatchStopRegions(int eventId)
        {
            return _iStop_Region_FileRepo.ExecStoredProcedureReturnList("sp_CarWatchStopRegion",
                new { @eventId = eventId });
        }

        /// <summary>
        /// 更新車牌辨識事件
        /// </summary>
        /// <param name="carWatchEvent"></param>
        /// <returns></returns>
        public string UpdateEvent(CarWatchEvent carWatchEvent)
        {
            var result = _iCarWatchEventRepo.ExecStoredProcedure("sp_UpdCarWatchEvent", new
            {
                @EventId = carWatchEvent.EventId,
                @IsSetGPS = carWatchEvent.IsSetGPS,
                @IsGPSHistory = carWatchEvent.IsGPSHistory,
                @IsCleGrant = carWatchEvent.IsCleGrant,
                @IsStopRegion = carWatchEvent.IsStopRegion,
            });
            return result;
        }

        /// <summary>
        /// 取得最後一張車牌辨識圖片
        /// </summary>
        /// <returns></returns>
        public LPRLog GetLastCarImg()
        {
            return _iLPRLogRepo.ExecStoredProcedureReturnList("sp_GetLastCarImg", new { }).FirstOrDefault();
        }

        /// <summary>
        /// 手動新增事件
        /// </summary>
        /// <returns></returns>
        public MessageModel InsCarWatchEvent(CarWatchEvent carWatchEvent)
        {
            try
            {
                carWatchEvent.IsSetGPS = true;
                carWatchEvent.IsManual = true;
                carWatchEvent.ManualInsetDateTime = DateTime.Now;
                _iCarWatchEventRepo.Insert(carWatchEvent);
                return new MessageModel()
                {
                    Status = true,
                    Message = "手動新增事件成功"
                };
            }
            catch (Exception ex)
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = "手動新增事件失敗"
                };
            }
        }

        /// <summary>
        /// 判斷該事件狀況
        /// </summary>
        /// <param name="carWatchEvent"></param>
        /// <returns></returns>
        public int GetStatus(CarWatchEvent carWatchEvent)
        {
            int status = 0;
            if (!carWatchEvent.IsSetGPS)
                status = 2;
            else if (!carWatchEvent.IsGPSHistory)
                status = 3;
            else if (carWatchEvent.IsStopRegion)
                status = 4;
            else
                status = 1;
            return status;
        }

        #region 車牌辨識紀錄

        public IEnumerable<Vw_CarWatchEvent> GetSelectVwCarWatchEvent(string IsSetGPS, string SDate, string EDate, string CARNum, string Fac_No, string Fac_Name, string RegionId)
        {
            List<string> sqlWheres = new List<string>();

            if (!string.IsNullOrWhiteSpace(IsSetGPS) & IsSetGPS != "-1")
                sqlWheres.Add("IsSetGPS =  @IsSetGPS");
            if (!string.IsNullOrWhiteSpace(SDate) && !string.IsNullOrWhiteSpace(EDate))
            {
                EDate = EDate + " 23:59:59";
                sqlWheres.Add("InsTime between @SDate and @EDate");
            }
            if (!string.IsNullOrWhiteSpace(CARNum) && CARNum != "-1")
                sqlWheres.Add("Head_No = @CARNum  or  Plate_No= @CARNum");
            if (!string.IsNullOrWhiteSpace(Fac_No) && Fac_No != "-1")
                sqlWheres.Add("Fac_No = @Fac_No");
            if (!string.IsNullOrWhiteSpace(Fac_Name) && Fac_Name != "-1")
                sqlWheres.Add("Fac_Name = @Fac_Name");
            if (!string.IsNullOrWhiteSpace(RegionId) && RegionId != "-1")
                sqlWheres.Add("RegionId like '%'+ @RegionId + '%'");
            IEnumerable<Vw_CarWatchEvent> VwcarWatchEvents = sqlWheres.Count > 0 ? _iVwCarWatchEventRepo.GetList(
               $"WHERE {string.Join(" AND ", sqlWheres)}", new
               {
                   @IsSetGPS = IsSetGPS,
                   @SDate = SDate,
                   @EDate = EDate,
                   @CARNum = CARNum,
                   @Fac_No = Fac_No,
                   @Fac_Name = Fac_Name,
                   @RegionId = RegionId
               }) : _iVwCarWatchEventRepo.GetList();


            return VwcarWatchEvents;
        }

        public IEnumerable<CleWasteCar> GetCleWasteCar()
        {
            IEnumerable<CleWasteCar> CleWasteCar;

            CleWasteCar = _iCleWasteCarRepo.GetList("WHERE IsDelete = 0 ");

            return CleWasteCar;
        }

        public IEnumerable<Stop_Region_File> GetStop_Region()
        {
            IEnumerable<Stop_Region_File> Stop_Region;

            Stop_Region = _iStop_Region_FileRepo.GetList("WHERE IsDelete = 0 ");

            return Stop_Region;
        }
        #endregion
    }
}
