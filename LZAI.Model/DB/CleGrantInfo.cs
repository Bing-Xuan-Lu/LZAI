using PSTFrame.Data.Dapper;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LZAI.Model.DB
{
    [Table("CleGrantInfo")]
    public class CleGrantInfo
    {
        [Key]
        [Column("CleId")]
        [DisplayName("事業單位序號")]
        [IgnoreUpdate]
        [IgnoreInsert]
        public int CleId { get; set; }

        //[Column("DocGuid")]
        //[DisplayName("資料Guid")]
        //[Required]
        //public Guid DocGuid { get; set; }

        
        [Column("CleNumber")]
        [DisplayName("編號")]
        [Required]
        public int CleNumber { get; set; }

        [Column("Cle_No")]
        [DisplayName("管制編號")]
        [Required]
        [StringLength(8, ErrorMessage = "【事業單位管制編號】不可超過8字")]
        public string Cle_No { get; set; }

        [Column("BusinessNo")]
        [DisplayName("統一編號")]
        [StringLength(8, ErrorMessage = "【統一編號】不可超過8字")]
        public string BusinessNo { get; set; }

        //[Column("Cle_Grade")]
        //[DisplayName("級別(甲、乙、丙)")]
        //[Required]
        //[StringLength(50, ErrorMessage = "【級別(甲、乙、丙)】不可超過50字")]
        //public string Cle_Grade { get; set; }

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
        [RegularExpression(@"^[-\+]?((1[0-7]\d{1}|0?\d{1,2})\.\d{1,10}|180\.0{1,10})$", ErrorMessage = "請輸入正確經度")]


        public float Gate_Long { get; set; }

        [Column("Gate_Lat")]
        [DisplayName("緯度")]
        [RegularExpression(@"^[-\+]?([0-8]?\d{1}\.\d{1,10}|90\.0{1,10})$", ErrorMessage = "請輸入正確緯度")]
        public float Gate_Lat { get; set; }


        //[Column("Capital")]
        //[DisplayName("資本額")]
        //public int Capital { get; set; }

        //[Column("Email")]
        //[DisplayName("電子郵件")]
        //[StringLength(50, ErrorMessage = "【電子郵件】不可超過50字")]
        //public string Email { get; set; }

        //[Column("Cle_Boss")]
        //[DisplayName("機構負責人姓名")]
        //[StringLength(20, ErrorMessage = "【機構負責人姓名】不可超過20字")]
        //public string Cle_Boss { get; set; }

        //[Column("Cle_Tel")]
        //[DisplayName("機構電話")]
        //[StringLength(30, ErrorMessage = "【機構電話】不可超過30字")]
        //public string Cle_Tel { get; set; }

        //[Column("Cle_Fax")]
        //[DisplayName("機構傳真")]
        //[StringLength(30, ErrorMessage = "【機構傳真】不可超過30字")]
        //public string Cle_Fax { get; set; }

        //[Column("GrantNo")]
        //[DisplayName("許可證字號")]
        //[StringLength(50, ErrorMessage = "【許可證字號】不可超過50字")]
        //public string GrantNo { get; set; }

        //[Column("GrantTotalQty")]
        //[DisplayName("核可總量(環保局核發時填寫)")]
        //public decimal GrantTotalQty { get; set; }

        //[Column("GrantDeadline")]
        //[DisplayName("證照許可有效期限")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        //[Required]
        //public DateTime GrantDeadline { get; set; }

        //[Column("ExtendDateTime")]
        //[DisplayName("展延日期(系統日期)")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        //public DateTime? ExtendDateTime { get; set; }

        //[Column("Cle_CityNo")]
        //[DisplayName("清除地址-縣市")]
        //[StringLength(1, ErrorMessage = "【清除地址-縣市】不可超過1字")]
        //public string Cle_CityNo { get; set; }

        //[Column("Cle_DistrictNo")]
        //[DisplayName("清除地址-行政區")]
        //[StringLength(4, ErrorMessage = "【清除地址-行政區】不可超過4字")]
        //public string Cle_DistrictNo { get; set; }

        //[Column("Cle_Addr")]
        //[DisplayName("清除地址")]
        //[Required]
        //[StringLength(70, ErrorMessage = "【清除地址】不可超過70字")]
        //public string Cle_Addr { get; set; }

        //[Column("Con_CityNo")]
        //[DisplayName("清除地址-縣市")]
        //[StringLength(1, ErrorMessage = "【清除地址-縣市】不可超過1字")]
        //public string Con_CityNo { get; set; }

        //[Column("Con_DistrictNo")]
        //[DisplayName("清除地址-行政區")]
        //[StringLength(4, ErrorMessage = "【清除地址-行政區】不可超過4字")]
        //public string Con_DistrictNo { get; set; }

        //[Column("Con_Addr")]
        //[DisplayName("聯絡(發文)地址")]
        //[Required]
        //[StringLength(120, ErrorMessage = "【聯絡(發文)地址】不可超過120字")]
        //public string Con_Addr { get; set; }


        //[Column("ApplyStatus")]
        //[DisplayName("狀態")]        
        //public string ApplyStatus { get; set; }

        //[Column("IsApproved")]
        //[DisplayName("是否核准")]
        //[Required]
        //public bool IsApproved { get; set; }

        //[Column("IsLocked")]
        //[DisplayName("是否鎖定")]
        //[Required]
        //public bool IsLocked { get; set; }

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
        [DisplayName("縣市")]
        [Required]
        public int CityId { get; set; }

        [Column("DistrictId")]
        [DisplayName("行政區")]
        [Required]
        public int DistrictId { get; set; }

    }
    //public class GrantInfo_download
    //{
     
       

        

    //    [Column("Cle_No")]
    //    [DisplayName("事業單位管制編號")]
        
    //    public string Cle_No { get; set; }

    //    [Column("BusinessNo")]
    //    [DisplayName("統一編號")]
    
    //    public string BusinessNo { get; set; }

       

    //    [Column("Cle_Name")]
    //    [DisplayName("事業單位名稱")]
        
    //    public string Cle_Name { get; set; }

       

    //    [Column("CityNo")]
    //    [DisplayName("縣市")]
       
    //    public string CityNo { get; set; }

    //    [Column("DistrictNo")]
    //    [DisplayName("縣市行政區")]
       
    //    public string DistrictNo { get; set; }

    //    [Column("Addr")]
    //    [DisplayName("地址")]
      
    //    public string Addr { get; set; }


    //    [Column("Gate_Long")]
    //    [DisplayName("經度")]
       

    //    public float Gate_Long { get; set; }

    //    [Column("Gate_Lat")]
    //    [DisplayName("緯度")]
       
    //    public float Gate_Lat { get; set; }



    //    [Column("CityId")]
    //    [DisplayName("CityId")]
    //    public int CityId { get; set; }

    //    [Column("DistrictId")]
    //    [DisplayName("DistrictId")]
    //    public int DistrictId { get; set; }
    //}
}
