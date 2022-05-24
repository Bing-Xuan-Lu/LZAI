using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSTFrame.Data.Dapper;

namespace LZAI.Model.DB
{
    [Table("Stop_Region_File")]
    public class Stop_Region_File
    {
        [Key]
        [Column("ID")]
        [DisplayName("ID")]
        [IgnoreInsert]
        public int ID { get; set; }

        [Column("RegionName")]
        [DisplayName("警示區域名稱")]
        [StringLength(100, ErrorMessage = "警示區域名稱不可超過100字")]
        [Required]
        public string RegionName { get; set; }

        [Column("MonitorEndTime")]
        [DisplayName("MonitorEndTime")]
        public DateTime? MonitorEndTime { get; set; }

        [Column("category")]
        [DisplayName("管制類別")]
        [Required]
        public int category { get; set; }

        [Column("Email")]
        [DisplayName("Email")]
        [StringLength(100, ErrorMessage = "Email不可超過100字")]
        public string Email { get; set; }

        [Column("SMSNum")]
        [DisplayName("SMSNum")]
        [StringLength(20, ErrorMessage = "SMSNum不可超過20字")]
        public string SMSNum { get; set; }

        [Column("StopType")]
        [DisplayName("StopType")]
        [Required]
        public string StopType { get; set; }

        [Column("Note")]
        [DisplayName("Note")]
        [StringLength(50, ErrorMessage = "Note不可超過50字")]
        public string Note { get; set; }

        [Column("LandUnitId")]
        [DisplayName("LandUnitId")]
        public int LandUnitId { get; set; }

        [Column("InsertDateTime")]
        [IgnoreUpdate]
        [DisplayName("建立日期")]
        public DateTime InsertDateTime { get; set; } = DateTime.Now;

        [Column("InsertUnitId")]
        [IgnoreUpdate]
        [DisplayName("建立單位")]
        [Required]
        public int InsertUnitId { get; set; }

        [Column("InsertUserId")]
        [IgnoreUpdate]
        [DisplayName("建立人員")]
        [Required]
        public decimal InsertUserId { get; set; }

        [Column("UpdateDateTime")]
        [DisplayName("更新日期")]
        [Required]
        public DateTime UpdateDateTime { get; set; } = DateTime.Now;

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
        public bool IsDelete { get; set; } = false;
    }
}
