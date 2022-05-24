using LZAI.Model.DB;
using PSTFrame.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZAI.Service.IService
{
    public interface IAuditEventService
    {
        MessageModel InsertAuditEvent(AuditEvent auditEvent);
        /// <summary>
        /// 條件查詢稽核記錄
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        IEnumerable<Vw_AuditEvent> GetSelectVw_AuditEvent(string Name, string SDate, string EDate);

        /// <summary>
        /// 查詢稽核記錄表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Vw_AuditEvent> GetVw_AuditEvent();
    }
}
