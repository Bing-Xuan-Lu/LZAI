using System;
using System.Collections.Generic;
using System.Data;
using LZAI.MgrLib.Repository;
using LZAI.MgrLib.Model.DB;
using PSTFrame.Data;
using PSTFrame.MVC.Model;

namespace LZAI.MgrLib.Service
{
    public class MgrFuncTypeService : BaseService<MgrFuncType, int>
    {
        MgrFuncTypeRepository MgrFuncTypeRepo;
        private IRepositoryContext DbContext;
        public MgrFuncTypeService(IRepositoryContext context)
        {
            DbContext = context;
            MgrFuncTypeRepo = new MgrFuncTypeRepository(context);
        }
    }
}
