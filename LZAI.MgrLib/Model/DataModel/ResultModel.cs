using Dapper;
using System;
//using PSTFrame.Data.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LZAI.MgrLib.Model.DataModel
{
    //接SP回傳資料
    public class ResultModel
    {
        public bool Status { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; }

    }


}
