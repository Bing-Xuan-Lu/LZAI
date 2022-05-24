using System;
using System.Collections.Generic;
using PSTFrame.Data.Dapper;
using PSTFrame.Data;
using PSTFrame.Utility.Extensions;

namespace LZAI.MgrLib.Repository
{
    internal  class LogLoginRepository : DapperRepository<Model.DB.LogLogin, int>
    {
        public LogLoginRepository(IRepositoryContext context) : base(context)
        {
        }
    }
}
