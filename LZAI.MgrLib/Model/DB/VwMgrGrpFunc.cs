using System;
//using PSTFrame.Data.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LZAI.MgrLib.Model.DB
{
    [Table("vwMgrGrpFunc")]
    public class VwMgrGrpFunc
    {
        [Column("GrpFuncId")]
        [DisplayName("功能權限代碼")]
        public int GrpFuncId { get; set; }
        [Key]
        [Column("GroupId")]
        [DisplayName("GroupId")]
        public int GroupId { get; set; }

        [Column("FuncId")]
        [DisplayName("功能代碼")]
        public int FuncId { get; set; }

        [Column("FuncName")]
        [DisplayName("功能名稱")]
        public string FuncName { get; set; }

        [Column("PermText")]
        [DisplayName("權限")]
        public string PermText { get; set; }
    }


}
