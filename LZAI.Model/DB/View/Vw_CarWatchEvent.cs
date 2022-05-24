using PSTFrame.Data.Dapper;
using System;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PSTFrame.Data.Dapper;

namespace LZAI.Model.DB
{
    public class Vw_CarWatchEvent
    {
        [Key]
        [Column("EventId")]
        [DisplayName("EventId")]
        [Required]
        public int EventId { get; set; }

        [Column("WatchId")]
        [DisplayName("WatchId")]
        [Required]
        public int WatchId { get; set; }

        [Column("Head_No")]
        [DisplayName("前車牌")]
        [Required]
        [StringLength(10, ErrorMessage = "Head_No不可超過10字")]
        public string Head_No { get; set; }

        [Column("Plate_No")]
        [DisplayName("後車牌")]
        [Required]
        [StringLength(10, ErrorMessage = "Plate_No不可超過10字")]
        public string Plate_No { get; set; }

        [Column("Lane")]
        [DisplayName("車道")]
        [Required]
        public int Lane { get; set; }

        [Column("CarImg")]
        [DisplayName("車輛照片")]
        [StringLength(200, ErrorMessage = "CarImg不可超過200字")]
        public string CarImg { get; set; }

        [Column("IsSetGPS")]
        [DisplayName("是否加裝GPS")]
        [Required]
        public bool IsSetGPS { get; set; }

        [Column("IsGPSHistory")]
        [DisplayName("是否有軌跡")]
        [Required]
        public bool IsGPSHistory { get; set; }

        [Column("IsManual")]
        [DisplayName("是否為手動輸入")]
        [Required]
        public bool IsManual { get; set; }


        [Column("ManualInsetDateTime")]
        [DisplayName("ManualInsetDateTime")]

        public DateTime ManualInsetDateTime { get; set; }

        [Column("InsTime")]
        [DisplayName("辨識時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [Required]
        public DateTime? InsTime { get; set; }

        [Column("Note")]
        [DisplayName("Note")]
        [StringLength(255, ErrorMessage = "Note不可超過255字")]
        public string Note { get; set; }

        [Column("Fac_No")]
        [DisplayName("清除機構管編")]

        public string Fac_No { get; set; }

        [Column("Fac_Name")]
        [DisplayName("清除機構名稱")]

        public string Fac_Name { get; set; }

        [Column("CHIsSetGPS")]
        [DisplayName("類別")]

        public string CHIsSetGPS { get; set; }

        [Column("CHIsGPSHistory")]
        [DisplayName("有無軌跡")]

        public string CHIsGPSHistory { get; set; }

        [Column("RegionName")]
        [DisplayName("進入警示區")]
        public string RegionName { get; set; }

        [Column("RegionId")]
        [DisplayName("進入警示區編號")]
        public string RegionId { get; set; }
      
        
        public class CarWatch_download
        {
           

        [Column("Head_No")]
        [DisplayName("前車牌")]
       
        public string Head_No { get; set; }

        [Column("Plate_No")]
        [DisplayName("後車牌")]
       
       
        public string Plate_No { get; set; }

       

        //[Column("CarImg")]
        //[DisplayName("車輛照片")]
        //[StringLength(200, ErrorMessage = "CarImg不可超過200字")]
        //public string CarImg { get; set; }


       
        [Column("InsTime")]
        [DisplayName("辨識時間")]
       
        public DateTime? InsTime { get; set; }

       

        [Column("Fac_No")]
        [DisplayName("清除機構管編")]

        public string Fac_No { get; set; }

        [Column("Fac_Name")]
        [DisplayName("清除機構名稱")]

        public string Fac_Name { get; set; }

        [Column("CHIsSetGPS")]
        [DisplayName("類別")]

        public string CHIsSetGPS { get; set; }

        [Column("CHIsGPSHistory")]
        [DisplayName("有無軌跡")]

        public string CHIsGPSHistory { get; set; }

        [Column("RegionName")]
        [DisplayName("進入警示區")]
        public string RegionName { get; set; }

     
        }
    }
}