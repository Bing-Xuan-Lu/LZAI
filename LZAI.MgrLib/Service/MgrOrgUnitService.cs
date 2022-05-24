using System;
using System.Collections.Generic;
using System.Data;
using LZAI.MgrLib.Model.DB;
using LZAI.MgrLib.Repository;
using System.Data.SqlClient;
using PSTFrame.Data;
using PSTFrame.MVC;
using PSTFrame.MVC.Model;

namespace LZAI.MgrLib.Service
{
    public class MgrOrgUnitService : BaseService<MgrOrgUnit, int>
    {
        private MgrOrgUnitRepository _MgrOrgUnitRepo;
        private IRepositoryContext DbContext;

        public MgrOrgUnitService(IRepositoryContext context)
        {
            DbContext = context;
            _MgrOrgUnitRepo = new MgrOrgUnitRepository(context);
        }

        /// <summary>
        /// 新增資料
        /// </summary>
        public MessageModel Insertdata(MgrOrgUnit oMgrOrgUnit)
        {
            MessageModel ResultMessage = new MessageModel();
            int Tkey = 0;
            try
            {
                Tkey = _MgrOrgUnitRepo.Insert(oMgrOrgUnit); //新增資料
                if (Tkey == 0)
                {
                    throw new Exception("新增失敗");
                }
                else
                {
                    ResultMessage.Status = true;
                    ResultMessage.Message = "儲存成功";

                }
            }
            catch (Exception ex)
            {
                ResultMessage.Status = false;
                ResultMessage.Message = ex.Message;
            }
            return ResultMessage;
        }
        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="oMgrOrgUnit"></param>
        /// <returns></returns>
        public MessageModel Updatedata(MgrOrgUnit oMgrOrgUnit)
        {
            int result = 0;
            MessageModel ResultMessage = new MessageModel();
            try
            {
                var data = this.Get(oMgrOrgUnit.OrgUnitId);
                data.UnitName = oMgrOrgUnit.UnitName;
                data.UpdateDateTime = oMgrOrgUnit.UpdateDateTime;
                data.UpdateUnitId = oMgrOrgUnit.UpdateUnitId;
                data.UpdateUserId = oMgrOrgUnit.UpdateUserId;
                result = _MgrOrgUnitRepo.Update(data); //修改資料
                if (result == 0)
                {
                    throw new Exception("修改失敗");
                }
                else
                {
                    ResultMessage.Status = true;
                    ResultMessage.Message = "修改成功";
                }
            }
            catch (Exception ex)
            {
                ResultMessage.Status = false;
                ResultMessage.Message = ex.Message;
            }
            return ResultMessage;
        }
        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="oMgrOrgUnit"></param>
        /// <returns></returns>
        public MessageModel Deletedata(int OrgUnitId)
        {
            int result = 0;
            MessageModel ResultMessage = new MessageModel();
            try
            {
                result = _MgrOrgUnitRepo.Delete(OrgUnitId); //新增資料
                if (result == 0)
                {
                    throw new Exception("刪除失敗");
                }
                else
                {
                    ResultMessage.Status = true;
                    ResultMessage.Message = "刪除成功";
                }
            }
            catch (Exception ex)
            {
                ResultMessage.Status = false;
                ResultMessage.Message = ex.Message;
            }
            return ResultMessage;
        }
        /// <summary>
        /// 取得資料
        /// </summary>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public IEnumerable<MgrOrgUnit> GetList(dynamic Condition = null)
        {
            IEnumerable<MgrOrgUnit> List = new List<MgrOrgUnit>();
            if (Condition == null)
            {
                List = _MgrOrgUnitRepo.GetList("WHERE IsDelete=0");
            }
            else if (Condition.UnitId == 29)
            {
                List = _MgrOrgUnitRepo.GetList("WHERE IsDelete=0 and OrgUnitId <> 27 ");
            }
            else
            {
                Condition.Search = (string.IsNullOrWhiteSpace(Condition.Search)) ? "" : Condition.Search;
                List = _MgrOrgUnitRepo.GetList("WHERE IsDelete=0 and UnitName LIKE @Search ", new { @Search = string.Format("%{0}%", Condition.Search) });
            }

            return List;
        }
        /// <summary>
        /// 取得修改單筆資料
        /// </summary>
        /// <param name="OrgUnitId"></param>
        /// <returns></returns>
        public MgrOrgUnit Get(int OrgUnitId)
        {
            MgrOrgUnit Date = new MgrOrgUnit();
            Date = _MgrOrgUnitRepo.Get(OrgUnitId);
            if (Date == null)
            {
                throw new Exception("無法取得資料");
            }

            return Date;
        }
        #region 更新前檢查是否已存在同名(UnitName)不含自己
        private bool CheckUpdateDataIsNotDouble(MgrOrgUnit oMgrOrgUnit, out string Message)
        {
            Message = "";
            int Result = _MgrOrgUnitRepo.GetDataListByUnitNameNoIncludeSelf(oMgrOrgUnit);//抓資料
            if (Result > 0) { Message = "單位名稱已存在！！"; return false; }
            return true;
        }
        #endregion
        
    }
}
