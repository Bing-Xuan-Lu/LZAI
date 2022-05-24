using LZAI.Model.DB;
using PSTFrame.MVC.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LZAIMonitor.Areas.Cle.Models.WasteCar
{
    public class WasteCarViewModel
    {
        /// <summary>
        /// 查詢條件
        /// </summary>
        public Filter QueryFilter { get; set; }

        /// <summary>
        /// 查詢結果
        /// </summary>
        public List<VwCleWasteCar> DataList { get; set; }

        public WasteCarViewModel()
        {
            QueryFilter = new Filter();
            DataList = new List<VwCleWasteCar>();
        }
      
        public class Filter: DataTablesParameters
        {
           

            [DisplayName("清除機構名稱")]
            public string Fac_Name { get; set; }

            [DisplayName("清除機構管編")]
            public string Fac_No { get; set; }

            [DisplayName("車號")]
            public string CARNum { get; set; }
            

                
        }

    }


}