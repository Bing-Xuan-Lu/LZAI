using System;
//using PSTFrame.Data.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PSTFrame.MVC.Model.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LZAI.MgrLib.Model.DB
{
    [Table("vwMgrUsers")]
    public class VwMgrUsers
    {
        [Key]
        [Column("UserId")]
        [DisplayName("使用者代碼")]
        public int UserId { get; set; }

        [Column("UnitId")]
        [DisplayName("單位代碼")]
        public int UnitId { get; set; }

        [Column("UnitName")]
        [DisplayName("單位名稱")]
        public string UnitName { get; set; }

        [Column("Account")]
        [DisplayName("登入者帳號")]
        public string Account { get; set; }

        [Column("UserName")]
        [DisplayName("使用者姓名")]
        public string UserName { get; set; }

        [Column("Tel")]
        [DisplayName("電話")]
        public string Tel { get; set; }

        [Column("PhoneNumber")]
        [DisplayName("手機")]

        public string PhoneNumber { get; set; }


        [Column("Department")]
        [DisplayName("部門")]
        public string Department { get; set; }

        [Column("Job")]
        [DisplayName("職稱")]
        public string Job { get; set; }

        [Column("Email")]
        [DisplayName("信箱")]
        public string Email { get; set; }

        [Column("IsDelete")]
        [DisplayName("是否已刪除")]
        public bool IsDelete { get; set; }

        [Column("PasswordHash")]
        [DisplayName("PasswordHash")]
        public string PasswordHash { get; set; }

        [Column("PasswordSalt")]
        [DisplayName("PasswordSalt")]
        public string PasswordSalt { get; set; }

        [Column("IsNeedChangePW")]
        [DisplayName("IsNeedChangePW")]
        public bool IsNeedChangePW { get; set; }

        [Column("ChangePwDateTime")]
        [DisplayName("ChangePwDateTime")]
        public DateTime? ChangePwDateTime { get; set; }

        [Column("WpErrCount")]
        [DisplayName("WpErrCount")]
        public int WpErrCount { get; set; }

        [Column("WpErrDateTime")]
        [DisplayName("WpErrDateTime")]
        public DateTime? WpErrDateTime { get; set; }

        [Column("LoginPermission")]
        [DisplayName("登入權限")]
        public string LoginPermission { get; set; }

        //[DisplayName("CityId")]
        //public int CityId { get; set; }

        //[DisplayName("自然人卡號")]
        //public string CertificationNo { get; set; }

        [Column("GroupName")]
        [DisplayName("群組名稱")]
        public string GroupName { get; set; }

        [Column("InsertDateTime")]
        [Updatable(true, false)]
        [DisplayName("建立日期")]
        public DateTime InsertDateTime { get; set; }

        [Column("InsertUnitId")]
        [Updatable(true, false)]
        [DisplayName("建立單位")]
        public int InsertUnitId { get; set; }

        [Column("InsertUserId")]
        [Updatable(true, false)]
        [DisplayName("建立人員")]
        public decimal InsertUserId { get; set; }

        [Column("UpdateDateTime")]
        [DisplayName("更新日期")]
        public DateTime UpdateDateTime { get; set; }

        [Column("UpdateUnitId")]
        [DisplayName("更新單位")]
        public int UpdateUnitId { get; set; }

        [Column("UpdateUserId")]
        [DisplayName("更新人員")]
        public decimal UpdateUserId { get; set; }
    }
}
