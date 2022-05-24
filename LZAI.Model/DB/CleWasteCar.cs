using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PSTFrame.Data.Dapper;

namespace LZAI.Model.DB
{
    [Table("CleWasteCar")]
    public class CleWasteCar
    {
        [Key]
        [Column("SerNo")]
        [DisplayName("SerNo")]
        [IgnoreUpdate]
        [IgnoreInsert]
        public int SerNo { get; set; }

        [Column("UnitID")]
        [DisplayName("UnitID")]
        [Required]
        public int UnitID { get; set; }

        [Column("Fac_No")]
        [DisplayName("清除機構管制編號")]
        [Required]
        [StringLength(50, ErrorMessage = "Fac_No不可超過50字")]
        public string Fac_No { get; set; }

        [Column("Fac_Name")]
        [DisplayName("清除機構名稱")]
        [Required]
        [StringLength(255, ErrorMessage = "Fac_Name不可超過255字")]
        public string Fac_Name { get; set; }

        [Column("Head_No")]
        [DisplayName("頭車車號")]
        [Required]
        [StringLength(10, ErrorMessage = "Head_No不可超過10字")]
        public string Head_No { get; set; }

        [Column("Plate_No")]
        [DisplayName("尾車車號")]
        
        [StringLength(10, ErrorMessage = "Plate_No不可超過10字")]
        public string Plate_No { get; set; }

        [Column("InsertUnitID")]
        [DisplayName("InsertUnitID")]
        [IgnoreUpdate]
        [Required]
        public int InsertUnitID { get; set; }

        [Column("InsertUserID")]
        [DisplayName("InsertUserID")]
        [IgnoreUpdate]
        [Required]
        public int InsertUserID { get; set; }

        [Column("InsertDateTime")]
        [IgnoreUpdate]
        [DisplayName("InsertDateTime")]
        [Required]
        public DateTime InsertDateTime { get; set; }

        [Column("UpdateUnitID")]
        [DisplayName("UpdateUnitID")]
        [IgnoreInsert]
        [Required]
        public int UpdateUnitID { get; set; }

        [Column("UpdateUserID")]
        [DisplayName("UpdateUserID")]
        [IgnoreInsert]
        [Required]
        public int UpdateUserID { get; set; }

        [Column("UpdateDateTime")]
        [DisplayName("UpdateDateTime")]
        [IgnoreInsert]
        [Required]
        public DateTime UpdateDateTime { get; set; }

        [Column("CompleteUnitID")]
        [DisplayName("CompleteUnitID")]
        public int CompleteUnitID { get; set; }

        [Column("CompleteUserID")]
        [DisplayName("CompleteUserID")]
        public int CompleteUserID { get; set; }

        [Column("CompleteDateTime")]
        [DisplayName("CompleteDateTime")]
        [IgnoreUpdate]
        public DateTime? CompleteDateTime { get; set; }

        [Column("IsDelete")]
        [DisplayName("IsDelete")]
        [Required]
        public bool IsDelete { get; set; }

        [Column("Reason")]
        [DisplayName("Reason")]
        [StringLength(255, ErrorMessage = "Reason不可超過255字")]
        public string Reason { get; set; }

        [Column("Status")]
        [DisplayName("Status")]
        [Required]
        public int Status { get; set; }

        [Column("CarMachine")]
        [DisplayName("車機裝設位置")]
        [Required]
        public int CarMachine { get; set; }

        [Column("CarModel")]
        [DisplayName("車種")]
        [Required]
        public int CarModel { get; set; }
    }
}
