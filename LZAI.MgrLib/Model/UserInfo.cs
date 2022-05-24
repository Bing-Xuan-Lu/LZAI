using System;
using System.Collections.Generic;
using System.Linq;
using PSTFrame.MVC.Project;
using LZAI.MgrLib.Service;

namespace LZAI.MgrLib.Model
{
    /// <summary>
    /// 功能項目
    /// </summary>
    public enum ManageFuncs
    {
        None = 0,
        /// <summary>
        /// 帳號管理
        /// </summary>
        ManageAccount = 1,
        /// <summary>
        /// 群組管理
        /// </summary>
        ManageGroup = 2,
        /// <summary>
        /// 功能管理
        /// </summary>
        ManageFunc = 3,
        /// <summary>
        /// 單位管理
        /// </summary>
        ManageUnit = 4
    }

    public class UserInfo : BaseUserInfo<BaseManageFuncPerm>
    {
        protected override List<BaseManageFuncPerm> GetFuncPermissionsByCurrentUser()
        {
            if (CacheSession.Session["UserFuncPermissions"] == null)
            {
                var userService = new MgrUsersService();
                CacheSession.Session["UserFuncPermissions"] =
                    userService.GetGroupFuncsByUserId(UserId).Select(x => new BaseManageFuncPerm()
                    {
                        FuncId = x.FuncId,
                        IsExec = x.IsExec,
                        IsAdd = x.IsAdd,
                        IsModify = x.IsModify,
                        IsCancel = x.IsCancel,
                        IsQuery = x.IsQuery,
                        IsOthPerm1 = x.IsOthPerm1,
                        IsOthPerm2 = x.IsOthPerm2,
                        IsOthPerm3 = x.IsOthPerm3,
                        IsOthPerm4 = x.IsOthPerm4,
                        IsOthPerm5 = x.IsOthPerm5
                    }).ToList();
            }
            return (List<BaseManageFuncPerm>)CacheSession.Session["UserFuncPermissions"];
        }

        /// <summary>
        /// 自來水專案 -- 縣市權限ID
        /// </summary>
        public int CityID { get; set; }

        /// <summary>
        /// 自來水專案 -- 縣市名稱
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 自來水專案 --目前權限區分 (WRA：管理端、CityId：縣市端)
        /// </summary>
        public string Role { get; set; }
    }

}
