using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LZAI.Model.DB
{
    [Table("Stop_Region_Location")]
    public class Stop_Region_Location
    {
        [Key]
        [Column("Sno")]
        [DisplayName("Sno")]
        [Required]
        public int Sno { get; set; }

        [Column("ID")]
        [DisplayName("ID")]
        [Required]
        public decimal ID { get; set; }

        [Column("WGS_LON")]
        [DisplayName("WGS_LON")]
        [Required]
        public decimal WGS_LON { get; set; }

        [Column("WGS_LAT")]
        [DisplayName("WGS_LAT")]
        [Required]
        public decimal WGS_LAT { get; set; }

        [Column("TM2X")]
        [DisplayName("TM2X")]
        [Required]
        public string TM2X { get; set; }

        [Column("TM2Y")]
        [DisplayName("TM2Y")]
        [Required]
        public string TM2Y { get; set; }

        [Column("ins_time")]
        [DisplayName("ins_time")]
        [Required]
        public DateTime ins_time { get; set; }

        [Column("Shape")]
        [DisplayName("Shape")]
        public string Shape { get; set; }

        [Column("ShapeXY")]
        [DisplayName("ShapeXY")]
        public string ShapeXY { get; set; }

        [Column("RegionCount")]
        [DisplayName("RegionCount")]
        [Required]
        public int RegionCount { get; set; }
    }
}