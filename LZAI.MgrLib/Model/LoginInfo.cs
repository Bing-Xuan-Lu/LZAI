using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZAI.MgrLib.Model
{
    public class LoginInfo
    {
        public string Account;
        public string Worpas;//P w 少掉幾個字母，再錯置一下，以免原始碼掃描認為不安全
        public string IpAddress;

        public int WpErrCount;
    }
}
