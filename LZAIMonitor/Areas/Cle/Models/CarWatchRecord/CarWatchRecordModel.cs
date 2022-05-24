using LZAI.Model.DB;
using PSTFrame.MVC.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LZAIMonitor.Areas.Cle.Models.CarWatchRecord
{
    public class CarWatchRecordModel
    {
        public List<Vw_CarWatchEvent> DataList { get; set; }
        public Filter QueryFilter { get; set; }
        public CarWatchRecordModel()
        {
            QueryFilter = new Filter();
            DataList = new List<Vw_CarWatchEvent>();
        }
        public class Filter : DataTablesParameters
        {
            [DisplayName("類別")]
            public string IsSetGPS { get; set; }
            [DisplayName("進入時間")]
            public string SDate { get; set; }
            [DisplayName("辨識結束時間")]
            public string EDate { get; set; }
          
            [DisplayName("清除機構名稱")]
            public string Fac_Name { get; set; }

            [DisplayName("清除機構管編")]
            public string Fac_No { get; set; }

            [DisplayName("車號")]
            public string CARNum { get; set; }

            [DisplayName("警示區")]
            public string RegionId { get; set; }

        }
    }
}