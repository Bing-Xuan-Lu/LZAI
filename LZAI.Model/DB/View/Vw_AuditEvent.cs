using PSTFrame.Data.Dapper;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LZAI.Model.DB
{
    [Table("vw_AuditEvent")]
    public class Vw_AuditEvent
    {
        [Key]
        [Column("AuditEventId")]
        [DisplayName("序號")]
        [IgnoreInsert]
        public decimal AuditEventId { get; set; }

        [Column("EventDateTime")]
        [DisplayName("操作時間")]
        [IgnoreInsert]
        public DateTime EventDateTime { get; set; }


        [Column("ClientIPAddress")]
        [DisplayName("IP位置")]
        [Required]
        [StringLength(64, ErrorMessage = "ClientIPAddress不可超過64字")]
        public string ClientIPAddress { get; set; }


        [Column("UserName")]
        [DisplayName("姓名")]
        public string UserName { get; set; }

        [Column("Account")]
        [DisplayName("帳號")]
        public string Account { get; set; }



        [Column("FuncName")]
        [DisplayName("功能")]
        public string FuncName { get; set; }

        [Column("ActionType")]
        [DisplayName("操作方式")]
        [Required]
        [StringLength(100, ErrorMessage = "ActionType不可超過100字")]
        public string ActionType { get; set; }

        [Column("Action")]
        [DisplayName("修改內容")]
        [Required]
        [StringLength(100, ErrorMessage = "ActionType不可超過100字")]
        public string Action { get; set; }
        public class AuditEvent_download
        {
           
            [Column("AuditEventId")]
            [DisplayName("序號")]
           
            public decimal AuditEventId { get; set; }

            [Column("EventDateTime")]
            [DisplayName("操作時間")]
          
            public DateTime EventDateTime { get; set; }


            [Column("ClientIPAddress")]
            [DisplayName("IP位置")]
           
            public string ClientIPAddress { get; set; }


            [Column("UserName")]
            [DisplayName("姓名")]
            public string UserName { get; set; }

            [Column("Account")]
            [DisplayName("帳號")]
            public string Account { get; set; }



            [Column("FuncName")]
            [DisplayName("功能")]
            public string FuncName { get; set; }

            [Column("ActionType")]
            [DisplayName("操作方式")]
           
            public string ActionType { get; set; }

            [Column("Action")]
            [DisplayName("修改內容")]
          
            public string Action { get; set; }
        }
    }

}