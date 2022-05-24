using System;
//using PSTFrame.Data.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LZAI.MgrLib.Model.DB
{
    [Table("MgrGrpFunc")]
    public class MgrGrpFunc
    {
        [Key]
        [Column("GrpFuncId")]
        [DisplayName("群組功能代碼")]
        public int GrpFuncId { get; set; }

        [Column("GroupId")]
        [DisplayName("群組代碼")]
        [Required]
        public int GroupId { get; set; }

        [Column("FuncId")]
        [DisplayName("功能代碼")]
        [Required]
        public int FuncId { get; set; }

        [Column("IsExec")]
        [DisplayName("執行權限(目前無使用)")]
        public bool IsExec { get; set; }

        [Column("IsAdd")]
        [DisplayName("新增權限")]
        [Required]
        //"1:有 0:無"
        public bool IsAdd { get; set; }

        [Column("IsModify")]
        [DisplayName("修改權限")]
        [Required]
        //"1:有 0:無"
        public bool IsModify { get; set; }

        [Column("IsCancel")]
        [DisplayName("刪除權限")]
        [Required]
        //"1:有 0:無"
        public bool IsCancel { get; set; }

        [Column("IsQuery")]
        [DisplayName("查詢權限")]
        [Required]
        //"1:有 0:無"
        public bool IsQuery { get; set; }

        [Column("IsOthPerm1")]
        [DisplayName("其它權限1")]
        //"1:有 0:無"
        public bool IsOthPerm1 { get; set; }

        [Column("IsOthPerm2")]
        [DisplayName("其它權限2")]
        //"1:有 0:無"
        public bool IsOthPerm2 { get; set; }

        [Column("IsOthPerm3")]
        [DisplayName("其它權限3")]
        //"1:有 0:無"
        public bool IsOthPerm3 { get; set; }

        [Column("IsOthPerm4")]
        [DisplayName("其它權限4")]
        //"1:有 0:無"
        public bool IsOthPerm4 { get; set; }

        [Column("IsOthPerm5")]
        [DisplayName("其它權限5")]
        //"1:有 0:無"
        public bool IsOthPerm5 { get; set; }

        [Column("InsertDateTime")]
        [DisplayName("建立日期")]
        [Required]
        public DateTime InsertDateTime { get; set; }

        [Column("InsertUnitId")]
        [DisplayName("建立單位")]
        [Required]
        public int InsertUnitId { get; set; }

        [Column("InsertUserId")]
        [DisplayName("建立人員")]
        [Required]
        public decimal InsertUserId { get; set; }

        [Column("UpdateDateTime")]
        [DisplayName("更新日期")]
        [Required]
        public DateTime UpdateDateTime { get; set; }

        [Column("UpdateUnitId")]
        [DisplayName("更新單位")]
        [Required]
        public int UpdateUnitId { get; set; }

        [Column("UpdateUserId")]
        [DisplayName("更新人員")]
        [Required]
        public decimal UpdateUserId { get; set; }

        [Column("IsDelete")]
        [DisplayName("是否刪除")]
        [Required]
        public bool IsDelete { get; set; }
    }

}
