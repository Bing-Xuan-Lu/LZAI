using System;
//using PSTFrame.Data.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PSTFrame.MVC.Model.DataAnnotations;

namespace LZAI.MgrLib.Model.DB
{
    [Table("MgrOrgUnitAllowIP")]
    public class MgrOrgUnitAllowIP
    {
        [Key]
        [Column("SerNo")]
        [DisplayName("流水號")]
        [Required]
        public decimal SerNo { get; set; }

        [Column("OrgUnitId")]
        [DisplayName("單位代碼")]
        [Required]
        public int OrgUnitId { get; set; }

        [Column("IpAddr")]
        [DisplayName("允許IP")]
        [Required]
        [StringLength(39, ErrorMessage = "允許IP不可超過39字")]
        public string IpAddr { get; set; }

        [Column("IpDesc")]
        [DisplayName("允許IP說明")]
        [Required]
        [StringLength(50, ErrorMessage = "允許IP說明不可超過50字")]
        public string IpDesc { get; set; }

        [Column("InsertDateTime")]
        [Updatable(true,false)]
        [DisplayName("建立日期")]
        [Required]
        public DateTime InsertDateTime { get; set; }

        [Column("InsertUnitID")]
        [Updatable(true,false)]
        [DisplayName("建立單位")]
        [Required]
        public int InsertUnitID { get; set; }

        [Column("InsertUserID")]
        [Updatable(true,false)]
        [DisplayName("建立人員")]
        [Required]
        public decimal InsertUserID { get; set; }

        [Column("UpdateDateTime")]
        [DisplayName("更新時間")]
        [Required]
        public DateTime UpdateDateTime { get; set; }

        [Column("UpdateUnitID")]
        [DisplayName("更新單位")]
        [Required]
        public int UpdateUnitID { get; set; }

        [Column("UpdateUserID")]
        [DisplayName("更新人員")]
        [Required]
        public decimal UpdateUserID { get; set; }

        [Column("IsDelete")]
        [DisplayName("是否已刪除")]
        [Required]
        public bool IsDelete { get; set; }
    }



}
