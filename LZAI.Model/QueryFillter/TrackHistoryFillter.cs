using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZAI.Model.QueryFillter
{
    /// <summary>
    /// 歷史軌跡查詢條件
    /// </summary>
    public class TrackHistoryFillter
    {
        [DisplayName("運送車號")]
        public string PlateNo { get; set; }
        [DisplayName("運送日期")]
        public string CleDate { get; set; }
        [DisplayName("運送時間(起)")]
        public string StartTime { get; set; }
        [DisplayName("運送時間(迄)")]
        public string EndTime { get; set; }
    }
}
