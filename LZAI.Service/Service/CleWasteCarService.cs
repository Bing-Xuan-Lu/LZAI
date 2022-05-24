using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LZAI.Model.DB;
using LZAI.Repository.IRepository;
using LZAI.Repository.Repository;
using LZAI.Service.IService;
using PSTFrame.Data;
using PSTFrame.MVC;

namespace LZAI.Service.Service
{
    public class CleWasteCarService : ICleWasteCarService
    {
        protected IBaseRepository<CleWasteCar, int> _ICleWasteCarRepo;
        protected IBaseRepository<VwCleWasteCar, int> _IVwCleWasteCarRepo;
        protected IPublicCodeRepository _PublicCodeRepo;

        public CleWasteCarService(IBaseRepository<CleWasteCar, int> IcleWasteCarRepo
            , IBaseRepository<VwCleWasteCar, int> IVwCleWasteCarRepo
            , IPublicCodeRepository publicCodeRepo)
        {
            _ICleWasteCarRepo = IcleWasteCarRepo;
            _IVwCleWasteCarRepo = IVwCleWasteCarRepo;
            _PublicCodeRepo = publicCodeRepo;
        }

        public IEnumerable<CleWasteCar> GetCars()
        {
            return _ICleWasteCarRepo.GetList();
        }
        public IEnumerable<CleWasteCar> GetCleWasteCar()
        {
            IEnumerable<CleWasteCar> CleWasteCar;

            CleWasteCar = _ICleWasteCarRepo.GetList("WHERE IsDelete = 0 ");

            return CleWasteCar;


        }
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="Fac_Name"></param>
        /// <param name="Fac_Id"></param>
        /// <param name="BusinessNumNo"></param>
        /// <returns></returns>
        public IEnumerable<VwCleWasteCar> SerachCleWasteCar(string Fac_Name, string Fac_No, string CARNum)
        {
            string whereStr = "";
            whereStr += "WHERE IsDelete = 0   \r\n";
            if (!string.IsNullOrWhiteSpace(Fac_Name) && (Fac_Name != "-1"))
                whereStr += "and Fac_Name =  @Fac_Name  \r\n";
            if (!string.IsNullOrWhiteSpace(Fac_No)&& (Fac_No != "-1"))
                whereStr += "and Fac_No = @Fac_No \r\n";
            if (!string.IsNullOrWhiteSpace(CARNum) && (CARNum != "-1"))
                whereStr += "and (Head_No = @CARNum or Plate_No=@CARNum)\r\n";
            IEnumerable<VwCleWasteCar> VwCleWasteCarData = _IVwCleWasteCarRepo.GetList(
                whereStr, new
                {
                    @Fac_Name = Fac_Name,
                    @Fac_No = Fac_No,
                    @CARNum = CARNum
                });
            return VwCleWasteCarData.OrderBy(x => x.InsertDateTime);


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
                var data = _ICleWasteCarRepo.Get(Sno);
                data.IsDelete = true;
                data.UpdateDateTime = DateTime.Now;
                _ICleWasteCarRepo.Update(data);//刪除

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
        public MessageModel UpdateData(CleWasteCar cleWasteCar)
        {
            try
            {
                var data = _ICleWasteCarRepo.Get(cleWasteCar.SerNo);
                cleWasteCar.CopyPropertiesTo(data);
                _ICleWasteCarRepo.Update(data);
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
        public MessageModel InsertData(CleWasteCar cleWasteCar)
        {
            try
            {

                _ICleWasteCarRepo.Insert(cleWasteCar);
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
        public CleWasteCar GetById(int SerNo)
        {

            return _ICleWasteCarRepo.Get(SerNo);
        }


        public IEnumerable<PublicCode> GetPublicCode(string type)
        {
            string whereStr = "";
            whereStr += "WHERE IsDelete = 0   \r\n";
            if (!string.IsNullOrWhiteSpace(type))
                whereStr += "and CodeType = @type \r\n";
            IEnumerable<PublicCode> PublicCodeData = _PublicCodeRepo.GetList
                (
                whereStr, new 
                {
                @type = type, });
            return PublicCodeData;
        }
    }

}

