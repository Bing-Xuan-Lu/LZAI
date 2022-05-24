using PSTFrame.MVC.Model.DataAnnotations;
using System;
//using PSTFrame.Data.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LZAI.MgrLib.Model.DB
{
    [Table("Mgr_Config")]
    public class Mgr_Config
    {
        [Key]
        [Column("ConfigID")]
        [DisplayName("ConfigID")]
        public string ConfigID { get; set; }

        [Column("ConfigValue")]
        [DisplayName("ConfigValue")]
        public string ConfigValue { get; set; }

        [Column("ConfigDesc")]
        [DisplayName("ConfigDesc")]
        public string ConfigDesc { get; set; }
    }

}
