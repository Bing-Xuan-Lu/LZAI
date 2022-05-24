using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using LZAI.MgrLib.Model.DB;

namespace LZAIMonitor.Areas.Mgr.Models
{
    public class GroupViewModel
    {
        /// <summary>
        /// 查詢條件
        /// </summary>
        public Filter QueryFilter { get; set; }

        /// <summary>
        /// 查詢結果
        /// </summary>
        public List<MgrGroup> DataList { get; set; }

        public GroupViewModel()
        {
            QueryFilter = new Filter();
        }

        public class Filter
        {
            [DisplayName("群組名稱")]
            public string QueryGroupName { get; set; }
        }

    }
}