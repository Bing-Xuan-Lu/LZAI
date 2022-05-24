using System.Data;
using PSTFrame.Data.Helepr;
using System.Data.SqlClient;
using System;
using LZAI.MgrLib.Model.DB;
using PSTFrame.Data.Dapper;
using PSTFrame.Data;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace LZAI.MgrLib.Repository
{
    internal class VwMgrUsersRepository : DapperRepository<Model.DB.VwMgrUsers,Int64>
    {
        public VwMgrUsersRepository(IRepositoryContext context) : base(context)
        {

        }

        /// <summary>
        /// 查詢 人員
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        internal IEnumerable<Model.DB.VwMgrUsers> GetUserByCity(int CityId = 0)
        {
            string SQLStr = "SELECT * \r\n";
            SQLStr += "  FROM VwMgrUsers\r\n";
            if (CityId != 0)
                SQLStr += " WHERE CityId = @CityId and LoginPermission = 'CityId'\r\n";
            else
                SQLStr += " WHERE LoginPermission = 'WRA'";

            return Conn.Query<VwMgrUsers>(SQLStr, new { @CityId = CityId });
        }
    }
}
