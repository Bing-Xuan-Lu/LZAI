using LZAI.Model.DB;
using PSTFrame.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZAI.Service.IService
{
    public interface ICleWasteCarService
    {
        IEnumerable<CleWasteCar> GetCars();
        IEnumerable<CleWasteCar> GetCleWasteCar(); 
        IEnumerable<VwCleWasteCar> SerachCleWasteCar(string Fac_Name, string Fac_No, string CARNum);
        MessageModel DeleteData(int CleId);
        MessageModel UpdateData(CleWasteCar cleWasteCar);
        MessageModel InsertData(CleWasteCar cleWasteCar);
        CleWasteCar GetById(int FuncId);
        IEnumerable<PublicCode> GetPublicCode(string type); 

        
        
    }
}
