using System;
//using PSTFrame.Data.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LZAI.MgrLib.Model.DB
{
    [Table("MgrUserGroup")]
    public class MgrUserGroup
    {
        [Key]
        [Column("UserGrpId")]
        [DisplayName("使用者群組功能代碼")]
        [Required]
        public int UserGrpId { get; set; }

        [Column("UserId")]
        [DisplayName("使用者代碼")]
        [Required]
        public decimal UserId { get; set; }

        [Column("GroupId")]
        [DisplayName("群組功能代碼")]
        [Required]
        public string GroupId { get; set; }

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
    }



}
