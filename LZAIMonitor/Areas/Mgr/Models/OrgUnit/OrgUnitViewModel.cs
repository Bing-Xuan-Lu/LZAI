using PSTFrame.MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LZAI.MgrLib.Model.DB;
using LZAI.MgrLib.Service;
using System.ComponentModel;

namespace LZAIMonitor.Areas.Mgr.Models.OrgUnit
{
    public class OrgUnitViewModel
    {
        public OrgUnitViewModel()
        {
            OrgUnitFilter = new Filter();
            DataList = new List<MgrOrgUnit>();
        }
        public Filter OrgUnitFilter { get; set; }
        public List<MgrOrgUnit> DataList { get; set; }

        public class Filter
        {
            [DisplayName("單位名稱")]
            public string Search { get; set; }

            [DisplayName("單位代碼")]
            public int UnitId { get; set; }
        }
    }
}