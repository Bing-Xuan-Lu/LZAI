using System;
//using PSTFrame.Data.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PSTFrame.MVC.Model.DataAnnotations;

namespace LZAI.MgrLib.Model.DB
{
    [Table("MgrFunc")]
    public class MgrFunc
    {
        [Key]
        [Column("FuncId")]
        [DisplayName("�\��N�X")]
        public int FuncId { get; set; }

        [Column("FuncName")]
        [DisplayName("�\��W��")]
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "�\��W�٤��i�W�L50�r")]
        public string FuncName { get; set; }

        [Column("FuncMemo")]
        [DisplayName("FuncMemo")]
        [StringLength(200, ErrorMessage = "FuncMemo���i�W�L200�r")]
        public string FuncMemo { get; set; }

        [Column("FuncTypeId")]
        [DisplayName("�D��")]
        public int FuncTypeId { get; set; }

        [Column("IsAddFunc")]
        [DisplayName("�O�_�s�W�\��")]
        [Required]
        //"1:���\�� 0:�L�\��"
        public bool IsAddFunc { get; set; }

        [Column("IsModFunc")]
        [DisplayName("�O�_�ק�\��")]
        [Required]
        //"1:���\�� 0:�L�\��""
        public bool IsModFunc { get; set; }

        [Column("IsCanFunc")]
        [DisplayName("�O�_�R���\��")]
        [Required]
        //"1:���\�� 0:�L�\��"
        public bool IsCanFunc { get; set; }

        [Column("IsQueFunc")]
        [DisplayName("�O�_�d�ߥ\��")]
        [Required]
        //"1:���\�� 0:�L�\��"
        public bool IsQueFunc { get; set; }

        [Column("IsOthPerm1")]
        [DisplayName("�䥦�v��1")]
        [Required]
        //0:�L 1:�� �h�ݭn��gOthPerm1Name����
        public bool IsOthPerm1 { get; set; }

        [Column("OthPerm1Name")]
        [DisplayName("�䥦�v��1_�W��")]
        [StringLength(50, ErrorMessage = "�䥦�v��1_�W�٤��i�W�L50�r")]
        public string OthPerm1Name { get; set; }

        [Column("IsOthPerm2")]
        [DisplayName("�䥦�v��2")]
        [Required]
        //0:�L 1:�� �h�ݭn��gOthPerm2Name����
        public bool IsOthPerm2 { get; set; }

        [Column("OthPerm2Name")]
        [DisplayName("�䥦�v��2_�W��")]
        [StringLength(50, ErrorMessage = "�䥦�v��2_�W�٤��i�W�L50�r")]
        public string OthPerm2Name { get; set; }

        [Column("IsOthPerm3")]
        [DisplayName("�䥦�v��3")]
        [Required]
        //0:�L 1:�� �h�ݭn��gOthPerm3Name����
        public bool IsOthPerm3 { get; set; }

        [Column("OthPerm3Name")]
        [DisplayName("�䥦�v��3_�W��")]
        [StringLength(50, ErrorMessage = "�䥦�v��3_�W�٤��i�W�L50�r")]
        public string OthPerm3Name { get; set; }

        [Column("IsOthPerm4")]
        [DisplayName("�䥦�v��4")]
        [Required]
        //0:�L 1:�� �h�ݭn��gOthPerm4Name����
        public bool IsOthPerm4 { get; set; }

        [Column("OthPerm4Name")]
        [DisplayName("�䥦�v��4_�W��")]
        [StringLength(50, ErrorMessage = "�䥦�v��4_�W�٤��i�W�L50�r")]
        public string OthPerm4Name { get; set; }

        [Column("IsOthPerm5")]
        [DisplayName("�䥦�v��5")]
        [Required]
        public bool IsOthPerm5 { get; set; }

        [Column("OthPerm5Name")]
        [DisplayName("�䥦�v��5_�W��")]
        [StringLength(50, ErrorMessage = "�䥦�v��5_�W�٤��i�W�L50�r")]
        public string OthPerm5Name { get; set; }

        [Column("InsertDateTime")]
        [Updatable(true,false)]
        [DisplayName("�إߤ��")]
        [Required]
        public DateTime InsertDateTime { get; set; }

        [Column("InsertUnitId")]
        [Updatable(true,false)]
        [DisplayName("�إ߳��")]
        [Required]
        public int InsertUnitId { get; set; }

        [Column("InsertUserId")]
        [Updatable(true,false)]
        [DisplayName("�إߤH��")]
        [Required]
        public decimal InsertUserId { get; set; }

        [Column("UpdateDateTime")]
        [DisplayName("��s���")]
        [Required]
        public DateTime UpdateDateTime { get; set; }

        [Column("UpdateUnitId")]
        [DisplayName("��s���")]
        [Required]
        public int UpdateUnitId { get; set; }

        [Column("UpdateUserId")]
        [DisplayName("��s�H��")]
        [Required]
        public decimal UpdateUserId { get; set; }

        [Column("IsDelete")]
        [DisplayName("�O�_�w�R��")]
        [Required]
        public bool IsDelete { get; set; }
    }

}
