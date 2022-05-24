using LZAI.Model.DB;
using PSTFrame.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZAI.Service.IService
{
  public  interface ICleGrantInfoService
    {
         IEnumerable<CleGrantInfo> GetCleGrantInfo();
         IEnumerable<Vw_CleGrantInfo> GetVwCleGrantInfo();
         IEnumerable<Vw_CleGrantInfo> SerachCleGrantInfo(string Cle_Name, string Cle_No, string CleNumber);
        MessageModel DeleteData(int CleId);
        MessageModel UpdateData(CleGrantInfo CleGrantInfo, List<int> carSelect);
        MessageModel InsertData(CleGrantInfo CleGrantInfo,List<int> carSelect);
        CleGrantInfo GetById(int FuncId);
        /// <summary>
        /// 運送車輛下拉單
        /// </summary>
        /// <returns></returns>
        IEnumerable<CleWasteCar> GetCleWasteCar();
        /// <summary>
        /// 縣市選單
        /// </summary>
        /// <returns></returns>
        IEnumerable<Vw_Addr> GetAdd();

        IEnumerable<Vw_Addr> GetSelectedDistrictList(int Cityid);
    }
}
