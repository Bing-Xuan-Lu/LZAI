using System;
//using PSTFrame.Data.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PSTFrame.MVC.Model.DataAnnotations;

namespace LZAI.MgrLib.Model.DB
{
    [Table("MgrGroup")]
    public class MgrGroup
    {
        [Key]
        [Column("GroupId")]
        [DisplayName("群組代碼")]
        [Updatable(false, false)]
        public int GroupId { get; set; }

        [Column("GroupName")]
        [DisplayName("群組名稱")]
        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "群組名稱不可空白和超過200字")]
        public string GroupName { get; set; }

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
        [DisplayName("是否已刪除")]
        [Required]
        public bool IsDelete { get; set; }
    }


}
