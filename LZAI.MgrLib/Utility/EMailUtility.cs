using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LZAI.MgrLib.Utility
{
    public static partial class EMailUtility
    {
        /// <summary>
        /// 信件內容轉換
        /// </summary>
        /// <param name="result">需轉成Dictionary<string, string> </param>
        /// <param name="MailBody">需轉換內容 關鍵字%XXX%</param>
        /// <returns></returns>
        public static string MailBody(Dictionary<string, string> result ,string MailBody)
        {
            string res = "";

             res = System.Text.RegularExpressions.Regex.Replace(
                      MailBody, @"\%(?<n>.+?)\%", m =>
                       {
                           var n = m.Groups["n"].Value;
                           return result.ContainsKey(n) ? result[n] : "%" + n + "%";
                       });


            return res;
        }
        /// <summary>
        /// 信件內容轉換
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">轉換內容 entity</param>
        /// <param name="MailBody">需轉換內容 關鍵字%XXX%</param>
        /// <returns></returns>
        public static string MailBodyWithEntity<T>(this T entity, string MailBody)
        {
            string res = "";

            var dic = entity.GetType().GetProperties(System.Reflection.BindingFlags.Instance | BindingFlags.Public)
                         .ToDictionary(prop => prop.Name, prop => prop.GetValue(entity, null));

            Dictionary<string, string> result = dic.ToDictionary(kvp => kvp.Key, kvp => Convert.ToString(kvp.Value));


            res = System.Text.RegularExpressions.Regex.Replace(
                     MailBody, @"\%(?<n>.+?)\%", m =>
                     {
                         var n = m.Groups["n"].Value;
                         return result.ContainsKey(n) ? result[n] : "%" + n + "%";
                     });


            return res;
        }

    }
}
