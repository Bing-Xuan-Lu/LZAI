using LZAI.Model.DB;
using PSTFrame.MVC.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LZAIMonitor.Areas.Cle.Models.AuditEvent
{
    public class AuditEventViewModel
    {
        /// <summary>
        /// 查詢條件
        /// </summary>
        public Filter QueryFilter { get; set; }

        /// <summary>
        /// 查詢結果
        /// </summary>
        public List<Vw_AuditEvent> DataList { get; set; }

        public AuditEventViewModel()
        {
            QueryFilter = new Filter();
            DataList = new List<Vw_AuditEvent>();
        }
      
        public class Filter: DataTablesParameters
        {
           

            [DisplayName("使用者姓名")]
            public string Name { get; set; }

            [DisplayName("查詢日期")]
            public string SDate { get; set; }
            [DisplayName("結束時間")]
            public string EDate { get; set; }
        }

    }


}