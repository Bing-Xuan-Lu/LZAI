using PSTFrame.Data.Dapper;
using System;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PSTFrame.Data.Dapper;

namespace LZAI.Model.DB
{
    [Table("vw_gps_realtime")]
    public class Vw_gps_realtime
    {
        [Key]
        [Column("Sno")]
        [DisplayName("Sno")]
        public int Sno { get; set; }

        [Column("Plate_no")]
        [DisplayName("Plate_no")]
        public string Plate_no { get; set; }

        [Column("DateTime")]
        [DisplayName("DateTime")]
        public DateTime? DateTime { get; set; }

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
        public string IO1 { get; set; }

        [Column("IO2")]
        [DisplayName("IO2")]
        public string IO2 { get; set; }

        [Column("IO3")]
        [DisplayName("IO3")]
        public string IO3 { get; set; }

        [Column("TM97X")]
        [DisplayName("TM97X")]
        public string TM97X { get; set; }

        [Column("TM97Y")]
        [DisplayName("TM97Y")]
        public string TM97Y { get; set; }

        [Column("CleHead_no")]
        [DisplayName("CleHead_no")]
        public string CleHead_no { get; set; }

        [Column("ClePlate_no")]
        [DisplayName("ClePlate_no")]
        public string ClePlate_no { get; set; }

        [Column("CarMachine")]
        [DisplayName("CarMachine")]
        public int CarMachine { get; set; }

        [Column("CleHandPlate_no")]
        [DisplayName("前車牌/後車牌")]
        public string CleHandPlate_no { get; set; }
    }
}
