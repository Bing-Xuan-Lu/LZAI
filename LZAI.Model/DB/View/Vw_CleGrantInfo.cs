using PSTFrame.Data.Dapper;
using System;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PSTFrame.Data.Dapper;

namespace LZAI.Model.DB
{
   public class Vw_CleGrantInfo
    {

        [Key]
        [Column("CleId")]
        [DisplayName("事業單位序號")]
        [IgnoreUpdate]
        [IgnoreInsert]
        public int CleId { get; set; }

        [Column("CleNumber")]
        [DisplayName("編號")]
        [Required]
        public int CleNumber { get; set; }

        [Column("Cle_No")]
        [DisplayName("事業單位管制編號")]
        [Required]
        [StringLength(8, ErrorMessage = "【事業單位管制編號】不可超過8字")]
        public string Cle_No { get; set; }

        [Column("BusinessNo")]
        [DisplayName("統一編號")]
        [StringLength(8, ErrorMessage = "【統一編號】不可超過8字")]
        public string BusinessNo { get; set; }

        

        [Column("Cle_Name")]
        [DisplayName("事業單位名稱")]
        [StringLength(70, ErrorMessage = "【事業單位名稱】不可超過70字")]
        public string Cle_Name { get; set; }

        [Column("Area_No")]
        [DisplayName("縣市別代碼[A-Z]")]
        [StringLength(1, ErrorMessage = "【縣市別代碼[A-Z]】不可超過1字")]
        public string Area_No { get; set; }

        [Column("CityNo")]
        [DisplayName("縣市")]
        [StringLength(1, ErrorMessage = "【縣市】不可超過1字")]
        public string CityNo { get; set; }

        [Column("DistrictNo")]
        [DisplayName("縣市行政區")]
        [StringLength(4, ErrorMessage = "【縣市行政區】不可超過4字")]
        public string DistrictNo { get; set; }

        [Column("Addr")]
        [DisplayName("地址")]
        [StringLength(120, ErrorMessage = "【地址】不可超過120字")]
        public string Addr { get; set; }


        [Column("Gate_Long")]
        [DisplayName("經度")]
        [RegularExpression(@"^[-\+]?((1[0-7]\d{1}|0?\d{1,2})\.\d{1,5}|180\.0{1,5})$", ErrorMessage = "請輸入正確經度")]


        public float Gate_Long { get; set; }

        [Column("Gate_Lat")]
        [DisplayName("緯度")]
        [RegularExpression(@"^[-\+]?([0-8]?\d{1}\.\d{1,5}|90\.0{1,5})$", ErrorMessage = "請輸入正確緯度")]
        public float Gate_Lat { get; set; }



        [Column("Remark")]
        [DisplayName("備註")]
        [StringLength(500, ErrorMessage = "【備註】不可超過500字")]
        public string Remark { get; set; }

        [Column("UnitId")]
        [DisplayName("FK MgrOrgUnit.UnitId")]
        [Required]
        public int UnitId { get; set; }

        [Column("InsertDateTime")]
        [DisplayName("InsertDateTime")]
        [Required]
        [IgnoreInsert]
        [IgnoreUpdate]
        public DateTime InsertDateTime { get; set; }

        [Column("InsertUnitId")]
        [DisplayName("InsertUnitId")]
        [Required]
        public int InsertUnitId { get; set; }

        [Column("InsertUserId")]
        [DisplayName("InsertUserId")]
        [Required]
        public long InsertUserId { get; set; }

        [Column("UpdateDateTime")]
        [DisplayName("UpdateDateTime")]
        [Required]
        public DateTime UpdateDateTime { get; set; }

        [Column("UpdateUnitId")]
        [DisplayName("UpdateUnitId")]
        [Required]
        public int UpdateUnitId { get; set; }

        [Column("UpdateUserId")]
        [DisplayName("UpdateUserId")]
        [Required]
        public long UpdateUserId { get; set; }

        [Column("IsDelete")]
        [DisplayName("IsDelete")]
        [Required]
        [IgnoreInsert]
        public bool IsDelete { get; set; }


        [Column("CarSelectGrantInfo")]
        [DisplayName("運送車輛")]

        public string CarSelectGrantInfo { get; set; }

        [Column("CityId")]
        [DisplayName("CityId")]
        public int CityId { get; set; }

        [Column("DistrictId")]
        [DisplayName("DistrictId")]
        public int DistrictId { get; set; }


        [Column("CityName")]
        [DisplayName("縣市")]
        public string CityName { get; set; }

        [Column("DistrictName")]
        [DisplayName("行政區")]
        public string DistrictName { get; set; }

        [Column("CarSelectGrantInfoCH")]
        [DisplayName("運送車輛")]
        public string CarSelectGrantInfoCH { get; set; }

    }
    public class GrantInfo_download
    {
        [Column("CleNumber")]
        [DisplayName("編號")]
        [Required]
        public int CleNumber { get; set; }

        [Column("Cle_No")]
        [DisplayName("事業單位管制編號")]


        public string Cle_No { get; set; }

        //[Column("BusinessNo")]
        //[DisplayName("統一編號")]

        //public string BusinessNo { get; set; }



        [Column("Cle_Name")]
        [DisplayName("事業單位名稱")]

        public string Cle_Name { get; set; }



        //[Column("CityNo")]
        //[DisplayName("縣市")]

        //public string CityNo { get; set; }

        //[Column("DistrictNo")]
        //[DisplayName("縣市行政區")]

        //public string DistrictNo { get; set; }

        [Column("Addr")]
        [DisplayName("地址")]

        public string Addr { get; set; }


        [Column("Gate_Long")]
        [DisplayName("經度")]


        public float Gate_Long { get; set; }

        [Column("Gate_Lat")]
        [DisplayName("緯度")]

        public float Gate_Lat { get; set; }

        [Column("CityName")]
        [DisplayName("縣市")]
        public string CityName { get; set; }

        [Column("DistrictName")]
        [DisplayName("行政區")]
        public string DistrictName { get; set; }

        [Column("CarSelectGrantInfoCH")]
        [DisplayName("運送車輛")]
        public string CarSelectGrantInfoCH { get; set; }

    }
}
