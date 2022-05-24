using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LZAI.Model.DB;
using LZAI.Repository.IRepository;
using PSTFrame.Data;
using PSTFrame.Data.Dapper;

namespace LZAI.Repository.Repository
{

    public class Stop_Region_LocationRepository : BaseRepository<Stop_Region_Location, int>, IStop_Region_LocationRepository
    {
        public Stop_Region_LocationRepository(IRepositoryContext context) : base(context)
        {

        }

        /// <summary>
        /// 新增警示區展圖用點位含TWD97轉換
        /// </summary>
        /// <param name="locations"></param>
        /// <returns></returns>
        public int InsertStop_Region_LocationAndTM97XY(List<Stop_Region_Location> locations)
        {
            string SQLStr = @"INSERT INTO [dbo].[Stop_Region_Location]
           ([ID]
           ,[WGS_LON]
           ,[WGS_LAT]
           ,[TM2X]
           ,[TM2Y])
     VALUES
           (@ID
           ,@WGS_LON
           ,@WGS_LAT
           ,dbo.fn_gps_tm97x(@WGS_LAT,@WGS_LON)
           ,dbo.fn_gps_tm97y(@WGS_LAT,@WGS_LON))";
            int result = Conn.Execute(SQLStr, locations, Context.Tran);
            return result;
        }

        /// <summary>
        /// 新增警示區地理資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int sp_Ins_Stop_Region_Geometry(int id)
        {
            int result = Conn.Execute("sp_Ins_Stop_Region_Geometry", new { @ID = id }
                , commandType: CommandType.StoredProcedure);
            return result;
        }

        /// <summary>
        /// 刪除警示區點位圖資資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteRegionLocationAndGeometry(int id)
        {
            string SQLdelete = "\r\n";
            SQLdelete += " Delete from dbo.Stop_Region_Location Where ID= @ID\r\n";
            SQLdelete += " Delete from dbo.Stop_Region_Geometry Where ID= @ID\r\n";
            int result = Conn.Execute(SQLdelete, new {@ID = id});
            return result;
        }
    }
}
