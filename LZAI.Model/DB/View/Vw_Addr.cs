using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZAI.Model.DB
{
   public class Vw_Addr
    {
        [Key]
        public int CityId { get; set; }


        public string CityName { get; set; }

        public int  DistrictId { get; set; }

        public string DistrictName { get; set; }
     
    }
}
