using PSTFrame.Data.Dapper;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LZAI.Model.DB
{
    [Table("LPRLog")]
    public class LPRLog
    {
        [Key]
        [Column("sid")]
        [DisplayName("sid")]
        [Required]
        public int sid { get; set; }

        [Column("channel_id")]
        [DisplayName("channel_id")]
        [Required]
        public int channel_id { get; set; }

        [Column("time")]
        [DisplayName("time")]
        [Required]
        public DateTime time { get; set; }

        [Column("data")]
        [DisplayName("data")]
        [Required]
        [StringLength(20, ErrorMessage = "data不可超過20字")]
        public string data { get; set; }

        [Column("image")]
        [DisplayName("image")]
        [StringLength(200, ErrorMessage = "image不可超過200字")]
        public string image { get; set; }

        [Column("result_code")]
        [DisplayName("result_code")]
        [StringLength(20, ErrorMessage = "result_code不可超過20字")]
        public string result_code { get; set; }
    }
}
