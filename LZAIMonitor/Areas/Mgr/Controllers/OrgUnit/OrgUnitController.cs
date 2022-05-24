using PSTFrame.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LZAI.MgrLib.Model.DB;
using LZAIMonitor.Areas.Mgr.Models.OrgUnit;
using LZAI.MgrLib.Service;
using LZAI.Model.DB;
using Newtonsoft.Json;
using LZAIMonitor.Controllers;
using LZAIMonitor.Fillters;
using PSTFrame.MVC.Filters;

namespace LZAIMonitor.Areas.Mgr.Controllers.OrgUnit
{
    public class OrgUnitController : WebBaseController
    {
        public readonly MgrOrgUnitService _mgrOrgUnitService;

        public OrgUnitController()
        {
            CreateDbContext();
            _mgrOrgUnitService = new MgrOrgUnitService(DBContext);
        }

        [AuditEventFilter(DataType.MgrOrgUnit, DataProcessType.Select)]
        public ActionResult OrgUnit10100()
        {
            OrgUnitViewModel ViewModel = new OrgUnitViewModel();
            return View(ViewModel);
        }

        /// <summary>
        /// 條件查詢
        /// </summary>
        /// <param name="Unitmodel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        public ActionResult Query(OrgUnitViewModel Unitmodel)
        {
            var data = _mgrOrgUnitService.GetList(Unitmodel.OrgUnitFilter).ToList();
            var result = new { data };
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ActionResult OrgUnit20100Partial()
        {
            MgrOrgUnit model = new MgrOrgUnit();
            return PartialView(model);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult OrgUnit201A0()
        {
            MgrOrgUnit model = new MgrOrgUnit();

            return View(model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public ActionResult OrgUnit201E0(int OrgUnitId)
        {
            MgrOrgUnit Date = new MgrOrgUnit();
            Date = _mgrOrgUnitService.Get(OrgUnitId);
            return View(Date);
        }
        /// <summary>
        /// 新增儲存
        /// </summary>
        /// <param name="UnitName"></param>
        /// <returns></returns>
        [HttpPost]
        //[AjaxValidateAntiForgeryToken]//使用Ajax傳資料記得使用此Token && $.ajax head 記得加入Token
        [AjaxValidateAntiForgeryToken]
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.MgrOrgUnit, DataProcessType.Insert)]
        public ActionResult Insert(string UnitName)
        {
            ResultMessage = new MessageModel();
            MgrOrgUnit Unit = new MgrOrgUnit();

            Unit.UnitName = UnitName;
            Unit.DispOrder = 9;
            Unit.IsDelete = false;
            Unit.InsertDateTime = DateTime.Now;
            Unit.InsertUnitId = LoginUser.UnitId;
            Unit.InsertUserId = LoginUser.UserId;
            Unit.UpdateDateTime = DateTime.Now;
            Unit.UpdateUnitId = LoginUser.UnitId;
            Unit.UpdateUserId = LoginUser.UserId;

            ResultMessage = _mgrOrgUnitService.Insertdata(Unit);
            dataKey = Unit.OrgUnitId;
            return Json(ResultMessage);
        }
        /// <summary>
        /// 編輯儲存
        /// </summary>
        /// <param name="UnitName"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]//注意!! 未使用Ajax傳資料記得使用此Token
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.MgrOrgUnit, DataProcessType.Update)]
        public ActionResult Update(MgrOrgUnit Data)
        {
            ResultMessage = new MessageModel(); 
            Data.UpdateDateTime = DateTime.Now;
            Data.UpdateUnitId = LoginUser.UnitId;
            Data.UpdateUserId = LoginUser.UserId;
            dataKey = Data.OrgUnitId;
            ResultMessage = _mgrOrgUnitService.Updatedata(Data);

            return Json(ResultMessage);
        }
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="UnitName"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]//使用Ajax傳資料記得使用此Token && $.ajax head 記得加入Token
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AuditEventFilter(DataType.MgrOrgUnit, DataProcessType.Delete)]
        public ActionResult Delete(int OrgUnitId)
        {
            ResultMessage = new MessageModel();
            dataKey = OrgUnitId;
            ResultMessage = _mgrOrgUnitService.Deletedata(OrgUnitId);
            return Json(ResultMessage);
        }

    }
}