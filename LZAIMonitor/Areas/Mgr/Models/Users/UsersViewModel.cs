using PSTFrame.MVC.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LZAI.MgrLib.Model.DB;
using LZAI.MgrLib.Service;

namespace LZAIMonitor.Areas.Mgr.Models.Users
{
    public class UsersViewModel
    {
        /// <summary>
        /// 單位下拉選項
        /// </summary>
        public List<SelectListItem> UnitList { get; set; }

        /// <summary>
        /// 權限選項
        /// </summary>
        public List<SelectListItem> GroupList { get; set; }

        /// <summary>
        /// 查詢條件
        /// </summary>
        public Filter QueryFilter { get; set; }

        /// <summary>
        /// 查詢結果
        /// </summary>
        public List<VwMgrUsers> DataList { get; set; }

        public class Filter: DataTablesParameters
        {
            [DisplayName("使用者姓名")]
            public string UserName { get; set; }

            [DisplayName("登入者帳號")]
            public string Account { get; set; }

            [DisplayName("群組名稱")]
            public string GroupName { get; set; }

            [DisplayName("單位ID")]
            public int UnitId { get; set; }
        }

        public UsersViewModel()
        {
            QueryFilter = new Filter();
            DataList = new List<VwMgrUsers>();
            UnitList = new List<SelectListItem>();
            GroupList = new List<SelectListItem>();
        }

    }

    /// <summary>
    /// 使用者(編輯)文件Model
    /// </summary>
    public class UsersDocModel
    {
        public MgrUsers mgrUsers { get; set; }
        public List<MgrUserGroup> mgrUserGroupList { get; set; }
        public MgrUserGroup mgrUserGroup { get; set; }
        public MgrGroup mgrGroup { get; set; }

        public UsersDocModel()
        {
            mgrUsers = new MgrUsers();
            mgrUserGroupList = new List<MgrUserGroup>();
            mgrUserGroup = new MgrUserGroup();
            mgrGroup = new MgrGroup();
    }
    }
}