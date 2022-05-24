using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LZAI.Model.DB;

namespace LZAIMonitor.Models
{
    [Serializable]
    public class TrackViewModel
    {
        public VwCleWasteCar WasteCarInfo { get; set; } = new VwCleWasteCar();

        /// <summary>
        /// 歷史軌跡RowData
        /// </summary>
        public object HistoryJsonData { get; set; }

        /// <summary>
        /// 歷史軌跡WKT
        /// </summary>
        public string WktLineString { get; set; }
    }
}