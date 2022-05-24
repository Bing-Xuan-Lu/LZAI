using System;
//using PSTFrame.Data.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PSTFrame.MVC.Model.DataAnnotations;

namespace LZAI.MgrLib.Model.DB
{
    [Table("MgrOrgUnitAllowIP")]
    public class MgrOrgUnitAllowIP
    {
        [Key]
        [Column("SerNo")]
        [DisplayName("�y����")]
        [Required]
        public decimal SerNo { get; set; }

        [Column("OrgUnitId")]
        [DisplayName("���N�X")]
        [Required]
        public int OrgUnitId { get; set; }

        [Column("IpAddr")]
        [DisplayName("���\IP")]
        [Required]
        [StringLength(39, ErrorMessage = "���\IP���i�W�L39�r")]
        public string IpAddr { get; set; }

        [Column("IpDesc")]
        [DisplayName("���\IP����")]
        [Required]
        [StringLength(50, ErrorMessage = "���\IP�������i�W�L50�r")]
        public string IpDesc { get; set; }

        [Column("InsertDateTime")]
        [Updatable(true,false)]
        [DisplayName("�إߤ��")]
        [Required]
        public DateTime InsertDateTime { get; set; }

        [Column("InsertUnitID")]
        [Updatable(true,false)]
        [DisplayName("�إ߳��")]
        [Required]
        public int InsertUnitID { get; set; }

        [Column("InsertUserID")]
        [Updatable(true,false)]
        [DisplayName("�إߤH��")]
        [Required]
        public decimal InsertUserID { get; set; }

        [Column("UpdateDateTime")]
        [DisplayName("��s�ɶ�")]
        [Required]
        public DateTime UpdateDateTime { get; set; }

        [Column("UpdateUnitID")]
        [DisplayName("��s���")]
        [Required]
        public int UpdateUnitID { get; set; }

        [Column("UpdateUserID")]
        [DisplayName("��s�H��")]
        [Required]
        public decimal UpdateUserID { get; set; }

        [Column("IsDelete")]
        [DisplayName("�O�_�w�R��")]
        [Required]
        public bool IsDelete { get; set; }
    }



}
