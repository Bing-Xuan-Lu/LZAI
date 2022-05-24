using Newtonsoft.Json;
using PSTFrame.MVC;
using LZAI.MgrLib.Model.DB;
using LZAI.MgrLib.Service;
using LZAIMonitor.Areas.Mgr.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LZAI.Model.DB;
using LZAIMonitor.Controllers;
using PSTFrame.MVC.Filters;
using PSTFrame.MVC.Model;
using LZAIMonitor.Areas.Mgr.Models.OrgUnit;
using LZAIMonitor.Fillters;

namespace LZAIMonitor.Areas.Mgr.Controllers.Users
{
    public class UsersController : WebBaseController
    {
        public readonly MgrUsersService _mgrUsersService;
        public readonly VwMgrUsersService _vwMgrUsersService;
        public readonly MgrOrgUnitService _mgrOrgUnitService;
        public readonly MgrGroupService _mgrGroupService;
        public readonly MgrUserGroupService _mgrUserGroupService;
        const string Allow_EPA = ":|: 27"; //署權限
        public UsersController()
        {
            CreateDbContext();
            _mgrUsersService = new MgrUsersService(DBContext);
            _vwMgrUsersService = new VwMgrUsersService(DBContext);
            _mgrOrgUnitService = new MgrOrgUnitService(DBContext);
            _mgrGroupService = new MgrGroupService(DBContext);
            _mgrUserGroupService = new MgrUserGroupService(DBContext);
        }
        // GET: Mgr/Users
        [AuditEventFilter(DataType.MgrUsers, DataProcessType.Select)]
        public ActionResult Users10100()
        {
            UsersViewModel ViewModel = new UsersViewModel();
            SetSelectListToViewBag();
            return View(ViewModel);
        }
        /// <summary>
        /// 個人資料
        /// </summary>
        /// <returns></returns>
        public ActionResult Users30100()
        {
            UsersViewModel model = new UsersViewModel();
            model.DataList = _vwMgrUsersService.GetVwMgrUsersId(LoginUser.UserId).ToList();
            return View(model);
        }
        /// <summary>
        /// 個人資料查詢
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]//注意!! 未使用Ajax傳資料記得使用此Token
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult QueryUsersid(UsersViewModel.Filter Filter)
        {
            UsersViewModel model = new UsersViewModel();
            var datas = _vwMgrUsersService.GetVwMgrUsersId(LoginUser.UserId);
            model.DataList = Filter.Length > 0
             ? datas.Skip(Filter.Start).Take(Filter.Length).ToList()
             : datas.ToList();
            Filter.TotalCount = datas.Count();
           
            model.QueryFilter = Filter;
            return DataTablesJson(model.QueryFilter.Draw, model.QueryFilter.TotalCount,
                model.QueryFilter.TotalCount, model.DataList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]//注意!! 未使用Ajax傳資料記得使用此Token
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult Query(UsersViewModel.Filter ViewModel)
        {
            if (LoginUser.GroupName == "單位管理")
            {
                ViewModel.UnitId = LoginUser.UnitId;
            }
            IEnumerable<LZAI.MgrLib.Model.DB.VwMgrUsers> data = _vwMgrUsersService.GetVwMgrUsers(ViewModel);
            var result = new { data };
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        /// <summary>
        /// 查詢 (不含子單位)
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]//注意!! 未使用Ajax傳資料記得使用此Token
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult QueryNoSubUnit(UsersViewModel.Filter ViewModel)
        {
            //ViewModel.UnitId = LoginUser.CityId;
            IEnumerable<LZAI.MgrLib.Model.DB.VwMgrUsers> data = _vwMgrUsersService.GetVwMgrUsersNoSubUnit(ViewModel, LoginUser.LoginPermission);
            var result = new { data };
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ActionResult Users201A0()
        {
            UsersDocModel ViewModel = new UsersDocModel();
            string[] GroupIdList = new string[0];
            SetSelectListToViewBag(GroupIdList);
            return View(ViewModel);
        }

        /// <summary>
        /// 新增人員
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="DocModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]//使用Ajax傳資料記得使用此Token && $.ajax head 記得加入Token
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.MgrUsers, DataProcessType.Insert)]
        public ActionResult Insert(FormCollection collection, UsersDocModel DocModel)
        {
            ResultMessage = new MessageModel();
            string pass = collection["worp"];

            DocModel.mgrUsers.InsertDateTime = DateTime.Now;
            DocModel.mgrUsers.InsertUnitId = LoginUser.UnitId;
            DocModel.mgrUsers.InsertUserId = LoginUser.UserId;
            DocModel.mgrUsers.UpdateDateTime = DateTime.Now;
            DocModel.mgrUsers.UpdateUnitId = LoginUser.UnitId;
            DocModel.mgrUsers.UpdateUserId = LoginUser.UserId;
            ResultMessage = _mgrUsersService.Insert(DocModel.mgrUsers, DocModel.mgrGroup.GroupId, pass);
            dataKey = (int)DocModel.mgrUsers.UserId;
            return Json(ResultMessage);
        }


        public ActionResult Users201E0()
        {
            UsersDocModel ViewModel = new UsersDocModel();
            string[] GroupIdList = null;
            //使用者
            var User = _mgrUsersService.Get(LoginUser.UserId);
            //使用者群組
            var UserGroup = _mgrUserGroupService.GetByUserID(LoginUser.UserId);
            ViewModel.mgrUsers = User;
            if (UserGroup.Count() > 0)
            {
                GroupIdList = UserGroup.Select(x => x.GroupId).ToArray();
            }
            SetSelectListToViewBag(GroupIdList);
            return View(ViewModel);
        }
        /// <summary>
        /// 個人資料維護編輯
        /// </summary>
        /// <returns></returns>
        public ActionResult Users301E0()
        {
            UsersDocModel ViewModel = new UsersDocModel();

            //使用者
            var User = _mgrUsersService.Get(LoginUser.UserId);
            //使用者群組
            var UserGroup = _mgrUserGroupService.GetByUserID(LoginUser.UserId);
            ViewModel.mgrUsers = User;
            
            return View(ViewModel);
        }
        /// <summary>
        /// 更新人員
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="DocModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]//使用Ajax傳資料記得使用此Token && $.ajax head 記得加入Token
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.MgrUsers, DataProcessType.Update)]
        public ActionResult Update(FormCollection collection, UsersDocModel DocModel)
        {
            ResultMessage = new MessageModel();
            string pass = "";//不可改其他人密碼 關閉
            DocModel.mgrUsers.UpdateDateTime = DateTime.Now;
            DocModel.mgrUsers.UpdateUnitId = LoginUser.UnitId;
            DocModel.mgrUsers.UpdateUserId = LoginUser.UserId;
            DocModel.mgrUsers.InsertDateTime = DateTime.Now;
            dataKey = (int)DocModel.mgrUsers.UserId;
            ResultMessage = _mgrUsersService.Update(DocModel.mgrUsers, DocModel.mgrGroup.GroupId, pass);
            return Json(ResultMessage);
        }
        /// <summary>
        /// 個人資料更新
        /// </summary>
        /// <param name="DocModel"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]//使用Ajax傳資料記得使用此Token && $.ajax head 記得加入Token
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.MgrUsers, DataProcessType.Update)]
        public ActionResult UpdatePersonal(UsersDocModel DocModel)
        {
          

            ResultMessage = new MessageModel();
            string pass = "";//不可改其他人密碼 關閉
            DocModel.mgrUsers.UpdateDateTime = DateTime.Now;
            DocModel.mgrUsers.UpdateUnitId = LoginUser.UnitId;
            DocModel.mgrUsers.UpdateUserId = LoginUser.UserId;
            DocModel.mgrUsers.InsertDateTime = DateTime.Now;
            dataKey = (int)DocModel.mgrUsers.UserId;
            ResultMessage = _mgrUsersService.UpdatePersonal(DocModel.mgrUsers);
            if (ResultMessage.Status)
            {
                ResultMessage.Url = "";
            }

            return Json(ResultMessage);
        }






        [HttpPost]
        [AjaxValidateAntiForgeryToken]//使用Ajax傳資料記得使用此Token && $.ajax head 記得加入Token
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.MgrUsers, DataProcessType.Delete)]
        public ActionResult Delete(int UserId)
        {
            ResultMessage = new MessageModel();
            ResultMessage = _mgrUsersService.DeleteData(UserId);
            dataKey = UserId;
            return Json(ResultMessage);
        }

        /// <summary>
        /// 設定頁面要選擇的項目List
        /// </summary>
        private void SetSelectListToViewBag(string[] DefaultValue = null)
        {

                OrgUnitViewModel Unitmodel = new OrgUnitViewModel();
                Unitmodel.OrgUnitFilter.Search = LoginUser.UnitName;
                Unitmodel.OrgUnitFilter.UnitId = LoginUser.UnitId;
                ViewBag.UnitList = _mgrOrgUnitService.GetList(Unitmodel.OrgUnitFilter).Select(x =>
               new SelectListItem()
               {
                   Text = x.UnitName.ToString(),
                   Value = x.OrgUnitId.ToString()
               });

               // ViewBag.UnitList = _mgrOrgUnitService.GetList().Select(x =>
               //new SelectListItem()
               //{
               //    Text = x.UnitName.ToString(),
               //    Value = x.OrgUnitId.ToString()
               //});

            if (DefaultValue != null)
            {
                //新、修
                ViewBag.GroupList = _mgrGroupService.GetMgrGroupList().Select(x =>
                new SelectListItem()
                {
                    Text = x.GroupName.ToString(),
                    Value = x.GroupId.ToString(),
                    Selected = DefaultValue.Contains(x.GroupId.ToString())
                });
            }
            else
            {
                DefaultValue = new string[0];
                //For查詢
                ViewBag.GroupList = _mgrGroupService.GetMgrGroupList().Select(x =>
                new SelectListItem()
                {
                    Text = x.GroupName.ToString(),
                    Value = x.GroupName.ToString(),
                    Selected = DefaultValue.Contains(x.GroupId.ToString())
                });
            }


            //ViewBag.AddrCityList = _addrCityService.GetList().Select(x =>
            //    new SelectListItem()
            //    {
            //        Text = x.CityName.ToString(),
            //        Value = x.CityId.ToString()
            //    });
        }


        /// <summary>
        /// 修改密碼頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Users10200()
        {
            var Users = _mgrUsersService.Get(LoginUser.UserId);
            //檢查是否第一次修改密碼
            if(Users.IsNeedChangePW)
            {
                ViewData["IsLock"] = "Lock";
            }
            return View(Users);
        }

        /// <summary>
        /// 修改密碼 儲存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]//使用Ajax傳資料記得使用此Token && $.ajax head 記得加入Token
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult AlterPwd(FormCollection collection,MgrUsers users)
        {
            var _Users = _mgrUsersService.Get((int)users.UserId);
            _Users.OldPwd = collection["OldPwd"];
            _Users.NewPwd = collection["NewPwd"];
            _Users.NewPwdCheck = collection["NewPwdCheck"];
            ResultMessage = _mgrUsersService.UpdatePwd(_Users);
            if (ResultMessage.Status == true)
                ResultMessage.Url = Url.Content("~/Index");//回首頁
            return Json(ResultMessage);
        }

    }
}