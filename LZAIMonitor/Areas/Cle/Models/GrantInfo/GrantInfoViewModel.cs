using LZAI.Model.DB;
using PSTFrame.MVC.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LZAIMonitor.Areas.Cle.Models.GrantInfo
{
    public class GrantInfoViewModel
    {
        /// <summary>
        /// 查詢條件
        /// </summary>
        public Filter QueryFilter { get; set; }

        /// <summary>
        /// 查詢結果
        /// </summary>
        public List<CleGrantInfo> DataList { get; set; }
        public List<Vw_CleGrantInfo> Vw_DataList { get; set; }

        public GrantInfoViewModel()
        {
            QueryFilter = new Filter();
            DataList = new List<CleGrantInfo>();
            Vw_DataList = new List<Vw_CleGrantInfo>();
        }
      
        public class Filter: DataTablesParameters
        {
           

            [DisplayName("事業單位名稱")]
            public string Fac_Name { get; set; }

            [DisplayName("管制編號")]
            public string Fac_Id { get; set; }

            [DisplayName("統一編號")]
            public string BusinessNumNo { get; set; }

            [DisplayName("編號")]
            public string SelectCleNumber { get; set; }
           

        }

    }


}