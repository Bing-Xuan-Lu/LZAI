using LZAI.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LZAIMonitor.Areas.Cle.Models.GrantInfo
{
    public class GrantInfoDocModel
    {
        public List<int> CarSelect { get; set; }
       

        public CleGrantInfo  GetCleGrantInfo{ get; set; }
        public GrantInfoDocModel()
        {
            GetCleGrantInfo = new CleGrantInfo();
            CarSelect = new List<int>();
           
        }
    }
}