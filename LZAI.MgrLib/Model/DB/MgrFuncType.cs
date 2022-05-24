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
        [DisplayName("�\�ඵ�ؤ���ID")]
        [Required]
        public int FuncTypeId { get; set; }

        [Column("FuncTypeName")]
        [DisplayName("�\�ඵ�ؤ����W��")]
        [Required]
        [StringLength(50, ErrorMessage = "�\�ඵ�ؤ����W�٤��i�W�L50�r")]
        public string FuncTypeName { get; set; }
    }
}
