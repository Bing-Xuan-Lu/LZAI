using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using PSTFrame.Data.Dapper;
using PSTFrame.Data.Schema;
using Dapper;

namespace LZAI.MgrLib.Repository
{
    internal class BaseRepository
    {

        public static string ConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["LZAI"].ToString();

        public static SqlTransaction GetTransaction()
        {
            return PSTFrame.Data.Helepr.SQLHelper.GenerateSQLTransaction(ConnStr);
        }

        static BaseRepository()
        {
            //因透過Dapper Query時，未支援依Column轉換，故需增加以下Mapping
            PSTFrame.Data.Dapper.TypeMapper.TypeMapper.Initialize("LZAI.MgrLib.Model.DB");
        }

        public static DapperContext NewContext()
        {
            return new DapperContext(ConnStr);
        }

        public static SqlConnection GetConnection()
        {
            return PSTFrame.Data.Helepr.SQLHelper.GenerateSQLConnection(ConnStr);
        }

        public static DapperContext GetContext()
        {
            return new DapperContext(ConnStr);
        }
    }
}
