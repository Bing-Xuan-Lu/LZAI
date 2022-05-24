using System;
using System.Collections.Generic;
using System.Data;
using LZAI.MgrLib.Model.DB;
using LZAI.MgrLib.Repository;
using System.Linq;
using System.Data.SqlClient;
using PSTFrame.Data;
using PSTFrame.MVC.Model;
using PSTFrame.MVC;

namespace LZAI.MgrLib.Service
{
    public class MgrGrpFuncService : BaseService<MgrGrpFunc, int>
    {
        MgrGrpFuncRepository _mgrGrpFuncRepository;
        VwMgrGrpFuncRepository _VwMgrGrpFuncRepository;
        private IRepositoryContext DbContext;

        public MgrGrpFuncService(IRepositoryContext context)
        {
            DbContext = context;
            _mgrGrpFuncRepository = new MgrGrpFuncRepository(context);
            _VwMgrGrpFuncRepository = new VwMgrGrpFuncRepository(context);
        }

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="oMgrFunc"></param>
        /// <returns></returns>
        public MessageModel InsertData(MgrGrpFunc oMgrGrpFunc)
        {
            string Message = "";
            if (!CheckData(oMgrGrpFunc, out Message))
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = Message
                };
            }
            _mgrGrpFuncRepository.Insert(oMgrGrpFunc);
            var result = new MessageModel()
            {
                Status = true,
                Message = "儲存成功"
            };
            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="mgrGroup">The source.</param>
        /// <returns></returns>
        public MessageModel UpdateData(MgrGrpFunc oMgrGrpFunc)
        {
            var data = _mgrGrpFuncRepository.Get(oMgrGrpFunc.GrpFuncId);
            if (data == null)
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = "資料不存在"
                };
            }
            data.IsAdd = oMgrGrpFunc.IsAdd;
            data.IsModify = oMgrGrpFunc.IsModify;
            data.IsCancel = oMgrGrpFunc.IsCancel;
            data.IsQuery = oMgrGrpFunc.IsQuery;
            data.IsOthPerm1 = oMgrGrpFunc.IsOthPerm1;
            data.IsOthPerm2 = oMgrGrpFunc.IsOthPerm2;
            data.IsOthPerm3 = oMgrGrpFunc.IsOthPerm3;
            data.IsOthPerm4 = oMgrGrpFunc.IsOthPerm4;
            data.IsOthPerm5 = oMgrGrpFunc.IsOthPerm5;
            _mgrGrpFuncRepository.Update(data);

            var result = new MessageModel()
            {
                Status = true,
                Message = "儲存成功"
            };
            return result;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="VillageId">要刪除的資料Id</param>
        /// <returns></returns>
        public MessageModel DeleteData(int GrpFuncId)
        {
            var data = _mgrGrpFuncRepository.Get(GrpFuncId);
            if (data == null)
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = "資料不存在"
                };
            }

            data.UpdateDateTime = DateTime.Now;
            data.IsDelete = true;
            data.UpdateUserId = 1;
            data.UpdateUnitId = 1;

            _mgrGrpFuncRepository.Update(data);

            var result = new MessageModel()
            {
                Status = true,
                Message = "刪除成功",
            };
            return result;
        }

        /// <summary>
        /// 依Id取得資料
        /// </summary>
        /// <param name="serId">資料Id</param>
        /// <returns></returns>
        public MgrGrpFunc GetById(int FuncId)
        {
            return _mgrGrpFuncRepository.Get(FuncId);
        }

        /// <summary>
        /// 依Id取得該群組之功能、功能權限清單
        /// </summary>
        /// <param name="serId">資料Id</param>
        /// <returns></returns>
        public IEnumerable<VwMgrGrpFunc> GetGroupById(int GroupId)
        {
            return _VwMgrGrpFuncRepository.GetList("Where GroupId =@GroupId", new { @GroupId = GroupId });
        }
        #region 資料檢查
        private bool CheckData(MgrGrpFunc oMgrGrpFunc, out string Message)
        {
            Message = "";
            if (string.IsNullOrEmpty(oMgrGrpFunc.GrpFuncId.ToString())) { Message = "請輸入群組功能代碼！！"; return false; }
            if (string.IsNullOrEmpty(oMgrGrpFunc.GroupId.ToString())) { Message = "請輸入群組代碼！！"; return false; }
            if (string.IsNullOrEmpty(oMgrGrpFunc.FuncId.ToString())) { Message = "請輸入功能代碼！！"; return false; }
            

            return true;
        }
        #endregion
    }
}
