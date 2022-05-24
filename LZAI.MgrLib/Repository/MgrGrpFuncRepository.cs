using System;
using System.Collections.Generic;
using Dapper;
using PSTFrame.Data.Dapper;
using PSTFrame.Data;
using PSTFrame.Utility.Extensions;
using LZAI.MgrLib.Model.DB;

namespace LZAI.MgrLib.Repository
{
    class MgrGrpFuncRepository : DapperRepository<Model.DB.MgrGrpFunc, int>
    {
        public MgrGrpFuncRepository(IRepositoryContext context) : base(context)
        {

        }

        /// <summary>
        /// 取得使用者的功能權限List
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <returns></returns>
        public IEnumerable<MgrGrpFunc> GetsByUserId(decimal userId)
        {

            string whereStr = @"WHERE GroupId IN (
        SELECT mug.GroupId
        FROM MgrUserGroup mug
        WHERE mug.UserId = @UserId)";
            return this.GetList(whereStr, new { UserId = userId });
        }

        /// <summary>
        /// 取得群組該權限是否設定過
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<MgrGrpFunc> GetsByGrpandFuncId(int GroupId,int FuncId)
        {
            string whereStr = @"WHERE GroupId =  @GroupId and FuncId = @FuncId";
            return this.GetList(whereStr, new { GroupId = GroupId, FuncId = FuncId });
        }
    }
}
