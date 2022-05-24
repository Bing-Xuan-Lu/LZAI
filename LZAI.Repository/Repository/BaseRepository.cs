using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LZAI.Model;
using LZAI.Repository.IRepository;
using PSTFrame.Data;
using PSTFrame.Data.Dapper;

namespace LZAI.Repository.Repository
{
    public class BaseRepository<TE, TK> : DapperRepository<TE, TK>, IBaseRepository<TE, TK> where TE : class
    {
        private IRepositoryContext _context;
        public BaseRepository(IRepositoryContext context) : base(context)
        {
            this._context = context;
        }

        int? IBaseRepository<TE, TK>.InsertOfKeyInt(TE e)
        {
            return InsertOfKeyInt(e);
        }

        public string ExecStoredProcedure(string sp_name, object param)
        {
            return _context.Conn.ExecuteScalar(sp_name, param, commandTimeout: 60000, commandType: CommandType.StoredProcedure)?.ToString();
        }

        public IEnumerable<TE> ExecStoredProcedureReturnList(string sp_name, object param)
        {
            return _context.Conn.Query<TE>(sp_name, param, commandTimeout: 60000, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// 執行自訂資料表含式
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<TE> ExecNonQuery(string sqlstr, object param)
        {
            return _context.Conn.Query<TE>(sqlstr, param, commandTimeout: 60000, commandType: CommandType.Text);
        }

    }
}
