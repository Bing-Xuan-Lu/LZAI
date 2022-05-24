using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LZAI.Model.DB;

namespace LZAIMonitor.Models
{
    public class CarWatchViewModel
    {
        public CarWatchEvent CarWatchEvent { get; set; }
        public IEnumerable<CleGrantInfo> CarWatchCleGrantInfos { get; set; }
        public IEnumerable<Stop_Region_File> CarWatchStopRegionFiles { get; set; }
        public int Status { get; set; } = 0;
    }
}