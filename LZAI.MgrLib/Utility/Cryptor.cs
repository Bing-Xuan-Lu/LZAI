using PSTFrame.Security.Cryptography;
using PSTFrame.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LZAI.MgrLib.Utility
{
    /// <summary>
    /// 資料加解密
    /// </summary>
    public class Cryptor
    {
        private static string dataCryptIV = "8gWBRjnwgkk7s5fp";

        private static string registryCryptIV = "qSSfuZAhTRPqDI0q";
        private static string registryPath = @"SOFTWARE\PSTCOM\Relief"; //只能在SOFTWARE下


        private static string tMachineCode = SymmetricCryptor.GetHardwareKey(HardwareKeyType.Key1);

        #region Data Cryptor Key
        /// <summary>
        /// 取得資料加解密Key
        /// </summary>
        /// <returns></returns>
        public static string GetDataCryptorKey()
        {
            if (PSTFrame.Utility.CacheHelper.ItemExists("DataCryptorKey"))
                return PSTFrame.Utility.CacheHelper.Retrieve<string>("DataCryptorKey");

            string tEncDataKey = RegistryHelper.GetStringValue(Microsoft.Win32.Registry.LocalMachine
                , registryPath, "DataCryptorKey");

            if (String.IsNullOrEmpty(tEncDataKey))
                throw new Exception("未設定加解密金鑰");

            string tDataCryptorKey;

            SymmetricCryptor.DeCode(EncryptionAlgorithm.Rijndael
                , System.Security.Cryptography.CipherMode.CBC
                , registryCryptIV, tEncDataKey, tMachineCode, out tDataCryptorKey);
            return tDataCryptorKey;
        }

        public static void SetDataCryptorKey(string cryptorKey)
        {
            if (string.IsNullOrWhiteSpace(cryptorKey))
                throw new ArgumentNullException("未指定加解密金鑰");
            string tEncDataCryptorKey;

            SymmetricCryptor.EnCode(EncryptionAlgorithm.Rijndael
                , System.Security.Cryptography.CipherMode.CBC, registryCryptIV, cryptorKey, tMachineCode, out tEncDataCryptorKey);

            RegistryHelper.CreateSubKey(Microsoft.Win32.Registry.LocalMachine, registryPath);
            RegistryHelper.SetStringValue(Microsoft.Win32.Registry.LocalMachine, registryPath, "DataCryptorKey", tEncDataCryptorKey);

            //由於在64位元下,若為32位元程式, key存取位置為Software\Wow6432Node, 故增加相關處理
            if (RegistryHelper.IsSubKeyExists(Microsoft.Win32.Registry.LocalMachine, @"SOFTWARE\Wow6432Node"))
            {
                RegistryHelper.SetStringValue(Microsoft.Win32.Registry.LocalMachine, registryPath.Replace(@"SOFTWARE\", @"SOFTWARE\Wow6432Node\"), "DataCryptorKey", tEncDataCryptorKey);
            }
        }

        /// <summary>
        /// 字串加密
        /// </summary>
        /// <param name="source">待加密字串</param>
        /// <returns></returns>
        public static string DataEncode(string source)
        {
            if (source.Trim() == "")
                return "";

            string tDataCryptorKey = GetDataCryptorKey();
            string encodeStr;
            //HYEB8754hjfkg788
            //IJUH789nfjhd47jh
            if (!PSTFrame.Security.Cryptography.SymmetricCryptor.EnCode(
                  PSTFrame.Security.Cryptography.EncryptionAlgorithm.Rijndael
                , System.Security.Cryptography.CipherMode.CBC, dataCryptIV
                , source, tDataCryptorKey, out encodeStr))
                throw new Exception("Encode Fail!!");

            return encodeStr;
        }

        /// <summary>
        /// 字串解密
        /// </summary>
        /// <param name="source">待解密字串</param>
        /// <returns></returns>
        public static string DataDecode(string source)
        {
            if (source.Trim() == "")
                return "";

            string tDataCryptorKey = GetDataCryptorKey();

            string decodeStr;


            if (!PSTFrame.Security.Cryptography.SymmetricCryptor.DeCode(
                  PSTFrame.Security.Cryptography.EncryptionAlgorithm.Rijndael
                , System.Security.Cryptography.CipherMode.CBC, dataCryptIV
                , source, tDataCryptorKey, out decodeStr))
                throw new Exception("Decode Fail!!");

            return decodeStr;
        }
        #endregion
        /// <summary>
        /// 亂數產生密碼
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CreateRandomPassword()
        {
            int length = 8;
            string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ0123456789#%";
            Random randNum = new Random((int)DateTime.Now.Ticks);
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = _allowedChars[randNum.Next(_allowedChars.Length)];
            }
            return new string(chars);
        }

        /// <summary>
        /// 亂數產生密碼
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CreateRandomPasswordOnlyNumber()
        {
            int length = 8;
            string _allowedChars = "0123456789";
            Random randNum = new Random((int)DateTime.Now.Ticks);
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = _allowedChars[randNum.Next(_allowedChars.Length)];
            }
            return new string(chars);
        }
    }
}
