using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LZAI.MgrLib.Model.DB
{
    [Table("MgrFuncType")]
    public class MgrFuncType
    {
        [Key]
        [Column("FuncTypeId")]
        [DisplayName("功能項目分類ID")]
        [Required]
        public int FuncTypeId { get; set; }

        [Column("FuncTypeName")]
        [DisplayName("功能項目分類名稱")]
        [Required]
        [StringLength(50, ErrorMessage = "功能項目分類名稱不可超過50字")]
        public string FuncTypeName { get; set; }
    }
}
