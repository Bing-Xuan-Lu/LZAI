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

namespace LZAIMonitor.Areas.Mgr.Controllers.Func
{
    public class FuncController : WebBaseController
    {
        private readonly MgrFuncService _MgrFuncService;

        public FuncController()
        {
            CreateDbContext();
            _MgrFuncService = new MgrFuncService(DBContext);
        }


        #region 查詢/清單
        [AuditEventFilter(DataType.MgrFunc, DataProcessType.Select)]
        public ActionResult Func10100()
        {
            FuncViewModel viewModel = new FuncViewModel
            {
                DataList = new List<MgrFunc>()
            };
            SetSelectListToViewBag();
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
        public ActionResult Query(FuncViewModel.Filter queryFilter)
        {
            IEnumerable<MgrFunc> data = _MgrFuncService.GetList(queryFilter.QueryFuncTypeId, queryFilter.QueryFuncName);
            var result = new { data };
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult Func201A0()
        {
            var model = new MgrFunc();
            SetSelectListToViewBag();
            return View(model);
        }

        /// <summary>
        /// 新增儲存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.MgrFunc, DataProcessType.Insert)]
        public ActionResult Insert(LZAI.MgrLib.Model.DB.MgrFunc mgrFunc, FormCollection collection)
        {

            if (!ModelState.IsValid)
            {
                return Json(GetModelError());
            }

            mgrFunc.FuncMemo = mgrFunc.FuncName;
            mgrFunc.InsertDateTime = DateTime.Now;
            mgrFunc.InsertUnitId = LoginUser.UnitId;
            mgrFunc.InsertUserId = LoginUser.UserId;
            mgrFunc.UpdateDateTime = DateTime.Now;
            mgrFunc.UpdateUnitId = LoginUser.UnitId;
            mgrFunc.UpdateUserId = LoginUser.UserId;

            ResultMessage = _MgrFuncService.Insert(mgrFunc);
            dataKey = mgrFunc.FuncId;
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
        public ActionResult Func201E0(int FuncID)
        {
            var mgrFunc = _MgrFuncService.GetById(FuncID);
            SetSelectListToViewBag();
            return View(mgrFunc);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.MgrFunc, DataProcessType.Update)]
        public ActionResult Update(MgrFunc mgrFunc, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return Json(GetModelError());
            }
            mgrFunc.UpdateDateTime = DateTime.Now;
            mgrFunc.UpdateUnitId = LoginUser.UnitId;
            mgrFunc.UpdateUserId = LoginUser.UserId;
            dataKey = mgrFunc.FuncId;
            ResultMessage = _MgrFuncService.UpdateData(mgrFunc);
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
        [AuditEventFilter(DataType.MgrFunc, DataProcessType.Delete)]
        public ActionResult Delete(int serId)
        {
            dataKey = serId;
            var ResultMessage = _MgrFuncService.DeleteData(serId);
            if (ResultMessage.Status)
            {
                ResultMessage.Url = "";
            }
            return Json(ResultMessage);
        }

        #endregion

        /// <summary>
        /// 設定頁面要選擇的項目List
        /// </summary>
        private void SetSelectListToViewBag()
        {
            ViewBag.MgrFuncType = _MgrFuncService.GetMgrFuncTypeList().Select(x =>
                new SelectListItem()
                {
                    Text = x.FuncTypeName,
                    Value = x.FuncTypeId.ToString()
                });
            List<SelectListItem> PrmList = new List<SelectListItem>();
            PrmList.AddRange(
                new[]
                {
                    new SelectListItem() { Text = "新增權限",Value = "0",Selected=false},
                    new SelectListItem() { Text = "修改權限",Value = "0",Selected=false},
                    new SelectListItem() { Text = "刪除權限",Value = "0",Selected=false},
                    new SelectListItem() { Text = "查詢權限",Value = "0",Selected=false}
                });
            ViewBag.PrmList = PrmList;
        }


    }
}