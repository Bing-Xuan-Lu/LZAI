using System;
using System.Collections.Generic;
using PSTFrame.Data.Dapper;
using PSTFrame.Data;
using PSTFrame.Utility.Extensions;
using LZAI.MgrLib.Model.DB;
using Dapper;

namespace LZAI.MgrLib.Repository
{
    class Mgr_ConfigRepository : DapperRepository<Model.DB.Mgr_Config, int>
    {
        public Mgr_ConfigRepository(IRepositoryContext context) : base(context)
        {

        }
    }
}
