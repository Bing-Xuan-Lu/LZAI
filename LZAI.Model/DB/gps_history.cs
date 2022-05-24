using PSTFrame.Data.Dapper;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LZAI.Model.DB
{
    [Table("gps_history")]
    public class gps_history
    {
        [Key]
        [Column("Sno")]
        [DisplayName("流水號")]
        [Required]
        public int Sno { get; set; }

        [Column("Plate_no")]
        [DisplayName("車號")]
        [Required]
        [StringLength(50, ErrorMessage = "車號不可超過50字")]
        public string Plate_no { get; set; }

        [Column("HistoryJsonFileName")]
        [DisplayName("軌跡Json檔名")]
        [Required]
        [StringLength(100, ErrorMessage = "軌跡Json檔名不可超過100字")]
        public string HistoryJsonFileName { get; set; }

        [Column("HistoryWKT")]
        [DisplayName("歷史軌跡整日WKT")]
        public string HistoryWKT { get; set; }

        [Column("HistoryDate")]
        [DisplayName("歷史軌跡日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? HistoryDate { get; set; }

        [Column("Ins_time")]
        [DisplayName("資料新增日期時間")]
        [Required]
        public DateTime Ins_time { get; set; }

        [Column("Sync_time")]
        [DisplayName("與事廢同步時間")]
        [Required]
        public DateTime Sync_time { get; set; }
    }
}
