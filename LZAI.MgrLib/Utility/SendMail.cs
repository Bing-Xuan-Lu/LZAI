using PSTFrame.MVC.Model;
using LZAI.MgrLib.Model.DB;
using LZAI.MgrLib.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using PSTFrame.Data;
using PSTFrame.Net;

namespace LZAI.MgrLib.Utility
{
    public class SendMail : BaseService<Mgr_Config, int>
    {
        private readonly Mgr_ConfigRepository _mgr_ConfigRepo;
        private IRepositoryContext DbContext;
        public SendMail(IRepositoryContext context)
        {
            DbContext = context;
            _mgr_ConfigRepo = new Mgr_ConfigRepository(context);
        }

        public void SendNewUserMail(string EMail, string mailSubject, string mailText, bool IsBodyHtml)
        {
            var dtConfig = _mgr_ConfigRepo.GetList();

            string mailHost = dtConfig.Where(x => x.ConfigID == "MailHost").First().ConfigValue;
            string mailAccount = dtConfig.Where(x => x.ConfigID == "MailAccount").First().ConfigValue;
            string mailUser = dtConfig.Where(x => x.ConfigID == "MailUser").First().ConfigValue;
            string mailPassword = dtConfig.Where(x => x.ConfigID == "MailPassword").First().ConfigValue;

            SmtpClient smtpClient = new SmtpClient(mailHost, 25);
            if (string.IsNullOrEmpty(mailUser)|| mailUser != " ")
            {
                smtpClient.Credentials = new NetworkCredential(mailUser, mailPassword);
            }
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(mailAccount, mailSubject, System.Text.Encoding.UTF8);
            msg.To.Add(EMail);
            msg.Subject = mailSubject;
            msg.IsBodyHtml = IsBodyHtml;
            msg.Body = mailText;
            
            smtpClient.Send(msg);
        }

        /// <summary>
        /// 有附檔的信
        /// </summary>
        /// <param name="EMail">收信信箱</param>
        /// <param name="mailSubject">主旨</param>
        /// <param name="mailText">內容</param>
        /// <param name="IsBodyHtml"></param>
        /// <param name="FileName"></param>
        /// <param name="FilePath"></param>
        public void SendMailByFile(string EMail, string mailSubject, string mailText, bool IsBodyHtml, string FileName, string FilePath)
        {
            var dtConfig = _mgr_ConfigRepo.GetList();

            string mailHost = dtConfig.Where(x => x.ConfigID == "MailHost").First().ConfigValue;
            string mailAccount = dtConfig.Where(x => x.ConfigID == "MailAccount").First().ConfigValue;
            string mailUser = dtConfig.Where(x => x.ConfigID == "MailUser").First().ConfigValue;
            string mailPassword = dtConfig.Where(x => x.ConfigID == "MailPassword").First().ConfigValue;

            SMTPClient smtpClient = new SMTPClient();
            smtpClient.ServerName = mailHost;
            if (string.IsNullOrEmpty(mailUser) || mailUser != " ")
            {
                smtpClient.UserName = mailUser;
                smtpClient.Password = mailPassword;
            }
            smtpClient.FromAddress = mailAccount;

            string[] EMailArr = EMail.Split(',');
            foreach (string i in EMailArr)
            {
                smtpClient.AddTo(i);
            }
            //smtpClient.AddTo(EMail);
            smtpClient.Subject = mailSubject;
            smtpClient.IsBodyHtml = IsBodyHtml;
            smtpClient.Body = mailText;
            if (!string.IsNullOrEmpty(FilePath))
                smtpClient.AddAttachment(FilePath, FileName);
            smtpClient.Send();

        }

    }
}
