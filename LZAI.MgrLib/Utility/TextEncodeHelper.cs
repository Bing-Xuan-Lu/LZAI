namespace LZAI.MgrLib.Utility
{
    public static class TextEncodeHelper
    {
        /// <summary>
        /// 將字串內NCR格式文字(&#NNNN;)轉換為Unicode
        /// </summary>
        /// <param name="s">要處理的字串</param>
        /// <returns></returns>
        public static string FromNCR(string s)
        {
            if (s == null)
                return s;
            foreach (System.Text.RegularExpressions.Match m 
                in System.Text.RegularExpressions.Regex.Matches(s,"&#(?<ncr>\\d+?);")) 
                s = s.Replace(m.Value, 
                    char.ConvertFromUtf32(int.Parse(m.Groups["ncr"].Value)));
            return s;
        }
    }
}
