using PSTFrame.Data.Dapper;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LZAI.Model.DB
{
    [Table("gps_realtime")]
    public class gps_realtime
    {
        [Key]
        [Column("Sno")]
        [DisplayName("Sno")]
        [IgnoreInsert]
        public int Sno { get; set; }

        [Column("Plate_no")]
        [DisplayName("Plate_no")]
        [Required]
        [StringLength(10, ErrorMessage = "Plate_no不可超過10字")]
        public string Plate_no { get; set; }

        [Column("DateTime")]
        [DisplayName("DateTime")]
        [Required]
        public DateTime DateTime { get; set; }

        [Column("WGS_LON")]
        [DisplayName("WGS_LON")]
        public string WGS_LON { get; set; }

        [Column("WGS_LAT")]
        [DisplayName("WGS_LAT")]
        public string WGS_LAT { get; set; }

        [Column("Heading")]
        [DisplayName("Heading")]
        public int Heading { get; set; }

        [Column("Speed")]
        [DisplayName("Speed")]
        public decimal Speed { get; set; }

        [Column("Sat")]
        [DisplayName("Sat")]
        public int Sat { get; set; }

        [Column("IO1")]
        [DisplayName("IO1")]
        [StringLength(1, ErrorMessage = "IO1不可超過1字")]
        public string IO1 { get; set; }

        [Column("IO2")]
        [DisplayName("IO2")]
        [StringLength(1, ErrorMessage = "IO2不可超過1字")]
        public string IO2 { get; set; }

        [Column("IO3")]
        [DisplayName("IO3")]
        [StringLength(1, ErrorMessage = "IO3不可超過1字")]
        public string IO3 { get; set; }

        [Column("TM97X")]
        [DisplayName("TM97X")]
        public string TM97X { get; set; }

        [Column("TM97Y")]
        [DisplayName("TM97Y")]
        public string TM97Y { get; set; }
    }
}
