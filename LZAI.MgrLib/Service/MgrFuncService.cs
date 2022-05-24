using System;
using System.Collections.Generic;
using System.Data;
using LZAI.MgrLib.Model;
using LZAI.MgrLib.Model.DB;
using LZAI.MgrLib.Repository;
using PSTFrame.MVC.Model;
using System.Linq;
using PSTFrame.MVC;
using PSTFrame.Data;

namespace LZAI.MgrLib.Service
{
    public class MgrFuncService : BaseService<MgrFunc, int>
    {
        MgrFuncRepository _MgrFuncRepository;
        MgrFuncTypeRepository _MgrFuncTypeRepository;
        private IRepositoryContext DbContext;

        public MgrFuncService(IRepositoryContext context)
        {
            DbContext = context;
            _MgrFuncRepository = new MgrFuncRepository(context);
            _MgrFuncTypeRepository = new MgrFuncTypeRepository(context);
        }
        /// <summary>
        /// 查詢
        /// </summary>
        /// <returns></returns>
        public List<MgrFunc> GetList(int QueryFuncTypeId, string QueryFuncName)
        {
            return _MgrFuncRepository.GetList(QueryFuncTypeId, QueryFuncName).ToList();
        }

        /// <summary>
        /// 功能項目分類下拉選單
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MgrFuncType> GetFuncs()
        {
            return _MgrFuncTypeRepository.GetFuncs();
        }

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="oMgrFunc"></param>
        /// <param name="NaviList">可使用功能清單(勾選)</param>
        /// <returns></returns>
        public MessageModel Insert(MgrFunc oMgrFunc)
        {
            string Message = "";
            var result = new MessageModel();
            if (!CheckData(oMgrFunc, true, out Message))
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = Message
                };
            }
            DbContext.BeginTran();
            try
            {
                _MgrFuncRepository = new MgrFuncRepository(DbContext);
                //Query自己寫
                int FuncId = _MgrFuncRepository.Insert(oMgrFunc);
                DbContext.Commit();
                result = new MessageModel()
                {
                    Status = true,
                    Message = "儲存成功"
                };
            }
            catch (Exception ex)
            {
                DbContext.Rollback();
                result = new MessageModel()
                {
                    Status = true,
                    Message = $"儲存失敗，原因{ex.Message}"
                };
            }
            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="mgrGroup">The source.</param>
        /// <returns></returns>
        public MessageModel UpdateData(MgrFunc oMgrFunc)
        {
            var data = _MgrFuncRepository.Get(oMgrFunc.FuncId);
            var result = new MessageModel();
            if (data == null)
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = "資料不存在"
                };
            }
            data.FuncName = oMgrFunc.FuncName;
            data.FuncMemo = oMgrFunc.FuncName;
            data.FuncTypeId = oMgrFunc.FuncTypeId;
            data.IsAddFunc = oMgrFunc.IsAddFunc;
            data.IsModFunc = oMgrFunc.IsModFunc;
            data.IsCanFunc = oMgrFunc.IsCanFunc;
            data.IsQueFunc = oMgrFunc.IsQueFunc;
            data.IsOthPerm1 = oMgrFunc.IsOthPerm1;
            data.OthPerm1Name = oMgrFunc.OthPerm1Name;
            data.IsOthPerm2 = oMgrFunc.IsOthPerm2;
            data.OthPerm2Name = oMgrFunc.OthPerm2Name;
            data.IsOthPerm3 = oMgrFunc.IsOthPerm3;
            data.OthPerm3Name = oMgrFunc.OthPerm3Name;
            data.IsOthPerm4 = oMgrFunc.IsOthPerm4;
            data.OthPerm4Name = oMgrFunc.OthPerm4Name;
            data.IsOthPerm5 = oMgrFunc.IsOthPerm5;
            data.OthPerm5Name = oMgrFunc.OthPerm5Name;
            DbContext.BeginTran();
            try
            {
                _MgrFuncRepository = new MgrFuncRepository(DbContext);
                _MgrFuncRepository.Update(data);
                DbContext.Commit();
                result = new MessageModel()
                {
                    Status = true,
                    Message = "儲存成功"
                };
            }
            catch (Exception ex)
            {
                DbContext.Rollback();
                result = new MessageModel()
                {
                    Status = true,
                    Message = $"修改失敗，原因{ex.Message}"
                };
            }

            return result;
        }


        #region 資料檢查
        private bool CheckData(MgrFunc oMgrFunc, bool IsAdd, out string Message)
        {
            Message = "";
            if (string.IsNullOrEmpty(oMgrFunc.FuncName)) { Message = "請輸入功能名稱！！"; return false; }

            //功能名稱不可相同
            DataTable dt = new DataTable();//MgrFuncDao.GetFuncName(oMgrFunc, IsAdd);
            if (dt.Rows.Count > 0)
            {
                Message = "功能名稱重覆！！"; return false;
            }
            if (IsAdd)
            {
                if (string.IsNullOrEmpty(oMgrFunc.FuncTypeId.ToString())) { Message = "請選擇功能種類！！"; return false; }
            }

            if (oMgrFunc.IsOthPerm1 && oMgrFunc.OthPerm1Name == "")
            {
                Message = "請輸入其他權限1的權限說明!!!";
                return false;
            }
            if (oMgrFunc.IsOthPerm2 && oMgrFunc.OthPerm2Name == "")
            {
                Message = "請輸入其他權限2的權限說明!!!";
                return false;
            }
            if (oMgrFunc.IsOthPerm3 && oMgrFunc.OthPerm3Name == "")
            {
                Message = "請輸入其他權限3的權限說明!!!";
                return false;
            }
            if (oMgrFunc.IsOthPerm4 && oMgrFunc.OthPerm4Name == "")
            {
                Message = "請輸入其他權限4的權限說明!!!";
                return false;
            }
            if (oMgrFunc.IsOthPerm5 && oMgrFunc.OthPerm5Name == "")
            {
                Message = "請輸入其他權限5的權限說明!!!";
                return false;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 依Id取得資料
        /// </summary>
        /// <param name="serId">資料Id</param>
        /// <returns></returns>
        public MgrFunc GetById(int FuncId)
        {
            return _MgrFuncRepository.Get(FuncId);
        }


        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="VillageId">要刪除的資料Id</param>
        /// <returns></returns>
        public MessageModel DeleteData(int FuncId)
        {
            var data = _MgrFuncRepository.Get(FuncId);
            var result = new MessageModel();
            if (data == null)
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = "資料不存在"
                };
            }
            DbContext.BeginTran();
            try
            {
                _MgrFuncRepository.Delete(FuncId);
                DbContext.Commit();
                result = new MessageModel()
                {
                    Status = true,
                    Message = "刪除成功"
                };
            }
            catch (Exception ex)
            {
                DbContext.Rollback();
                result = new MessageModel()
                {
                    Status = true,
                    Message = $"刪除失敗，原因{ex.Message}"
                };
            }
            return result;
        }

        /// <summary>
        /// 取得MgrFuncType清單
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MgrFuncType> GetMgrFuncTypeList()
        {
            return _MgrFuncTypeRepository.GetList();
        }

        /// <summary>
        /// 依人員及功能項目找出權限狀況
        /// </summary>
        /// <param name="aUserId">人員ID</param>
        /// <param name="aFuncId">功能項目ID</param>
        /// <returns>權限陣列</returns>
        public Permission GetFuncPerm(decimal aUserId, int aFuncId)
        {
            DataTable dt = new DataTable();//MgrFuncDao.GetUserFuncPermission(aUserId, aFuncId);
            if (dt.Rows.Count == 0)
            {
                return new Permission { IsPermission = false }; //回傳沒有權限
            }
            Permission Perm = new Permission
            {
                IsExec = Convert.ToBoolean(dt.Rows[0]["IsExec"]),
                IsQuery = Convert.ToBoolean(dt.Rows[0]["IsQuery"]),
                IsAdd = Convert.ToBoolean(dt.Rows[0]["IsAdd"]),
                IsModify = Convert.ToBoolean(dt.Rows[0]["IsModify"]),
                IsCancel = Convert.ToBoolean(dt.Rows[0]["IsCancel"]),
                IsOthPerm1 = Convert.ToBoolean(dt.Rows[0]["IsOthPerm1"]),
                IsOthPerm2 = Convert.ToBoolean(dt.Rows[0]["IsOthPerm2"]),
                IsOthPerm3 = Convert.ToBoolean(dt.Rows[0]["IsOthPerm3"]),
                IsOthPerm4 = Convert.ToBoolean(dt.Rows[0]["IsOthPerm4"]),
                IsOthPerm5 = Convert.ToBoolean(dt.Rows[0]["IsOthPerm5"])
            };

            //Permission Perm = new Permission();
            //Perm.IsExec = Convert.ToBoolean(dt.Rows[0]["IsExec"].ToString());

            //有任一功能權限，則代表有整個權限
            if (Perm.IsExec || Perm.IsQuery || Perm.IsAdd || Perm.IsModify || Perm.IsCancel || Perm.IsOthPerm1
                || Perm.IsOthPerm2 || Perm.IsOthPerm3 || Perm.IsOthPerm4 || Perm.IsOthPerm5)
            {
                Perm.IsPermission = true;
            }

            return Perm;
        }

        /// <summary>
        /// 依人員及功能項目找出權限狀況
        /// </summary>
        /// <param name = "aUserId" >人員ID</param >
        /// < param name="aFuncId">功能項目ID</param>
        /// <param name="projId">The proj identifier.</param>
        /// <returns>權限陣列</returns>
        public Permission GetFuncPerm(Int64 aUserId, int aFuncId, long projId)
        {
            DataTable dt = new DataTable();//MgrFuncDao.GetUserFuncPermission(aUserId, aFuncId, projId);
            if (dt.Rows.Count == 0)
            {
                return new Permission { IsPermission = false }; //回傳沒有權限
            }

            Permission Perm = new Permission
            {
                IsExec = Convert.ToBoolean(dt.Rows[0]["IsExec"]),
                IsQuery = Convert.ToBoolean(dt.Rows[0]["IsQuery"]),
                IsAdd = Convert.ToBoolean(dt.Rows[0]["IsAdd"]),
                IsModify = Convert.ToBoolean(dt.Rows[0]["IsModify"]),
                IsCancel = Convert.ToBoolean(dt.Rows[0]["IsCancel"]),
                IsOthPerm1 = Convert.ToBoolean(dt.Rows[0]["IsOthPerm1"]),
                IsOthPerm2 = Convert.ToBoolean(dt.Rows[0]["IsOthPerm2"]),
                IsOthPerm3 = Convert.ToBoolean(dt.Rows[0]["IsOthPerm3"]),
                IsOthPerm4 = Convert.ToBoolean(dt.Rows[0]["IsOthPerm4"]),
                IsOthPerm5 = Convert.ToBoolean(dt.Rows[0]["IsOthPerm5"])
            };

            //有任一功能權限，則代表有整個權限
            if (Perm.IsExec || Perm.IsQuery || Perm.IsAdd || Perm.IsModify || Perm.IsCancel || Perm.IsOthPerm1
                || Perm.IsOthPerm2 || Perm.IsOthPerm3 || Perm.IsOthPerm4 || Perm.IsOthPerm5)
            {
                Perm.IsPermission = true;
            }

            return Perm;
        }

    }
}
