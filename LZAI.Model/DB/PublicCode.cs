using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZAI.Model.DB
{
    [Table("PublicCode")]
    public class PublicCode
    {
        [Key]
        [Column("Sno")]
        [Required]
        public int Sno { get; set; }

        [Column("CodeType")]
        [DisplayName("類型")]
        [Required]
        [StringLength(50, ErrorMessage = "類型不可超過50字")]
        public string CodeType { get; set; }

        [Column("CodeNo")]
        [DisplayName("代碼")]
        [Required]
        [StringLength(20, ErrorMessage = "代碼不可超過20字")]
        public string CodeNo { get; set; }

        [Column("CodeNameCH")]
        [DisplayName("中文名稱")]
        [StringLength(200, ErrorMessage = "中文不可超過200字")]
        public string CodeNameCH { get; set; }

        [Column("CodeNameEN")]
        [DisplayName("英文名稱")]
        [StringLength(200, ErrorMessage = "英文不可超過200字")]
        public string CodeNameEN { get; set; }

        [Column("CodeNotes")]
        [DisplayName("備註")]
        [StringLength(300, ErrorMessage = "備註不可超過300字")]
        public string CodeNotes { get; set; }

        [Column("CodeUnit")]
        [DisplayName("單位")]
        [StringLength(200, ErrorMessage = "單位不可超過200字")]
        public string CodeUnit { get; set; }

        [Column("CodeOrder")]
        [DisplayName("排序")]
        [Required]
        public int CodeOrder { get; set; }

        [Column("InsertDateTime")]
        [DisplayName("InsertDateTime")]
        [Required]
        public DateTime InsertDateTime { get; set; }

        [Column("IsDelete")]
        [DisplayName("IsDelete")]
        [Required]
        public bool IsDelete { get; set; }
    }
}
