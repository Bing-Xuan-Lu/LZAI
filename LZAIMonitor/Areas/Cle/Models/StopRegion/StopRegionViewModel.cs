using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LZAI.Model.DB;

namespace LZAIMonitor.Areas.Cle.Models.StopRegion
{
    public class StopRegionViewModel
    {
        public StopRegionViewModel()
        {
            StopRegionFileList = new List<Stop_Region_File>();
            DocModel = new StopRegionDocModel();
        }
        public IEnumerable<Stop_Region_File> StopRegionFileList { get; set; }
        public StopRegionDocModel DocModel { get; set; }
    }
}