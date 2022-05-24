using LZAI.Model.DB;
using LZAI.Repository.IRepository;
using LZAI.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSTFrame.MVC.Model;
using PSTFrame.MVC;
using PSTFrame.Data.Dapper;

namespace LZAI.Service.Service
{
    /// <summary>
    /// 稽核紀錄
    /// </summary>
    public class AuditEventService : IAuditEventService
    {
        protected IBaseRepository<AuditEvent, int> AuditEventRepo;
        protected IBaseRepository<Vw_AuditEvent, int> Vw_AuditEventRepo;

        public AuditEventService(IBaseRepository<AuditEvent, int> auditEventRepo, IBaseRepository<Vw_AuditEvent, int> vw_auditEventRepo)
        {
            AuditEventRepo = auditEventRepo;
            Vw_AuditEventRepo = vw_auditEventRepo;
        }

        /// <summary>
        /// 新增稽核記錄
        /// </summary>
        /// <param name="auditEvent"></param>
        /// <returns></returns>
        public MessageModel InsertAuditEvent(AuditEvent auditEvent)
        {
            try
            {
                AuditEventRepo.Insert(auditEvent);
                return new MessageModel()
                {
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new MessageModel()
                {
                    Status = false
                };
            }
            
        }

        public IEnumerable<Vw_AuditEvent> GetSelectVw_AuditEvent(string Name, string SDate, string EDate)
        {
            List<string> sqlWheres = new List<string>();

            if (!string.IsNullOrWhiteSpace(Name) & Name != "-1")
                sqlWheres.Add("UserName =  @UserName");
            if (!string.IsNullOrWhiteSpace(SDate) && !string.IsNullOrWhiteSpace(EDate))
            {
                EDate = EDate + " 23:59:59";
                sqlWheres.Add("EventDateTime between @SDate and @EDate");
            }
            IEnumerable<Vw_AuditEvent> Vw_AuditEvent = sqlWheres.Count > 0 ? Vw_AuditEventRepo.GetList(
               $"WHERE {string.Join(" AND ", sqlWheres)}", new{
                   @UserName = Name,
                     @SDate = SDate,
                   @EDate = EDate,
               }) : Vw_AuditEventRepo.GetList();


            return Vw_AuditEvent;
        }

        public IEnumerable<Vw_AuditEvent> GetVw_AuditEvent()
        {
            IEnumerable<Vw_AuditEvent> AuditEvent;

            AuditEvent = Vw_AuditEventRepo.GetList();
            return AuditEvent;
        }
    }

}
