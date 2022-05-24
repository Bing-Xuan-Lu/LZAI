using System.Collections.Generic;
using PSTFrame.Data;

namespace LZAI.Repository.IRepository
{
    public interface IBaseRepository<TE, TK> : IRepository<TE, TK> where TE : class
    {
        int? InsertOfKeyInt(TE e);
        string ExecStoredProcedure(string sp_name, object param);
        IEnumerable<TE> ExecStoredProcedureReturnList(string sp_name, object param);
        IEnumerable<TE> ExecNonQuery(string sqlstr, object param);
    }
}