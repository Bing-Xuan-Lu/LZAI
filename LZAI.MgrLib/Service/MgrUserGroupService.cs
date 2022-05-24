using System;
using System.Collections.Generic;
using System.Data;
using LZAI.MgrLib.Model.DB;
using LZAI.MgrLib.Repository;
using PSTFrame.Security.Cryptography;
using System.Data.SqlClient;
using System.Collections;
using System.Linq;
using PSTFrame.Data;
using PSTFrame.MVC.Model;
using BaseRepository = LZAI.MgrLib.Repository.BaseRepository;

namespace LZAI.MgrLib.Service
{
    public class MgrUserGroupService : BaseService<MgrUserGroup,int>
    {
        private MgrUserGroupRepository _MgeUserGroupRepo;
        private IRepositoryContext DbContext;

        public MgrUserGroupService()
        {
            DbContext = BaseRepository.GetContext();
            _MgeUserGroupRepo = new MgrUserGroupRepository(DbContext);
        }
        public MgrUserGroupService(IRepositoryContext context)
        {
            _MgeUserGroupRepo = new MgrUserGroupRepository(context);
        }
        #region 新增使用者群組權限
        public bool Insert(MgrUserGroup oMgrUserGroup, out string Message)
        {
            //CheckData
            if (!CheckData(oMgrUserGroup, out Message)) { return false; }
            DbContext.BeginTran();
            try
            {
                _MgeUserGroupRepo.Insert(oMgrUserGroup);//新增權限
                DbContext.Commit();
            }
            catch
            {
                DbContext.Rollback();
                throw;
            }
            return true;
        }

        public void Insert(MgrUserGroup oMgrUserGroup, ArrayList GroupIdList)
        {
            DbContext.BeginTran();
            try
            {   
                //砍掉
                _MgeUserGroupRepo.DeleteList(new { UserId = oMgrUserGroup.UserId });

                //重練
                foreach (int GroupId in GroupIdList)
                {
                    //oMgrUserGroup.GroupId = GroupId;
                    _MgeUserGroupRepo.Insert(oMgrUserGroup);
                }
                DbContext.Commit();
            }
            catch
            {
                DbContext.Rollback();
                throw;
            }

        }

        #endregion


        #region 群組功能代碼更新
        public bool Update(MgrUserGroup oMgrUserGroup, out string Message)
        {
            Message = "";
            //Check
            if (!CheckData(oMgrUserGroup, out Message)) { return false; }
            _MgeUserGroupRepo.Update(oMgrUserGroup);
            return true;
        }
        #endregion
        #region 取得清單
        public MgrUserGroup Get(int id)
        {
            return _MgeUserGroupRepo.Get(id);
        }
        #endregion

        #region 刪除權限
        public void Delete(MgrUserGroup oMgrUserGroup)
        {
            _MgeUserGroupRepo.Delete(oMgrUserGroup);
        }
        #endregion

        #region 藉由PK得群組權限資料
        /// <summary>
        /// 藉由PK 取得群組權限資料
        /// </summary>
        /// <param name="oMgrUserGroup">若查詢結果只有一筆資料，</param>
        /// <returns></returns>
        public bool GetDataByPk(MgrUserGroup oMgrUserGroup)
        {
            oMgrUserGroup = _MgeUserGroupRepo.Get(oMgrUserGroup.UserGrpId);//抓資料


            if (oMgrUserGroup == null) return false;

            return true;
        }
        #endregion

        #region 藉由使用者ID or  群組ID 取得群組權限資料
        /// <summary>
        /// 藉由使用者ID or  群組ID 取得群組權限資料
        /// </summary>
        /// <param name="oMgrUserGroup"></param>
        /// <returns></returns>
        public IEnumerable<MgrUserGroup> GetByUserID(int UserId)
        {
            return _MgeUserGroupRepo.GetDataByUserID(UserId).AsQueryable();//抓資料
        }
        #endregion

        #region 資料檢查
        private bool CheckData(MgrUserGroup oMgrUserGroup, out string Message)
        {
            Message = "";
            if (string.IsNullOrEmpty(oMgrUserGroup.UserGrpId.ToString())) { Message = "請輸入使用者群組功能代碼！！"; return false; }
            if (string.IsNullOrEmpty(oMgrUserGroup.UserId.ToString())) { Message = "請輸入使用者代碼！！"; return false; }
            if (string.IsNullOrEmpty(oMgrUserGroup.GroupId.ToString())) { Message = "請輸入群組功能代碼！！"; return false; }
            if (string.IsNullOrEmpty(oMgrUserGroup.InsertDateTime.ToString())) { Message = "請輸入建立日期！！"; return false; }
            if (string.IsNullOrEmpty(oMgrUserGroup.InsertUnitId.ToString())) { Message = "請輸入建立單位！！"; return false; }
            if (string.IsNullOrEmpty(oMgrUserGroup.InsertUserId.ToString())) { Message = "請輸入建立人員！！"; return false; }
            return true;
        }
        #endregion
        public void Delete(int UserId)
        {
            _MgeUserGroupRepo.Delete(UserId);
        }

    }
}
