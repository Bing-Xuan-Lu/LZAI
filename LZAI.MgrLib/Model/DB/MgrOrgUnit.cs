using System;
//using PSTFrame.Data.Schema;
//using PSTFrame.Data.Dapper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PSTFrame.Data.Dapper;
using PSTFrame.MVC.Model.DataAnnotations;

namespace LZAI.MgrLib.Model.DB
{
    [Table("MgrOrgUnit")]
    public class MgrOrgUnit
    {
        [Key]
        [Column("OrgUnitId")]
        [DisplayName("單位代碼")]
        public int OrgUnitId { get; set; }

        [Column("UnitName")]
        [DisplayName("單位名稱")]
        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "	單位名稱不可超過200字")]
        public string UnitName { get; set; }

        [Column("DispOrder")]
        [DisplayName("顯示順序")]
        public int DispOrder { get; set; }

        [Column("InsertDateTime")]
        [DisplayName("建立日期")]
        [IgnoreInsert]
        public DateTime InsertDateTime { get; set; }

        [Column("InsertUnitId")]
        [DisplayName("建立單位")]
        [IgnoreUpdate]
        public int InsertUnitId { get; set; }

        [Column("InsertUserId")]
        [DisplayName("建立人員")]
        [IgnoreUpdate]
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
        [IgnoreInsert]
        public bool IsDelete { get; set; }
    }


}
