using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LZAI.MgrLib.Utility
{
    class AddressUtility
    {

        public class ADDRESS
        {

            public ADDRESS(string address)
            {
                //  OrginalAddres address;
                ParseByRegex(address);
            }

            public string LongAddress { get; set; }
            public string ShortAddrrss { get; set; }
            public string City { get; set; }
            public string CityID { get; set; }
            public string District { get; set; }
            public string DistrictID { get; set; }
            public string ZIPCode { get; set; }
            public string Village { get; set; }
            public string VillageID { get; set; }
            public string NeighborNo { get; set; }
            public string Road { get; set; }
            public string Section { get; set; }
            public string Lane { get; set; }
            public string DivLane { get; set; }
            public string Alley { get; set; }
            public string DivAlley { get; set; }
            public string Num { get; set; }
            public string DivNum { get; set; }
            public string Room { get; set; }
            public string Floor { get; set; }
            public string DivFloor { get; set; }

            /// 是否符合pattern規範
            public bool IsParseSuccessed { get; set; }

            private void ParseByRegex(string address)
            {
                var pattern = @"(?<ZIPCode>(^\d{5}|^\d{3})?)?(?<CityId>\D+?[縣市])?(?<District>\D+?(市區|鎮區|鎮市|[鄉鎮市區]))?(?<Village>\D+?[村里])?(?<NeighborNo>\d+[鄰])?(?<Road>\D+?(村路|[路街道段]))?(?<Section>\D+?[段])?(?<Lane>\d+巷?)?(?<DivLane>(-|之)\d+?(巷))?(?<Alley>\d+弄?)?(?<DivAlley>(-|之)\d+?(弄))?(?<Num>\d+號?)?(?<DivNum>(-|之)\d+?([號]))?(?<Floor>\d+?[樓Ff])?(?<DivFloor>.+)?";

                Match match = Regex.Match(address, pattern);

                if (match.Success)
                {
                    this.IsParseSuccessed = true;

                    this.ZIPCode = match.Groups["ZIPCode"].ToString();
                    this.City = match.Groups["CityId"].ToString();
                    this.District = match.Groups["District"].ToString();
                    this.Village = match.Groups["Village"].ToString();
                    this.NeighborNo = Regex.Replace(match.Groups["NeighborNo"].ToString(), "[^0-9]", "");
                    this.Road = match.Groups["Road"].ToString();
                    this.Section = match.Groups["Section"].ToString();
                    this.Lane = Regex.Replace(match.Groups["Lane"].ToString(), "[^0-9]", "");
                    this.DivLane = Regex.Replace(match.Groups["DivLane"].ToString(), "[^0-9]", "");
                    this.Alley = Regex.Replace(match.Groups["Alley"].ToString(), "[^0-9]", "");
                    this.DivAlley = Regex.Replace(match.Groups["DivAlley"].ToString(), "[^0-9]", "");
                    this.Num = Regex.Replace(match.Groups["Num"].ToString(), "[^0-9]", "");
                    this.DivNum = Regex.Replace(match.Groups["DivNum"].ToString(), "[^0-9]", "");
                    this.Floor = Regex.Replace(match.Groups["Floor"].ToString(), "[^0-9]", "");
                    this.DivFloor = match.Groups["DivFloor"].ToString();
                }

            }
        }

    }
}
