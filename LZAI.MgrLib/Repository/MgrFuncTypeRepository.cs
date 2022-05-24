using System;
using System.Collections.Generic;
using PSTFrame.Data.Dapper;
using PSTFrame.Data;
using PSTFrame.Utility.Extensions;
using LZAI.MgrLib.Model.DB;
using Dapper;

namespace LZAI.MgrLib.Repository
{
    class MgrFuncTypeRepository : DapperRepository<Model.DB.MgrFuncType, int>
    {
        public MgrFuncTypeRepository(IRepositoryContext context) : base(context)
        {

        }

        /// <summary>
        /// 取得目前可使用權限清單
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Model.DB.MgrFuncType> GetFuncs()
        {
            string SQLStr = "SELECT MF.FuncID as FuncTypeId,MF.FuncName as FuncTypeName FROM MgrFunc MF WITH(NOLOCK) \r\n";
            SQLStr += " WHERE IsDelete='0' \r\n";
            SQLStr += " ORDER BY  FuncID \r\n";
            return Conn.Query<MgrFuncType>(SQLStr); ;
        }
    }
}
