using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZAI.MgrLib.Model
{
    public class CsvFileResult<T> where T : class
    {
        [DisplayName("檢核")]
        public string DataStatus { get; set; }

        public int RowNumber { get; set; }

        public T Data { get; set; }
    }
}
