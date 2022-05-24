using System;
//using PSTFrame.Data.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PSTFrame.MVC.Model.DataAnnotations;

namespace LZAI.MgrLib.Model.DB
{
    [Table("MgrFunc")]
    public class MgrFunc
    {
        [Key]
        [Column("FuncId")]
        [DisplayName("功能代碼")]
        public int FuncId { get; set; }

        [Column("FuncName")]
        [DisplayName("功能名稱")]
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "功能名稱不可超過50字")]
        public string FuncName { get; set; }

        [Column("FuncMemo")]
        [DisplayName("FuncMemo")]
        [StringLength(200, ErrorMessage = "FuncMemo不可超過200字")]
        public string FuncMemo { get; set; }

        [Column("FuncTypeId")]
        [DisplayName("主項")]
        public int FuncTypeId { get; set; }

        [Column("IsAddFunc")]
        [DisplayName("是否新增功能")]
        [Required]
        //"1:有功能 0:無功能"
        public bool IsAddFunc { get; set; }

        [Column("IsModFunc")]
        [DisplayName("是否修改功能")]
        [Required]
        //"1:有功能 0:無功能""
        public bool IsModFunc { get; set; }

        [Column("IsCanFunc")]
        [DisplayName("是否刪除功能")]
        [Required]
        //"1:有功能 0:無功能"
        public bool IsCanFunc { get; set; }

        [Column("IsQueFunc")]
        [DisplayName("是否查詢功能")]
        [Required]
        //"1:有功能 0:無功能"
        public bool IsQueFunc { get; set; }

        [Column("IsOthPerm1")]
        [DisplayName("其它權限1")]
        [Required]
        //0:無 1:有 則需要填寫OthPerm1Name欄位值
        public bool IsOthPerm1 { get; set; }

        [Column("OthPerm1Name")]
        [DisplayName("其它權限1_名稱")]
        [StringLength(50, ErrorMessage = "其它權限1_名稱不可超過50字")]
        public string OthPerm1Name { get; set; }

        [Column("IsOthPerm2")]
        [DisplayName("其它權限2")]
        [Required]
        //0:無 1:有 則需要填寫OthPerm2Name欄位值
        public bool IsOthPerm2 { get; set; }

        [Column("OthPerm2Name")]
        [DisplayName("其它權限2_名稱")]
        [StringLength(50, ErrorMessage = "其它權限2_名稱不可超過50字")]
        public string OthPerm2Name { get; set; }

        [Column("IsOthPerm3")]
        [DisplayName("其它權限3")]
        [Required]
        //0:無 1:有 則需要填寫OthPerm3Name欄位值
        public bool IsOthPerm3 { get; set; }

        [Column("OthPerm3Name")]
        [DisplayName("其它權限3_名稱")]
        [StringLength(50, ErrorMessage = "其它權限3_名稱不可超過50字")]
        public string OthPerm3Name { get; set; }

        [Column("IsOthPerm4")]
        [DisplayName("其它權限4")]
        [Required]
        //0:無 1:有 則需要填寫OthPerm4Name欄位值
        public bool IsOthPerm4 { get; set; }

        [Column("OthPerm4Name")]
        [DisplayName("其它權限4_名稱")]
        [StringLength(50, ErrorMessage = "其它權限4_名稱不可超過50字")]
        public string OthPerm4Name { get; set; }

        [Column("IsOthPerm5")]
        [DisplayName("其它權限5")]
        [Required]
        public bool IsOthPerm5 { get; set; }

        [Column("OthPerm5Name")]
        [DisplayName("其它權限5_名稱")]
        [StringLength(50, ErrorMessage = "其它權限5_名稱不可超過50字")]
        public string OthPerm5Name { get; set; }

        [Column("InsertDateTime")]
        [Updatable(true,false)]
        [DisplayName("建立日期")]
        [Required]
        public DateTime InsertDateTime { get; set; }

        [Column("InsertUnitId")]
        [Updatable(true,false)]
        [DisplayName("建立單位")]
        [Required]
        public int InsertUnitId { get; set; }

        [Column("InsertUserId")]
        [Updatable(true,false)]
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
        [DisplayName("是否已刪除")]
        [Required]
        public bool IsDelete { get; set; }
    }

}
