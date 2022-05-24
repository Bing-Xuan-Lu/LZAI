using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PSTFrame.MVC;
using PSTFrame.MVC.Gear;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Web;
using System.Web.Mvc;
using LZAI.MgrLib.Model.DataModel;
using LZAI.MgrLib.Model.DB;
using LZAI.MgrLib.Models.ViewModel;

namespace LZAIMonitor.Controllers
{
    public class WebBaseController : BaseController
    {
        protected PSTFrame.Data.Dapper.DapperContext DBContext { get; set; }

        protected Dictionary<string, string> ConfigSettings { get { return _Settings; } }

        private Dictionary<string, string> _Settings;

        /// <summary>
        /// 讀取web.config所有的AppKey 到 Dictionary
        /// </summary>
        private void ReadSettings()
        {
            _Settings = new Dictionary<string, string>();
            var authorSettings = ConfigurationManager.AppSettings;
            if (authorSettings.Count > 0)
            {
                foreach (var key in authorSettings.AllKeys)
                {
                    _Settings.Add(key, authorSettings[key]);
                }
            }
            //加密的key需decode
            string[] encryKeys = { "FileEncrytKey" };
            foreach (string key in encryKeys)
            {
                if (_Settings.ContainsKey(key))
                {
                    _Settings[key] = LZAI.MgrLib.Utility.Cryptor.DataDecode(_Settings[key]);
                }
            }

        }


        public virtual VwMgrUsers LoginUser { get => CurrentUser == null || CurrentUser.UserInfoDetail == null 
            ? null : JsonConvert.DeserializeObject<VwMgrUsers>(CurrentUser.UserInfoDetail); }

        /// <summary>
        /// 稽核紀錄欄位使用
        /// </summary>
        public virtual int dataKey { get; set; }
        /// <summary>
        /// 稽核紀錄欄位使用
        /// </summary>
        public virtual string subDataType { get; set; }
        /// <summary>
        /// 稽核紀錄欄位使用
        /// </summary>
        public virtual int subDataKey { get; set; }

        public WebBaseController()
        {
            ReadSettings();

        }

        /// <summary>
        /// 建立DB連線
        /// </summary>
        protected void CreateDbContext()
        {
            DBContext = new PSTFrame.Data.Dapper.DapperContext(System.Configuration.ConfigurationManager.ConnectionStrings["LZAI"]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonFile">DDLJson檔名</param>
        /// <param name="condition">條件</param>
        /// <param name="defaultValue">預設勾選項目</param>
        /// <returns></returns>
        public List<SelectListItem> GetJsonDDLarray(string jsonFile, string condition = "", string[] defaultValue = null)
        {
            return GetJsonDDL(jsonFile, "Text", "Value", condition, defaultValue);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonFile">DDLJson檔名</param>
        /// <param name="condition">條件</param>
        /// <param name="defaultValue">預設勾選項目</param>
        /// <returns></returns>
        public List<SelectListItem> GetJsonDDL(string jsonFile, string condition = "", string defaultValue = null)
        {

            return GetJsonDDL(jsonFile, "Text", "Value", condition, new string[1] { defaultValue });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonFile">DDLJson檔名</param>
        /// <param name="condition">條件</param>
        /// <param name="defaultValue">預設勾選項目</param>
        /// <returns></returns>
        public List<SelectListItem> GetJsonDDL(string jsonFile, string textField, string valueField, string condition = "", string[] defaultValue = null)
        {
            List<SelectListItem> item = new List<SelectListItem>();
            List<DDLModel> ddl = new List<DDLModel>();
            string DDLJsonFile = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/CodeTable/" + jsonFile);
            //  string jsonFilePath = Server.MapPath("~/TextFile/BusinessType.json");
            using (StreamReader r = new StreamReader(DDLJsonFile, System.Text.Encoding.Default))
            {
                string json = r.ReadToEnd();
                ddl = JsonConvert.DeserializeObject<List<DDLModel>>(json);
            }

            if (!string.IsNullOrWhiteSpace(condition))
            {
                item = ddl.Where(x => x.Condition == condition).Select(listData =>
                     new SelectListItem()
                     {
                         Text = listData.GetPropertyValue(textField).ToString(),
                         Value = listData.GetPropertyValue(valueField).ToString(),
                         Selected = defaultValue.Contains(listData.GetPropertyValue(valueField).ToString()) ? true : false
                     }).ToList();
            }
            else
            {
                item = ddl.Select(listData =>
                       new SelectListItem()
                       {
                           Text = listData.GetPropertyValue(textField).ToString(),
                           Value = listData.GetPropertyValue(valueField).ToString(),
                           Selected = defaultValue.Contains(listData.GetPropertyValue(valueField).ToString()) ? true : false
                       }).ToList();

            }

            return item;
        }
        public List<SelectListItem> GetJsonDDLTT(string JsonFile, string Condition = "", string DefaultValue = null)
        {
            List<SelectListItem> item = new List<SelectListItem>();
            List<DDLModel> ddl = new List<DDLModel>();
            string DDLJsonFile = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/CodeTable/" + JsonFile);
            //  string jsonFilePath = Server.MapPath("~/TextFile/BusinessType.json");
            using (StreamReader r = new StreamReader(DDLJsonFile, System.Text.Encoding.Default))
            {
                string json = r.ReadToEnd();
                ddl = JsonConvert.DeserializeObject<List<DDLModel>>(json);
            }

            if (!string.IsNullOrWhiteSpace(Condition))
            {
                item = ddl.Where(x => x.Condition == Condition).Select(listData =>
                     new SelectListItem()
                     {
                         Text = listData.Text,
                         Value = listData.Text,
                         Selected = listData.Text == DefaultValue ? true : false
                     }).ToList();
            }
            else
            {
                item = ddl.Select(listData =>
                       new SelectListItem()
                       {
                           Text = listData.Text,
                           Value = listData.Text,
                           Selected = listData.Text == DefaultValue ? true : false
                       }).ToList();

            }



            return item;
        }
        public List<SelectListItem> GetJsonDDLVV(string JsonFile, string Condition = "", string DefaultValue = null)
        {
            List<SelectListItem> item = new List<SelectListItem>();
            List<DDLModel> ddl = new List<DDLModel>();
            string DDLJsonFile = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/CodeTable/" + JsonFile);
            //  string jsonFilePath = Server.MapPath("~/TextFile/BusinessType.json");
            using (StreamReader r = new StreamReader(DDLJsonFile, System.Text.Encoding.Default))
            {
                string json = r.ReadToEnd();
                ddl = JsonConvert.DeserializeObject<List<DDLModel>>(json);
            }

            if (!string.IsNullOrWhiteSpace(Condition))
            {
                item = ddl.Where(x => x.Condition == Condition).Select(listData =>
                     new SelectListItem()
                     {
                         Text = listData.Value,
                         Value = listData.Value,
                         Selected = listData.Text == DefaultValue ? true : false
                     }).ToList();
            }
            else
            {
                item = ddl.Select(listData =>
                       new SelectListItem()
                       {
                           Text = listData.Value,
                           Value = listData.Value,
                           Selected = listData.Text == DefaultValue ? true : false
                       }).ToList();

            }



            return item;
        }
        /// <summary>
        /// 取得縣市
        /// </summary>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public List<SelectListItem> GetCity(string DefaultValue = null, string textField = "Text", string valueField = "Value")
        {
            return GetJsonDDL("CodeCity.json", textField, valueField, "", new string[1] { DefaultValue });
        }


        /// <summary>
        /// 取得行政區
        /// </summary>
        /// <param name="CityNo"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public List<SelectListItem> GetDistrict(string CityNo, string DefaultValue = null)
        {
            return GetJsonDDL("CodeDistrict.json", CityNo, DefaultValue);
        }

        /// <summary>
        /// 加密器
        /// </summary>
        protected AESCryptor TokenCryptor { get => (new AESCryptor("TjascVLo6riUBQc5", "VjT4JgI0nVjnmYjz", @"SOFTWARE\PSTCOM\WEIM")); }

        /// <summary>
        /// 將下載檔案資訊封裝在token
        /// </summary>
        /// <param name="data">完整檔案路徑及檔名</param>
        /// <param name="isEncrypted">檔案是否加密</param>
        /// <param name="timeout">幾秒後Timeout</param>
        /// <returns></returns>
        protected string GetUrlTokenString(string data, bool isEncrypted, int timeout = 120)
        {

            UrlToken urlToken = new UrlToken(TokenCryptor);

            return urlToken.GetToken(data, isEncrypted, timeout);
        }

        /// <summary>
        /// 判斷token是否還有效
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public bool IsTokenValid(IToken Token)
        {
            bool result = true;
            if (Token.ClientIP != PSTFrame.MVC.Helper.HelperClass.GetIPHelper() || Token.KeyTime.CompareTo(DateTime.Now) < 0)
            {
                result = false;
            }

            return result;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">上傳檔案</param>
        /// <param name="filePath">檔案絕對路徑</param>
        /// <param name="newFileName">新檔名，若不提供以原檔名儲存</param>
        /// <param name="IsEncrypt">true, 加密後檔名：檔名.副檔名.aes，原檔名檔案會刪除</param>
        /// <param name="encryptPass">加密金鑰</param>
        /// <returns>Message.ReturnData 回傳實際儲存檔案完整路徑及檔名</returns>
        protected MessageModel SaveFile(HttpPostedFileBase file, string filePath, string newFileName = "", bool IsEncrypt = false, string encryptPass = "Pstcom23393250")
        {
            MessageModel result = new MessageModel { Status = false, Message = "" };

            if (IsEncrypt && string.IsNullOrEmpty(encryptPass))
                throw new Exception("檔案加密需提供加密金錀");

            if (string.IsNullOrWhiteSpace(newFileName))
                newFileName = file.FileName;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string fullPathFile = Path.Combine(filePath, newFileName);
            string encryptFulllPathFile = string.Format("{0}.aes", fullPathFile);

            file.SaveAs(fullPathFile);

            result.ReturnData = fullPathFile;

            if (IsEncrypt)
            {
                FileCrytor.Encrypt(encryptPass, fullPathFile, encryptFulllPathFile);
                //加密完成後明碼檔案要刪除
                if (System.IO.File.Exists(fullPathFile) && System.IO.File.Exists(encryptFulllPathFile))
                {
                    System.IO.File.Delete(fullPathFile);
                }
                else
                {
                    throw new Exception("檔案儲存失敗");
                }
                result.ReturnData = encryptFulllPathFile;
            }
            return result;
        }





        public ActionResult GetFile(string token)
        {
            MessageModel fileResult = new MessageModel();
            UrlToken urlToken = new UrlToken(TokenCryptor);
            urlToken = (UrlToken)urlToken.TokenDeCode<UrlToken>(token);

            string fileEncrytKey = ConfigSettings["FileEncrytKey"];
            bool isTokenValid = IsTokenValid(urlToken);

            FileDataModel fileModel = JsonConvert.DeserializeObject<FileDataModel>(urlToken.Data.ToString());

            if (isTokenValid)
            {
                string fullFileName = Path.Combine(fileModel.FilePath, (fileModel.IsEncrypt ? fileModel.CryptedFileName : fileModel.RealFileName));
                FileInfo fileInfo = new FileInfo(fullFileName);

                return GetFile(fullFileName, fileModel.FileName, fileModel.IsEncrypt, fileEncrytKey);
            }

            return Content("<script>alert('token 無效')<script>");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullFileName">完整路徑及檔名</param>
        /// <param name="IsEncrypt"></param>
        /// <param name="encryptPass"></param>
        /// <returns></returns>
        protected FileResult GetFile(string fullFileName, string displayFileName, bool IsEncrypt = false, string encryptPass = "")
        {

            byte[] fileBytes = null;
            if (IsEncrypt && string.IsNullOrEmpty(encryptPass))
                encryptPass = ConfigSettings["FileEncrytKey"]; //throw new Exception("檔案加密需提供加密金錀");
            string newFileFullPath = fullFileName.Replace(".aes", "");


            if (IsEncrypt)
            {
                FileCrytor.Decrypt(encryptPass, fullFileName, newFileFullPath);
            }

            if (System.IO.File.Exists(newFileFullPath))
            {
                fileBytes = System.IO.File.ReadAllBytes(newFileFullPath);
            }

            if (fileBytes != null && IsEncrypt && System.IO.File.Exists(newFileFullPath) && System.IO.File.Exists(fullFileName) && fullFileName.IndexOf(".aes") > 0)
            {
                System.IO.File.Delete(newFileFullPath);
            }

            if (string.IsNullOrWhiteSpace(displayFileName))
                displayFileName = Path.GetFileName(displayFileName);


            return File(fileBytes, "Application/unknow", displayFileName);
        }
        /// <summary>
        /// 刪除暫存檔
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="Timeout"></param>
        protected void DeleteDueFile(string sourcePath, int Timeout = 30)
        {
            if (!string.IsNullOrEmpty(sourcePath) && Directory.Exists(sourcePath))
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(sourcePath);

                foreach (FileInfo file in di.GetFiles())
                {
                    TimeSpan ts1 = new TimeSpan(file.CreationTime.Ticks);
                    TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
                    TimeSpan ts = ts1.Subtract(ts2).Duration();

                    if (ts.Minutes > Timeout)
                        file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    TimeSpan ts1 = new TimeSpan(dir.CreationTime.Ticks);
                    TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
                    TimeSpan ts = ts1.Subtract(ts2).Duration();

                    if (ts.Minutes > Timeout)
                        dir.Delete(true);
                }
            }
        }

        /// <summary>
        /// 複製副本
        /// </summary>
        /// <returns></returns>
        protected string CopyDocument(string Docx, string FileName, string fileName)
        {
            string path = System.IO.Path.GetDirectoryName(fileName);
            string newDocx = System.IO.Path.GetFileName(fileName);
            newDocx = System.IO.Path.Combine(path, $"{FileName}_{DateTime.Now.ToString("HHmmss")}" + newDocx);

            CopyWordFile(Docx, System.IO.Path.Combine(path, newDocx));//複製文件
            return newDocx;
        }
        /// <summary>
        /// 複製檔案
        /// </summary>
        /// <param name="fromWordFile"></param>
        /// <param name="toWordFile"></param>
        private static void CopyWordFile(string fromWordFile, string toWordFile)//, out string templateText)
        {
            if (System.IO.File.Exists(toWordFile))
            {
                System.IO.File.Delete(toWordFile);
            }
            else
            {
                string path = System.IO.Path.GetDirectoryName(toWordFile);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            System.IO.File.Copy(fromWordFile, toWordFile);

        }
    }
}