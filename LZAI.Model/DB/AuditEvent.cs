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
        [StringLength(100, ErrorMessage = "EventType���i�W�L100�r")]
        public string EventType { get; set; }

        [Column("ClientIPAddress")]
        [DisplayName("ClientIPAddress")]
        [Required]
        [StringLength(64, ErrorMessage = "ClientIPAddress���i�W�L64�r")]
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
        [StringLength(100, ErrorMessage = "ActionType���i�W�L100�r")]
        public string ActionType { get; set; }

        [Column("TableName")]
        [DisplayName("TableName")]
        [StringLength(100, ErrorMessage = "TableName���i�W�L100�r")]
        public string TableName { get; set; }

        [Column("DataProcessType")]
        [DisplayName("DataProcessType")]
        [Required]
        [StringLength(100, ErrorMessage = "DataProcessType���i�W�L100�r")]
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
    /// ��ƳB�z�覡
    /// </summary>
    public enum DataProcessType
    {
        Other = 0,
        /// <summary>
        /// �s�W
        /// </summary>
        Insert = 1,
        /// <summary>
        /// �ק�
        /// </summary>
        Update = 2,
        /// <summary>
        /// �R��
        /// </summary>
        Delete = 3,
        /// <summary>
        /// �H��
        /// </summary>
        Select = 4
    }

    public enum DataType
    {
        /// <summary>
        /// �H���޲z
        /// </summary>
        MgrUsers = 1,
        /// <summary>
        /// �s�պ޲z
        /// </summary>
        MgrGroup = 2,
        /// <summary>
        /// ���޲z
        /// </summary>
        MgrOrgUnit = 3,
        /// <summary>
        /// �\��޲z
        /// </summary>
        MgrFunc = 4,
        /// <summary>
        /// ĵ�ܰ�
        /// </summary>
        Stop_Region_File = 5,
        /// <summary>
        /// ���P���Ѭ���
        /// </summary>
        CarWatchEvent = 6,
        /// <summary>
        /// �Ʒ~�򥻸�ƺ޲z
        /// </summary>
        CleGrantInfo = 7,
        /// <summary>
        /// �����޲z
        /// </summary>
        CleWasteCar = 8
    }

}
