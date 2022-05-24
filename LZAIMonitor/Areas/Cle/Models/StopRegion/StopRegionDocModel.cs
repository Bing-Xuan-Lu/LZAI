using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LZAI.Model.DB;

namespace LZAIMonitor.Areas.Cle.Models.StopRegion
{
    /// <summary>
    /// 警示區資訊
    /// </summary>
    public class StopRegionDocModel
    {
        public string RegionPoints { get; set; }
        public string RegionWKT { get; set; }
        /// <summary>
        /// 警示區主檔
        /// </summary>
        public Stop_Region_File StopRegionFile { get; set; } = new Stop_Region_File();
    }
}