using System;
using System.Collections.Generic;
using PSTFrame.Data.Dapper;
using PSTFrame.Data;
using PSTFrame.Utility.Extensions;

namespace LZAI.MgrLib.Repository
{
    internal class VwMgrGrpFuncRepository : DapperRepository<Model.DB.VwMgrGrpFunc,int>
    {
        public VwMgrGrpFuncRepository(IRepositoryContext context) : base(context)
        {

        }
    }
}
