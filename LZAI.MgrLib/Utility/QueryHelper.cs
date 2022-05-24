using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PSTFrame.Data.Schema;

namespace LZAI.MgrLib.Utility
{
    public static class QueryHelper
    {
        /// <summary>
        /// 取得要查詢的查詢條件
        /// </summary>
        /// <returns></returns>
        public static NameValueCollection GetQueryValues<T>(T data)
        {
            var queryValues = new NameValueCollection();
            var propertyInfos = data.GetType().GetProperties();

            foreach (var propertyInfo in propertyInfos)
            {
                //判斷Class是否有DisplayName，有的話視為查詢條件
                //TODO:可以在設計的更好QQ
                //與標題交叉比對(LINQ)
                if (propertyInfo.CustomAttributes.Any())
                {
                    var Value = propertyInfo.GetValue(data, null);
                    if (Value == null || (propertyInfo.PropertyType == typeof(Int32) && (int)Value == 0))
                        continue;
                    queryValues.Add(propertyInfo.GetColumnName(), propertyInfo.Name);
                }
            }
            return queryValues;
        }
        private static string GetDisplayName(this MemberInfo memberInfo)
        {
            var titleName = string.Empty;

            //Try get DisplayName
            var attribute = memberInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault();
            if (attribute != null)
            {
                titleName = (attribute as DisplayNameAttribute).DisplayName;
            }
            //If no DisplayName
            else
            {
                //Pass Display
                titleName = "";//memberInfo.Name;
            }

            return titleName;
        }
        private static string GetColumnName(this MemberInfo memberInfo)
        {
            var titleName = string.Empty;

            //Try get DisplayName
            var attribute = memberInfo.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault();
            if (attribute != null)
            {
                titleName = (attribute as ColumnAttribute).Name;
            }
            //If no DisplayName
            else
            {
                //Pass Display
                titleName = "";//memberInfo.Name;
            }

            return titleName;
        }

        /// <summary>
        /// NameValueCollection迴圈時轉入KeyValuePair
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, string>> ConvertToKVP(this NameValueCollection source)
        {
            return source.AllKeys.SelectMany(
                source.GetValues,
                (k, v) => new KeyValuePair<string, string>(k, v));
        }
    }
}
