using LZAI.Model.DB;
using LZAI.Repository.IRepository;
using LZAI.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSTFrame.Data;
using PSTFrame.MVC;

namespace LZAI.Service.Service
{
    public class StopRegionService : IStopRegionService
    {
        protected IBaseRepository<Stop_Region_File, int> StopRegionFileRepo;
        protected IStop_Region_LocationRepository StopRegionLocationRepo;
        protected IPublicCodeRepository PublicCodeRepo;
        protected IRepositoryContext Context;
        public StopRegionService(IBaseRepository<Stop_Region_File, int> iStopRegionFileRepo
            , IStop_Region_LocationRepository stopRegionLocationRepo
            , IPublicCodeRepository publicCodeRepo
            , IRepositoryContext context)
        {
            StopRegionFileRepo = iStopRegionFileRepo;
            StopRegionLocationRepo = stopRegionLocationRepo;
            PublicCodeRepo = publicCodeRepo;
            Context = context;
        }

        /// <summary>
        /// 取得所有警示區
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Stop_Region_File> GetStopRegionFiles()
        {
            return StopRegionFileRepo.GetList().Where(x => !x.IsDelete);
        }

        /// <summary>
        /// 取得單一警示區
        /// </summary>
        /// <returns></returns>
        public Stop_Region_File GetStopRegionFile(int id)
        {
            return StopRegionFileRepo.Get(id);
        }

        /// <summary>
        /// 取得單一警示區點位資料
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Stop_Region_Location> GetStopRegionPoints(int id)
        {
            return StopRegionLocationRepo.GetList("where ID =@ID", new { @ID = id });
        }

        /// <summary>
        /// 取得警示區分類
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PublicCode> GetRegionType()
        {
            return PublicCodeRepo.GetList("where CodeType= @CodeType", new { @CodeType = "RegionType" });
        }

        /// <summary>
        /// 新增警示區主檔
        /// </summary>
        /// <param name="stopRegionFile"></param>
        /// <param name="pointGroup"></param>
        /// <returns></returns>
        public MessageModel InsertRegion(Stop_Region_File stopRegionFile, string pointGroup)
        {
            var locations = new List<Stop_Region_Location>();
            try
            {
                Context.BeginTran();
                stopRegionFile.ID = StopRegionFileRepo.Insert(stopRegionFile);
                var wktList = pointGroup.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (wktList.Length > 0)
                {
                    locations = wktList.Select(x => new Stop_Region_Location()
                    {
                        ID = stopRegionFile.ID,
                        WGS_LON = decimal.Round(decimal.Parse(x.Split(',')[0]), 9),
                        WGS_LAT = decimal.Round(decimal.Parse(x.Split(',')[1]), 10)
                    }).ToList();
                }
                StopRegionLocationRepo.InsertStop_Region_LocationAndTM97XY(locations);
                Context.Commit();
                if (Context.Committed)
                {
                    StopRegionLocationRepo.sp_Ins_Stop_Region_Geometry(stopRegionFile.ID);
                }
                return new MessageModel()
                {
                    Status = true,
                    Message = "新增警示區成功"
                };
            }
            catch (Exception ex)
            {
                Context.Rollback();
                return new MessageModel()
                {
                    Status = false,
                    Message = "新增警示區失敗"
                };
            }

        }
        /// <summary>
        /// 刪除警示區主檔
        /// </summary>
        /// <param name="stopRegionFile"></param>
        /// <param name="pointGroup"></param>
        /// <returns></returns>
        public MessageModel DeleteRegion(Stop_Region_File stopRegionFile)
        {
            try
            {
                StopRegionFileRepo.Update(stopRegionFile);
                StopRegionLocationRepo.DeleteRegionLocationAndGeometry(stopRegionFile.ID);
                return new MessageModel()
                {
                    Status = true,
                    Message = "刪除警示區成功"
                };
            }
            catch (Exception ex)
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = "刪除警示區失敗"
                };
            }

        }

    }
}
