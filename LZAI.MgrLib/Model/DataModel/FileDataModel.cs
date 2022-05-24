using PSTFrame.MVC.Model;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LZAI.MgrLib.Models.ViewModel
{

    public class FileDataModel
    {
        
        [DisplayName("SerId")]
        public long SerId { get; set; }

        
        [DisplayName("資料Guid")]
        
        public Guid DocGuid { get; set; }

        
        [DisplayName("主文件Id")]
        
        public long DocId { get; set; }

        
        [DisplayName("文件類別")]        
        public string AttachCode { get; set; }

        
        [DisplayName("檔案類別")]        
        public string FileType { get; set; }

        
        [DisplayName("檔名%#顯示用#%")]        
        public string FileName { get; set; }

        
        [DisplayName("實際檔名稱")]        
        public string RealFileName { get; set; }

        
        [DisplayName("加密後檔名")]        
        public string CryptedFileName { get; set; }

        
        [DisplayName("FilePath")]        
        public string FilePath { get; set; }

        
        [DisplayName("檔案連結")]        
        public string FileUrl { get; set; }

        
        [DisplayName("備註")]
        public string Remark { get; set; }

        
        [DisplayName("檔案是否加密")]
        
        public bool IsEncrypt { get; set; }

    }
}
