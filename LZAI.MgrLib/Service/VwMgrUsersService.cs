using System;
using System.Collections.Generic;
using System.Data;
using PSTFrame.Security.Cryptography;
using System.Data.SqlClient;
using System.Collections;
using System.Linq;
using LZAI.MgrLib.Model.DB;
using LZAI.MgrLib.Repository;
using PSTFrame.MVC.Model;

namespace LZAI.MgrLib.Service
{
    public class VwMgrUsersService : BaseService<VwMgrUsers, int>
    {
        private VwMgrUsersRepository _VwMgrUsersRespository;


        public VwMgrUsersService(PSTFrame.Data.IRepositoryContext Context)
        {
            _VwMgrUsersRespository = new VwMgrUsersRepository(Context);
        }

        public IEnumerable<VwMgrUsers> GetVwMgrUsers(dynamic Condition)
        {
            string UnitIdwhere = "";
            if (string.IsNullOrWhiteSpace(Condition.UserName))
                Condition.UserName = "";
            if (string.IsNullOrWhiteSpace(Condition.Account))
                Condition.Account = "";
            if (string.IsNullOrWhiteSpace(Condition.GroupName))
                Condition.GroupName = "";
            if(Condition.UnitId != 0 && Condition.UnitId != 27)
                UnitIdwhere = " AND UnitId LIKE @UnitId";
            if (Condition.UnitId != 0 && Condition.UnitId == 29)
                UnitIdwhere = " AND UnitId <> 27";
            return _VwMgrUsersRespository.GetList("WHERE UserName LIKE @UserName AND  Account LIKE @Account AND  GroupName LIKE @GroupName" + UnitIdwhere,
                    new
                    {
                        @UserName = string.Format("%{0}%", Condition.UserName),
                        @Account = string.Format("%{0}%", Condition.Account),
                        @GroupName = string.Format("%{0}%", Condition.GroupName),
                        @UnitId =Condition.UnitId
                    });
        }
        /// <summary>
        /// 個人資料抓UserId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IEnumerable<VwMgrUsers> GetVwMgrUsersId(int UserId)
        {

            return _VwMgrUsersRespository.GetList("WHERE UserId = @UserId " ,
                    new
                    {
                        @UserId = UserId
                    });
        }

        /// <summary>
        /// 不含子單位
        /// </summary>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public IEnumerable<VwMgrUsers> GetVwMgrUsersNoSubUnit(dynamic Condition,string LoginPermission)
        {
            string UnitIdwhere = "";
            if (string.IsNullOrWhiteSpace(Condition.UserName))
                Condition.UserName = "";
            if (string.IsNullOrWhiteSpace(Condition.Account))
                Condition.Account = "";
            if (string.IsNullOrWhiteSpace(Condition.GroupName))
                Condition.GroupName = "";
            if (Condition.UnitId != 0)
                UnitIdwhere = " AND UnitId = @UnitId";
            if (!string.IsNullOrWhiteSpace(LoginPermission))
            {
                if (LoginPermission == "A")
                {
                    UnitIdwhere += " AND LoginPermission !='C' ";
                }
                if (LoginPermission == "B")
                {
                    UnitIdwhere += " AND LoginPermission ='B' ";
                }
            }
                
            return _VwMgrUsersRespository.GetList("WHERE UserName LIKE @UserName AND  Account LIKE @Account AND  GroupName LIKE @GroupName" + UnitIdwhere,
                    new
                    {
                        @UserName = string.Format("%{0}%", Condition.UserName),
                        @Account = string.Format("%{0}%", Condition.Account),
                        @GroupName = string.Format("%{0}%", Condition.GroupName),
                        @UnitId = Condition.UnitId,
                       
                    });
        }

        /// <summary>
        /// 自然人憑證卡號取得帳戶資料
        /// </summary>
        /// <param name="certificationNo"></param>
        /// <returns></returns>
        public VwMgrUsers GetMgrUserFromGPKI(string certificationNo)
        {
            var vwMgrUsers = _VwMgrUsersRespository.GetList("where CertificationNo=@CertificationNo",
                new
                {
                    @CertificationNo = certificationNo
                }).ToList();
            return vwMgrUsers.Any() ? vwMgrUsers.First() : null;
        }

    }
}
