using LZAI.Model.DB;
using LZAI.Repository.IRepository;
using LZAI.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSTFrame.MVC.Model;
using PSTFrame.MVC;
using PSTFrame.Data.Dapper;

namespace LZAI.Service.Service
{
    public class CleGrantInfoService : ICleGrantInfoService
    {
        protected IBaseRepository<CleGrantInfo, int> _ICleGrantInfoRepo;
        protected IBaseRepository<CleWasteCar, int> _CleWasteCarRepo;
        protected IBaseRepository<Vw_CleGrantInfo, int> _IVwCleGrantInfoRepo;
        protected IBaseRepository<Vw_Addr, int> _IVw_AddrRepo;
        public CleGrantInfoService(IBaseRepository<CleGrantInfo, int> IcleGrantInfoRepo, IBaseRepository<CleWasteCar, int> CleWasteCarRepo, IBaseRepository<Vw_CleGrantInfo, int> Vw_CleGrantInfoRepo, IBaseRepository<Vw_Addr, int> Vw_AddrRepo)
        {
            _ICleGrantInfoRepo = IcleGrantInfoRepo;
            _CleWasteCarRepo = CleWasteCarRepo;
            _IVwCleGrantInfoRepo = Vw_CleGrantInfoRepo;
            _IVw_AddrRepo = Vw_AddrRepo;
        }
        public IEnumerable<CleGrantInfo> GetCleGrantInfo()
        {
            IEnumerable<CleGrantInfo> CleGrantInfo;

            CleGrantInfo = _ICleGrantInfoRepo.GetList("WHERE IsDelete = 0 ");

            return CleGrantInfo;

        }

        public IEnumerable<Vw_CleGrantInfo> GetVwCleGrantInfo()
        {
            return _IVwCleGrantInfoRepo.GetList();
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="Fac_Name"></param>
        /// <param name="Fac_Id"></param>
        /// <param name="BusinessNumNo"></param>
        /// <returns></returns>
        public IEnumerable<Vw_CleGrantInfo> SerachCleGrantInfo(string Fac_Name, string Fac_Id, string CleNumber)
        {
            string whereStr = "";
            whereStr += "WHERE IsDelete = 0   \r\n";
            if (!string.IsNullOrWhiteSpace(Fac_Name)&& Fac_Name!="-1")
                whereStr += "and Cle_Name =  @Fac_Name  \r\n";
            if (!string.IsNullOrWhiteSpace(Fac_Id) && Fac_Id != "-1")
                whereStr += "and Cle_No = @Fac_Id \r\n";
            if (!string.IsNullOrWhiteSpace(CleNumber)&& CleNumber!="-1")
                whereStr += "and CleNumber = @CleNumber \r\n";
            IEnumerable<Vw_CleGrantInfo> CleGrantInfoData = _IVwCleGrantInfoRepo.GetList(
                whereStr, new
                {
                    @Fac_Name = Fac_Name,
                    @Fac_Id = Fac_Id,
                    @CleNumber = CleNumber
                });
            return CleGrantInfoData.OrderBy(x => x.InsertDateTime);


        }
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="Sno"></param>
        /// <returns></returns>
        public MessageModel DeleteData(int Sno)
        {
            try
            {
                var data = _ICleGrantInfoRepo.Get(Sno);
                data.IsDelete = true;
                data.UpdateDateTime = DateTime.Now;
                _ICleGrantInfoRepo.Update(data);//刪除

                var result = new MessageModel()
                {
                    Status = true,
                    Message = "刪除成功"
                };
                return result;
            }
            catch (Exception ex)
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = $"刪除失敗，原因{ex.Message}"
                };
            }

        }
        /// <summary>
        /// 編輯
        /// </summary>
        /// <param name="Station"></param>
        /// <returns></returns>
        public MessageModel UpdateData(CleGrantInfo CleGrantInfo, List<int> car)
        {
            try
            {
                var data = _ICleGrantInfoRepo.Get(CleGrantInfo.CleId);
               
                CleGrantInfo.CopyPropertiesTo(data);
                data.CarSelectGrantInfo = string.Join(",", car);
                //data.Cle_No = CleGrantInfo.Cle_No;
                //data.BusinessNo = CleGrantInfo.BusinessNo;
                //data.Cle_Name = CleGrantInfo.Cle_Name;
                //data.UpdateDateTime = CleGrantInfo.UpdateDateTime;
                //data.UpdateUserId = CleGrantInfo.UpdateUserId;
                //data.CityId = CleGrantInfo.CityId;
                //data.DistrictId = CleGrantInfo.DistrictId;
                _ICleGrantInfoRepo.Update(data);
                var result = new MessageModel()
                {
                    Status = true,
                    Message = "儲存成功"
                };
                return result;
            }
            catch (Exception ex)
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = $"修改失敗，原因{ex.Message}"
                };
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="senserFix"></param>
        /// <param name="senserSnos"></param>
        /// <returns></returns>
        public MessageModel InsertData(CleGrantInfo CleGrantInfo, List<int> car)
        {
            try
            {
                CleGrantInfo.CarSelectGrantInfo = string.Join(",", car);
                _ICleGrantInfoRepo.Insert(CleGrantInfo);
                var result = new MessageModel()
                {
                    Status = true,
                    Message = "儲存成功"
                };
                return result;
            }
            catch (Exception ex)
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = $"新增失敗，原因{ex.Message}"
                };
            }
        }
        public CleGrantInfo GetById(int FuncId)
        {

            return _ICleGrantInfoRepo.Get(FuncId);
        }

        public IEnumerable<CleWasteCar> GetCleWasteCar()
        {
            IEnumerable<CleWasteCar> CleWasteCar;

            CleWasteCar = _CleWasteCarRepo.GetList("WHERE IsDelete = 0 ");

            return CleWasteCar;


        }
        /// <summary>
        /// 縣市選單
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vw_Addr> GetAdd()
        {
            return _IVw_AddrRepo.GetList();

        }


        public IEnumerable<Vw_Addr> GetSelectedDistrictList(int Cityid)
        {
            string whereStr = "WHERE";
           
                whereStr += " Cityid = @Cityid \r\n";
            
            IEnumerable<Vw_Addr> Vw_AddrData = _IVw_AddrRepo.GetList(
                whereStr, new
                {
                    @Cityid = Cityid
                  
                });
            return Vw_AddrData;
        }

    }

}
