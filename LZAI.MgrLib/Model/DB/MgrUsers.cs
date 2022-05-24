using System;
//using PSTFrame.Data.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PSTFrame.MVC.Model.DataAnnotations;

namespace LZAI.MgrLib.Model.DB
{
    [Table("MgrUsers")]
    public partial class MgrUsers
    {
        [Key]
        [Column("UserId")]
        [DisplayName("使用者代碼")]
        public decimal UserId { get; set; }

        [Column("UnitId")]
        [DisplayName("單位代碼")]
        [Required]
        public int UnitId { get; set; }

        [Column("Account")]
        [DisplayName("登入者帳號")]
        [Required]
        [StringLength(20, ErrorMessage = "登入者帳號不可超過20字")]
        public string Account { get; set; }

        [Column("UserName")]
        [DisplayName("使用者姓名")]
        [Required]
        [StringLength(80, ErrorMessage = "使用者姓名不可超過80字")]
        public string UserName { get; set; }

        [Column("City")]
        [DisplayName("縣市")]
        public string City { get; set; }

        [Column("Tel")]
        [DisplayName("電話")]
        [StringLength(20, ErrorMessage = "電話不可超過20字")]
        public string Tel { get; set; }

        [Column("PhoneNumber")]
        [DisplayName("手機")]
        [StringLength(20, ErrorMessage = "手機不可超過20字")]
        public string PhoneNumber { get; set; }

        [Column("Department")]
        [DisplayName("部門")]
        public string Department { get; set; }

        [Column("Job")]
        [DisplayName("職稱")]
        public string Job { get; set; }

        [Column("Email")]
        [DisplayName("信箱")]
        [Required]
        [StringLength(50, ErrorMessage = "信箱不可超過50字")]
        public string Email { get; set; }

        [Column("IsDelete")]
        [DisplayName("是否已刪除")]
        [Updatable(true, false)]
        [Required]
        public bool IsDelete { get; set; }

        [Column("PasswordHash")]
        [DisplayName("PasswordHash")]
        [StringLength(44, ErrorMessage = "PasswordHash不可超過44字")]
        public string PasswordHash { get; set; }

        [Column("PasswordSalt")]
        [DisplayName("PasswordSalt")]
        [StringLength(8, ErrorMessage = "PasswordSalt不可超過8字")]
        public string PasswordSalt { get; set; }

        [Column("IsNeedChangePW")]
        [DisplayName("IsNeedChangePW")]
        [Updatable(true, false)]
        [Required]
        public bool IsNeedChangePW { get; set; }

        [Column("ChangePwDateTime")]
        [DisplayName("ChangePwDateTime")]
        [Updatable(true, false)]
        public DateTime? ChangePwDateTime { get; set; }

        [Column("WpErrCount")]
        [DisplayName("WpErrCount")]
        [Updatable(true, false)]
        [Required]
        public int WpErrCount { get; set; }

        [Column("WpErrDateTime")]
        [DisplayName("WpErrDateTime")]
        [Updatable(true, false)]
        public DateTime? WpErrDateTime { get; set; }

        [Column("InsertDateTime")]
        [Updatable(true, false)]
        [DisplayName("建立日期")]
        [Required]
        public DateTime InsertDateTime { get; set; }

        [Column("InsertUnitId")]
        [Updatable(true, false)]
        [DisplayName("建立單位")]
        [Required]
        public int InsertUnitId { get; set; }

        [Column("InsertUserId")]
        [Updatable(true, false)]
        [DisplayName("建立人員")]
        [Required]
        public decimal InsertUserId { get; set; }

        [Column("UpdateDateTime")]
        [DisplayName("更新日期")]
        [Required]
        public DateTime UpdateDateTime { get; set; }

        [Column("UpdateUnitId")]
        [DisplayName("更新單位")]
        [Required]
        public int UpdateUnitId { get; set; }

        [Column("UpdateUserId")]
        [DisplayName("更新人員")]
        [Required]
        public decimal UpdateUserId { get; set; }

        [Column("CertificationNo")]
        [DisplayName("自然人憑證卡號")]
        public string CertificationNo{get; set;}

        public string GroupID;

        public string worpas; //暫放明碼號，資料庫並無對應之欄位
        public string OldPwd;
        public string NewPwd;
        public string NewPwdCheck;
        public string Pwd;
    }
}
