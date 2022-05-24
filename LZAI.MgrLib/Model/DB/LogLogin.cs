using System;
//using PSTFrame.Data.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PSTFrame.MVC.Model.DataAnnotations;

namespace LZAI.MgrLib.Model.DB
{
    [Table("LogLogin")]
    public class LogLogin
    {
        [Key]
        [Column("SerId")]
        [DisplayName("SerId")]
        [Updatable(false,false)]
        public int SerId { get; set; }

        [Column("LogDateTime")]
        [DisplayName("建立日期")]
        [Required]
        public DateTime LogDateTime { get; set; }

        [Column("Account")]
        [DisplayName("使用者帳號")]
        [Required]
        [StringLength(20, ErrorMessage = "使用者帳號不可超過20字")]
        public string Account { get; set; }

        [Column("UserIP")]
        [DisplayName("使用者 IP")]
        [Required]
        [StringLength(39, ErrorMessage = "使用者 IP不可超過39字")]
        public string UserIP { get; set; }

        [Column("LoginStatusId")]
        [DisplayName("0:成功登入,1:無此帳號,2:密碼錯誤,3:Ip不允許")]
        [Required]
        public int LoginStatusId { get; set; }

        [Column("UserID")]
        [DisplayName("使用者ID")]
        public int UserID { get; set; }

        [Column("Remark")]
        [DisplayName("備註")]
        public string Remark { get; set; }
    }


}
