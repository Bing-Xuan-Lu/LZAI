using PSTFrame.Data.Dapper;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LZAI.Model.DB
{
    [Table("AuditEvent")]
    public class AuditEvent
    {
        [Key]
        [Column("AuditEventId")]
        [DisplayName("AuditEventId")]
        [IgnoreInsert]
        public decimal AuditEventId { get; set; }

        [Column("EventDateTime")]
        [DisplayName("EventDateTime")]
        [IgnoreInsert]
        public DateTime EventDateTime { get; set; }

        [Column("EventType")]
        [DisplayName("EventType")]
        [Required]
        [StringLength(100, ErrorMessage = "EventType不可超過100字")]
        public string EventType { get; set; }

        [Column("ClientIPAddress")]
        [DisplayName("ClientIPAddress")]
        [Required]
        [StringLength(64, ErrorMessage = "ClientIPAddress不可超過64字")]
        public string ClientIPAddress { get; set; }

        [Column("UserId")]
        [DisplayName("UserId")]
        public int UserId { get; set; }

        [Column("UnitId")]
        [DisplayName("UnitId")]
        public int UnitId { get; set; }

        [Column("FuncId")]
        [DisplayName("FuncId")]
        public int FuncId { get; set; }

        [Column("ActionType")]
        [DisplayName("ActionType")]
        [Required]
        [StringLength(100, ErrorMessage = "ActionType不可超過100字")]
        public string ActionType { get; set; }

        [Column("TableName")]
        [DisplayName("TableName")]
        [StringLength(100, ErrorMessage = "TableName不可超過100字")]
        public string TableName { get; set; }

        [Column("DataProcessType")]
        [DisplayName("DataProcessType")]
        [Required]
        [StringLength(100, ErrorMessage = "DataProcessType不可超過100字")]
        public string DataProcessType { get; set; }

        [Column("PrimaryData1")]
        [DisplayName("PrimaryData1")]
        [Required]
        public string PrimaryData1 { get; set; }

        [Column("PrimaryData2")]
        [DisplayName("PrimaryData2")]
        [Required]
        public string PrimaryData2 { get; set; }

        [Column("PrimaryData3")]
        [DisplayName("PrimaryData3")]
        [Required]
        public string PrimaryData3 { get; set; }

        [Column("PrimaryData4")]
        [DisplayName("PrimaryData4")]
        public decimal PrimaryData4 { get; set; }

        [Column("PrimaryDataInt")]
        [DisplayName("PrimaryDataInt")]
        public decimal PrimaryDataInt { get; set; }

        [Column("JsonData")]
        [DisplayName("JsonData")]
        [Required]
        public string JsonData { get; set; }

        [Column("SubDataType")]
        [DisplayName("SubDataType")]
        public string SubDataType { get; set; }

        [Column("SubDataPrimaryDataInt")]
        [DisplayName("SubDataPrimaryDataInt")]
        public decimal SubDataPrimaryDataInt { get; set; }
    }

    /// <summary>
    /// 資料處理方式
    /// </summary>
    public enum DataProcessType
    {
        Other = 0,
        /// <summary>
        /// 新增
        /// </summary>
        Insert = 1,
        /// <summary>
        /// 修改
        /// </summary>
        Update = 2,
        /// <summary>
        /// 刪除
        /// </summary>
        Delete = 3,
        /// <summary>
        /// 杳詢
        /// </summary>
        Select = 4
    }

    public enum DataType
    {
        /// <summary>
        /// 人員管理
        /// </summary>
        MgrUsers = 1,
        /// <summary>
        /// 群組管理
        /// </summary>
        MgrGroup = 2,
        /// <summary>
        /// 單位管理
        /// </summary>
        MgrOrgUnit = 3,
        /// <summary>
        /// 功能管理
        /// </summary>
        MgrFunc = 4,
        /// <summary>
        /// 警示區
        /// </summary>
        Stop_Region_File = 5,
        /// <summary>
        /// 車牌辨識紀錄
        /// </summary>
        CarWatchEvent = 6,
        /// <summary>
        /// 事業基本資料管理
        /// </summary>
        CleGrantInfo = 7,
        /// <summary>
        /// 車輛管理
        /// </summary>
        CleWasteCar = 8
    }

}
