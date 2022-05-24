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

namespace LZAIMonitor.Areas.Mgr.Controllers.GrpFunc
{
    public class GrpFuncController : WebBaseController
    {
        private readonly MgrGrpFuncService _MgrGrpFuncService;
        private readonly MgrFuncService _MgrFuncService;

        public GrpFuncController()
        {
            CreateDbContext();
            _MgrGrpFuncService = new MgrGrpFuncService(DBContext);
            _MgrFuncService = new MgrFuncService(DBContext);
        }


        #region 查詢/清單
        public ActionResult GrpFunc10100()
        {
            GrpFuncViewModel viewModel = new GrpFuncViewModel
            {
                DataList = new List<VwMgrGrpFunc>()
            };
            if (TempData["GrpId"] != null)
            {
                //ViewData[GrpId] for 101F0 預先帶入參數
                TempData["_GrpId"] = TempData["GrpId"];
                ViewData["GroupId"] = int.Parse(TempData["GrpId"].ToString());
            }
            else
            {
                //F5刷新時Temp參數失效，返回群組管理
                return Redirect("../Group/Group10100");
            }
            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult Query()
        {
            int GroupId = 0;
            //TempData[_GrpId] for 10110 進入頁面自動查詢
            GroupId = (TempData["_GrpId"] != null) ? 
                int.Parse(TempData["_GrpId"].ToString()) : 0;
            IEnumerable<VwMgrGrpFunc> data = _MgrGrpFuncService.GetGroupById(GroupId);
            var result = new { data };
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult GrpFunc201A0(string QueryGroupId)
        {
            var model = new MgrGrpFunc();
            SetSelectListToViewBag();
            model.GroupId = int.Parse(QueryGroupId);
            return View(model);
        }

        /// <summary>
        /// 新增儲存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [ValidateInput(false)]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult Insert(LZAI.MgrLib.Model.DB.MgrGrpFunc mgrGrpFunc)
        {
            if (!ModelState.IsValid)
            {
                return Json(GetModelError());
            }

            mgrGrpFunc.InsertDateTime = DateTime.Now;
            mgrGrpFunc.InsertUnitId = LoginUser.UnitId;
            mgrGrpFunc.InsertUserId = LoginUser.UserId;
            mgrGrpFunc.UpdateDateTime = DateTime.Now;
            mgrGrpFunc.UpdateUnitId = LoginUser.UnitId;
            mgrGrpFunc.UpdateUserId = LoginUser.UserId;
            ResultMessage = _MgrGrpFuncService.InsertData(mgrGrpFunc);
            if (ResultMessage.Status)
            {
                //結束返回參數 GroupId 自動查詢
                TempData["_GrpId"] = mgrGrpFunc.GroupId;
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
        public ActionResult GrpFunc201E0(int SerID)
        {
            var mgrGrpFunc = _MgrGrpFuncService.GetById(SerID);
            SetSelectListToViewBag();
            return View(mgrGrpFunc);
        }

        /// <summary>
        /// 編輯儲存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult Update(MgrGrpFunc mgrGrpFunc)
        {
            if (!ModelState.IsValid)
            {
                return Json(GetModelError());
            }

            mgrGrpFunc.UpdateDateTime = DateTime.Now;
            mgrGrpFunc.UpdateUnitId = LoginUser.UnitId;
            mgrGrpFunc.UpdateUserId = LoginUser.UserId;

            ResultMessage =  _MgrGrpFuncService.UpdateData(mgrGrpFunc);
            if (ResultMessage.Status)
            {
                //結束返回參數 GroupId 自動查詢
                TempData["_GrpId"] = mgrGrpFunc.GroupId;
                ResultMessage.Url = "";
            }

            return Json(ResultMessage);
        }


        #endregion

        #region 刪除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="serId"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult Delete(int serId)
        {
            MgrGrpFunc mgrGrpFunc = new MgrGrpFunc();
            mgrGrpFunc.GrpFuncId = serId;
            var ResultMessage = _MgrGrpFuncService.DeleteData(serId);
            if (ResultMessage.Status)
            {
                //結束返回參數 GroupId 自動查詢
                TempData["_GrpId"] = Request.Form["GroupId"];
                ResultMessage.Url = "";
            }
            return Json(ResultMessage);
        }

        #endregion

        /// <summary>
        /// 其他權限判斷(ajax)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetFuncParam(int FuncId)
        {
            MgrFunc mgrFuncs = _MgrFuncService.GetById(FuncId);
            return Json(mgrFuncs);
        }

        
        /// <summary>
        /// 設定頁面要選擇的項目List
        /// </summary>
        private void SetSelectListToViewBag()
        {
            ViewBag.GetFuncs = _MgrFuncService.GetFuncs().Select(x =>
                new SelectListItem()
                {
                    Text = IsNull(x.FuncTypeName).ToString(),
                    Value = IsNull(x.FuncTypeId.ToString())
                });
        }

        public string IsNull(string data)
        {
            if (data == null)
            {
                return "";
            }
            else
            {
                return data;
            }
        }
    }
}