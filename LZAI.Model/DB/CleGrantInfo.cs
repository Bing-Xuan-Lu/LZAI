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
        [DisplayName("�Ʒ~���Ǹ�")]
        [IgnoreUpdate]
        [IgnoreInsert]
        public int CleId { get; set; }

        //[Column("DocGuid")]
        //[DisplayName("���Guid")]
        //[Required]
        //public Guid DocGuid { get; set; }

        
        [Column("CleNumber")]
        [DisplayName("�s��")]
        [Required]
        public int CleNumber { get; set; }

        [Column("Cle_No")]
        [DisplayName("�ި�s��")]
        [Required]
        [StringLength(8, ErrorMessage = "�i�Ʒ~���ި�s���j���i�W�L8�r")]
        public string Cle_No { get; set; }

        [Column("BusinessNo")]
        [DisplayName("�Τ@�s��")]
        [StringLength(8, ErrorMessage = "�i�Τ@�s���j���i�W�L8�r")]
        public string BusinessNo { get; set; }

        //[Column("Cle_Grade")]
        //[DisplayName("�ŧO(�ҡB�A�B��)")]
        //[Required]
        //[StringLength(50, ErrorMessage = "�i�ŧO(�ҡB�A�B��)�j���i�W�L50�r")]
        //public string Cle_Grade { get; set; }

        [Column("Cle_Name")]
        [DisplayName("�Ʒ~���W��")]
        [StringLength(70, ErrorMessage = "�i�Ʒ~���W�١j���i�W�L70�r")]
        public string Cle_Name { get; set; }

        [Column("Area_No")]
        [DisplayName("�����O�N�X[A-Z]")]
        [StringLength(1, ErrorMessage = "�i�����O�N�X[A-Z]�j���i�W�L1�r")]
        public string Area_No { get; set; }

        [Column("CityNo")]
        [DisplayName("����")]
        [StringLength(1, ErrorMessage = "�i�����j���i�W�L1�r")]
        public string CityNo { get; set; }

        [Column("DistrictNo")]
        [DisplayName("������F��")]
        [StringLength(4, ErrorMessage = "�i������F�ϡj���i�W�L4�r")]
        public string DistrictNo { get; set; }

        [Column("Addr")]
        [DisplayName("�a�}")]
        [StringLength(120, ErrorMessage = "�i�a�}�j���i�W�L120�r")]
        public string Addr { get; set; }


        [Column("Gate_Long")]
        [DisplayName("�g��")]
        [RegularExpression(@"^[-\+]?((1[0-7]\d{1}|0?\d{1,2})\.\d{1,10}|180\.0{1,10})$", ErrorMessage = "�п�J���T�g��")]


        public float Gate_Long { get; set; }

        [Column("Gate_Lat")]
        [DisplayName("�n��")]
        [RegularExpression(@"^[-\+]?([0-8]?\d{1}\.\d{1,10}|90\.0{1,10})$", ErrorMessage = "�п�J���T�n��")]
        public float Gate_Lat { get; set; }


        //[Column("Capital")]
        //[DisplayName("�ꥻ�B")]
        //public int Capital { get; set; }

        //[Column("Email")]
        //[DisplayName("�q�l�l��")]
        //[StringLength(50, ErrorMessage = "�i�q�l�l��j���i�W�L50�r")]
        //public string Email { get; set; }

        //[Column("Cle_Boss")]
        //[DisplayName("���c�t�d�H�m�W")]
        //[StringLength(20, ErrorMessage = "�i���c�t�d�H�m�W�j���i�W�L20�r")]
        //public string Cle_Boss { get; set; }

        //[Column("Cle_Tel")]
        //[DisplayName("���c�q��")]
        //[StringLength(30, ErrorMessage = "�i���c�q�ܡj���i�W�L30�r")]
        //public string Cle_Tel { get; set; }

        //[Column("Cle_Fax")]
        //[DisplayName("���c�ǯu")]
        //[StringLength(30, ErrorMessage = "�i���c�ǯu�j���i�W�L30�r")]
        //public string Cle_Fax { get; set; }

        //[Column("GrantNo")]
        //[DisplayName("�\�i�Ҧr��")]
        //[StringLength(50, ErrorMessage = "�i�\�i�Ҧr���j���i�W�L50�r")]
        //public string GrantNo { get; set; }

        //[Column("GrantTotalQty")]
        //[DisplayName("�֥i�`�q(���O���ֵo�ɶ�g)")]
        //public decimal GrantTotalQty { get; set; }

        //[Column("GrantDeadline")]
        //[DisplayName("�ҷӳ\�i���Ĵ���")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        //[Required]
        //public DateTime GrantDeadline { get; set; }

        //[Column("ExtendDateTime")]
        //[DisplayName("�i�����(�t�Τ��)")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        //public DateTime? ExtendDateTime { get; set; }

        //[Column("Cle_CityNo")]
        //[DisplayName("�M���a�}-����")]
        //[StringLength(1, ErrorMessage = "�i�M���a�}-�����j���i�W�L1�r")]
        //public string Cle_CityNo { get; set; }

        //[Column("Cle_DistrictNo")]
        //[DisplayName("�M���a�}-��F��")]
        //[StringLength(4, ErrorMessage = "�i�M���a�}-��F�ϡj���i�W�L4�r")]
        //public string Cle_DistrictNo { get; set; }

        //[Column("Cle_Addr")]
        //[DisplayName("�M���a�}")]
        //[Required]
        //[StringLength(70, ErrorMessage = "�i�M���a�}�j���i�W�L70�r")]
        //public string Cle_Addr { get; set; }

        //[Column("Con_CityNo")]
        //[DisplayName("�M���a�}-����")]
        //[StringLength(1, ErrorMessage = "�i�M���a�}-�����j���i�W�L1�r")]
        //public string Con_CityNo { get; set; }

        //[Column("Con_DistrictNo")]
        //[DisplayName("�M���a�}-��F��")]
        //[StringLength(4, ErrorMessage = "�i�M���a�}-��F�ϡj���i�W�L4�r")]
        //public string Con_DistrictNo { get; set; }

        //[Column("Con_Addr")]
        //[DisplayName("�p��(�o��)�a�}")]
        //[Required]
        //[StringLength(120, ErrorMessage = "�i�p��(�o��)�a�}�j���i�W�L120�r")]
        //public string Con_Addr { get; set; }


        //[Column("ApplyStatus")]
        //[DisplayName("���A")]        
        //public string ApplyStatus { get; set; }

        //[Column("IsApproved")]
        //[DisplayName("�O�_�֭�")]
        //[Required]
        //public bool IsApproved { get; set; }

        //[Column("IsLocked")]
        //[DisplayName("�O�_��w")]
        //[Required]
        //public bool IsLocked { get; set; }

        [Column("Remark")]
        [DisplayName("�Ƶ�")]
        [StringLength(500, ErrorMessage = "�i�Ƶ��j���i�W�L500�r")]
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
        [DisplayName("�B�e����")]
        
       
        public string CarSelectGrantInfo { get; set; }

        [Column("CityId")]
        [DisplayName("����")]
        [Required]
        public int CityId { get; set; }

        [Column("DistrictId")]
        [DisplayName("��F��")]
        [Required]
        public int DistrictId { get; set; }

    }
    //public class GrantInfo_download
    //{
     
       

        

    //    [Column("Cle_No")]
    //    [DisplayName("�Ʒ~���ި�s��")]
        
    //    public string Cle_No { get; set; }

    //    [Column("BusinessNo")]
    //    [DisplayName("�Τ@�s��")]
    
    //    public string BusinessNo { get; set; }

       

    //    [Column("Cle_Name")]
    //    [DisplayName("�Ʒ~���W��")]
        
    //    public string Cle_Name { get; set; }

       

    //    [Column("CityNo")]
    //    [DisplayName("����")]
       
    //    public string CityNo { get; set; }

    //    [Column("DistrictNo")]
    //    [DisplayName("������F��")]
       
    //    public string DistrictNo { get; set; }

    //    [Column("Addr")]
    //    [DisplayName("�a�}")]
      
    //    public string Addr { get; set; }


    //    [Column("Gate_Long")]
    //    [DisplayName("�g��")]
       

    //    public float Gate_Long { get; set; }

    //    [Column("Gate_Lat")]
    //    [DisplayName("�n��")]
       
    //    public float Gate_Lat { get; set; }



    //    [Column("CityId")]
    //    [DisplayName("CityId")]
    //    public int CityId { get; set; }

    //    [Column("DistrictId")]
    //    [DisplayName("DistrictId")]
    //    public int DistrictId { get; set; }
    //}
}
