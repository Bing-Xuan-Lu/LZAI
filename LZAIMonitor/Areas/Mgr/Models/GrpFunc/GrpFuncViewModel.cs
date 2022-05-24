using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LZAI.MgrLib.Model.DB;

namespace LZAIMonitor.Areas.Mgr.Models
{
    public class GrpFuncViewModel
    {
        /// <summary>
        /// 查詢條件
        /// </summary>
        public Filter QueryFilter { get; set; }

        /// <summary>
        /// 查詢結果
        /// </summary>
        public List<VwMgrGrpFunc> DataList { get; set; }

        public GrpFuncViewModel()
        {
            QueryFilter = new Filter();
            DataList = new List<VwMgrGrpFunc>();
        }

        public class Filter
        {
            public int GroupId { get; set; }
        }

    }
}