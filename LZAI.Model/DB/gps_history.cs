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
        [DisplayName("�y����")]
        [Required]
        public int Sno { get; set; }

        [Column("Plate_no")]
        [DisplayName("����")]
        [Required]
        [StringLength(50, ErrorMessage = "�������i�W�L50�r")]
        public string Plate_no { get; set; }

        [Column("HistoryJsonFileName")]
        [DisplayName("�y��Json�ɦW")]
        [Required]
        [StringLength(100, ErrorMessage = "�y��Json�ɦW���i�W�L100�r")]
        public string HistoryJsonFileName { get; set; }

        [Column("HistoryWKT")]
        [DisplayName("���v�y����WKT")]
        public string HistoryWKT { get; set; }

        [Column("HistoryDate")]
        [DisplayName("���v�y����")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? HistoryDate { get; set; }

        [Column("Ins_time")]
        [DisplayName("��Ʒs�W����ɶ�")]
        [Required]
        public DateTime Ins_time { get; set; }

        [Column("Sync_time")]
        [DisplayName("�P�Ƽo�P�B�ɶ�")]
        [Required]
        public DateTime Sync_time { get; set; }
    }
}
