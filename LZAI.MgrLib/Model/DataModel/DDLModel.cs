using Dapper;
using System;
//using PSTFrame.Data.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LZAI.MgrLib.Model.DataModel
{
    public class DDLModel
    {
        public string Value { get; set; }

        public string Text { get; set; }

        public string Condition { get; set; }

    }


}
