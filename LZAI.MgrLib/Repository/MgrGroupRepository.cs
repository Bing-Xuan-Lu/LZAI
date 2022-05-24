using System.Linq;
using System.Collections.Generic;
using PSTFrame.Data.Dapper;
using PSTFrame.Data;
using Dapper;
using LZAI.MgrLib.Model.DB;

namespace LZAI.MgrLib.Repository
{
    class MgrGroupRepository : DapperRepository<Model.DB.MgrGroup, int>
    {
        public MgrGroupRepository(IRepositoryContext context) : base(context)
        {

        }

        public IEnumerable<MgrGroup> GetList(string QueryGroupName)
        {
            if (!string.IsNullOrEmpty(QueryGroupName))
                return this.GetList("where GroupName like '%' +@GroupName + '%' and IsDelete = 0 ", new { GroupName = QueryGroupName });
            else
                return this.GetList(new { IsDelete = false });
        }

        /// <summary>
        /// 取得使用中資料清單
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Model.DB.MgrGroup> GetUseList()
        {
            return this.GetList(new {IsDelete = false});
        }

        /// <summary>
        /// Updates the delate mark.
        /// </summary>
        /// <param name="groupIds">The group ids.</param>
        /// <param name="updateUnitId">The update unit identifier.</param>
        /// <param name="updateUserId">The update user identifier.</param>
        internal void UpdateDelateMark(int[] groupIds, int updateUnitId, long updateUserId)
        {
            if (!groupIds.Any())
                return;
            string sqlStr;
            sqlStr = "Update MgrGroup ";
            sqlStr += "SET IsDelete=1, UpdateDateTime = GETDATE() ";
            sqlStr += "    , UpdateUnitId = @UPDATEUNITID, UpdateUserId = @UPDATEUSERID ";
            sqlStr += "  WHERE GroupId in @IDs";
                Context.Conn.Execute(sqlStr
                    , new { IDs = groupIds, UpdateUnitId = updateUnitId, UpdateUserId = updateUserId }
                    , Context.Tran);
        }
    }
}
