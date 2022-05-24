using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PSTFrame.Data.Dapper;

namespace LZAI.Model.DB
{
    [Table("CarWatchEvent")]
    public class CarWatchEvent
    {
        [Key]
        [Column("EventId")]
        [DisplayName("EventId")]
        public int EventId { get; set; }

        [Column("WatchId")]
        [DisplayName("WatchId")]
        [Required]
        public int WatchId { get; set; }

        [Column("Head_No")]
        [DisplayName("Head_No")]
        [Required]
        [StringLength(10, ErrorMessage = "Head_No不可超過10字")]
        public string Head_No { get; set; }

        [Column("Plate_No")]
        [DisplayName("Plate_No")]
        [StringLength(10, ErrorMessage = "Plate_No不可超過10字")]
        public string Plate_No { get; set; }

        [Column("Lane")]
        [DisplayName("車道")]
        [Required]
        public int Lane { get; set; }

        [Column("CarImg")]
        [DisplayName("CarImg")]
        [StringLength(200, ErrorMessage = "CarImg不可超過200字")]
        public string CarImg { get; set; }

        [Column("IsSetGPS")]
        [DisplayName("是否加裝GPS")]
        public bool IsSetGPS { get; set; }

        [Column("IsGPSHistory")]
        [DisplayName("是否有軌跡")]
        public bool IsGPSHistory { get; set; }

        [Column("IsCleGrant")]
        [DisplayName("是否事業點")]
        public bool IsCleGrant { get; set; }

        [Column("IsStopRegion")]
        [DisplayName("是否警示區")]
        public bool IsStopRegion { get; set; }

        [Column("IsManual")]
        [DisplayName("是否為手動輸入")]
        public bool IsManual { get; set; }

        [Column("ManualInsetDateTime")]
        [DisplayName("ManualInsetDateTime")]
       
        public DateTime ManualInsetDateTime { get; set; }

        [Column("InsTime")]
        [DisplayName("InsTime")]
        [IgnoreInsert]
        public DateTime InsTime { get; set; }

        [Column("InsertUnitID")]
        [DisplayName("InsertUnitID")]
        [IgnoreUpdate]
        public int InsertUnitID { get; set; }

        [Column("InsertUserID")]
        [DisplayName("InsertUserID")]
        [IgnoreUpdate]
        public int InsertUserID { get; set; }

        [Column("Note")]
        [DisplayName("Note")]
        [StringLength(255, ErrorMessage = "Note不可超過255字")]
        public string Note { get; set; }

        [Column("LastWatchTime")]
        [DisplayName("LastWatchTime")]
        public DateTime? LastWatchTime { get; set; }
    }
}