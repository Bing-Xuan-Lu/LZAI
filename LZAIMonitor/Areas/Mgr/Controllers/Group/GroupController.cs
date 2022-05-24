using Newtonsoft.Json;
using PSTFrame.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LZAI.MgrLib.Model.DB;
using LZAI.MgrLib.Service;
using LZAI.Model.DB;
using PSTFrame.MVC.Helper;
using LZAIMonitor.Areas.Mgr.Models;
using LZAIMonitor.Controllers;
using LZAIMonitor.Fillters;
using PSTFrame.MVC.Filters;

namespace LZAIMonitor.Areas.Mgr.Controllers.Group
{
    public class GroupController : WebBaseController
    {
        private readonly MgrGroupService _MgrGroupService;

        public GroupController()
        {
            CreateDbContext();
            _MgrGroupService = new MgrGroupService(DBContext);
        }


        #region 查詢/清單
        [AuditEventFilter(DataType.MgrGroup, DataProcessType.Select)]
        public ActionResult Group10100()
        {
            GroupViewModel viewModel = new GroupViewModel
            {
                DataList = new List<MgrGroup>()
            };
            return View(viewModel);
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="queryFilter">The query filter.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult Query(GroupViewModel.Filter queryFilter)
        {
            IEnumerable<MgrGroup> data = _MgrGroupService.GetList(queryFilter.QueryGroupName);
            var result = new{data};
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult Group201A0()
        {
            var model = new MgrGroup();
            return View(model);
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [ValidateInput(false)]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.MgrGroup, DataProcessType.Insert)]
        public ActionResult Insert(LZAI.MgrLib.Model.DB.MgrGroup mgrGroup)
        {
            if (!ModelState.IsValid)
            {
                return Json(GetModelError());
            }

            mgrGroup.InsertDateTime = DateTime.Now;
            mgrGroup.InsertUnitId = LoginUser.UnitId;
            mgrGroup.InsertUserId = LoginUser.UserId;
            mgrGroup.UpdateDateTime = DateTime.Now;
            mgrGroup.UpdateUnitId = LoginUser.UnitId;
            mgrGroup.UpdateUserId = LoginUser.UserId;

            ResultMessage = _MgrGroupService.InsertData(mgrGroup);
            dataKey = mgrGroup.GroupId;
            if (ResultMessage.Status)
            {
                ResultMessage.Url = "";
            }
            return Json(ResultMessage);
        }
        #endregion

        #region 編輯
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public ActionResult Group201E0(int SerID)
        {
            var mgrGroup = _MgrGroupService.GetById(SerID);
            return View(mgrGroup);
        }

        /// <summary>
        /// 修改儲存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.MgrGroup, DataProcessType.Update)]
        public ActionResult Update(MgrGroup mgrGroup)
        {
            if (!ModelState.IsValid)
            {
                return Json(GetModelError());
            }

            mgrGroup.UpdateDateTime = DateTime.Now;
            mgrGroup.UpdateUnitId = LoginUser.UnitId;
            mgrGroup.UpdateUserId = LoginUser.UserId;

            ResultMessage = _MgrGroupService.UpdateData(mgrGroup);
            dataKey = mgrGroup.GroupId;
            if (ResultMessage.Status)
            {
                ResultMessage.Url = "";
            }

            return Json(ResultMessage);
        }


        #endregion

        #region 刪除
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="serId"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.MgrGroup, DataProcessType.Delete)]
        public ActionResult Delete(int serId)
        {
            MgrGroup mgrGroup = new MgrGroup();
            mgrGroup.GroupId = serId;
            mgrGroup.UpdateDateTime = DateTime.Now;
            mgrGroup.UpdateUnitId = LoginUser.UnitId;
            mgrGroup.UpdateUserId = LoginUser.UserId;
            var ResultMessage = _MgrGroupService.DeleteData(serId);
            dataKey = serId;
            if (ResultMessage.Status)
            {
                ResultMessage.Url = "";
            }
            return Json(ResultMessage);
        }

        #endregion     

        [HttpPost]
        public ActionResult GotoParam(int GroupId)
        {
            TempData["GrpId"] = GroupId;
            return Json(new { redirectUrl = Url.Action("GrpFunc10100", "GrpFunc" )});
        }
    }
}