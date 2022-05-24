using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using LZAI.MgrLib.Model.DB;
using LZAI.MgrLib.Service;
using Newtonsoft.Json;
using PSTFrame.MVC;
using PSTFrame.MVC.Filters;
using PSTFrame.MVC.Model;

namespace LZAIMonitor.Controllers
{
    public class HomeController : WebBaseController
    {
        /// <summary>
        /// 登入頁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        /// <summary>
        /// 首頁
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            LoginViewModel LoginInfo;

            try
            {
                LoginInfo = new LoginViewModel();
                LoginInfo.UserInfo = new Dictionary<string, object>();
                SignOn(model.AccountId, "", model.Worpas);
            }
            catch (Exception ex)
            {
                ResultMessage.Message = ex.Message;
                ResultMessage.Status = false;
            }
            finally
            {
                LogLoginInfo();
            }
            TempData["ErrMsg"] = ResultMessage.Message;
            if (!ResultMessage.Status)
                return View();

            return RedirectToAction("CarWatch", "Monitor");
        }


        /// <summary>
        /// 將登入資訊寫到FormAuthentication
        /// </summary>
        /// <param name="CardNumber"></param>
        private void SignOn(string Account, string LoginType, string worp = "")
        {
            LoginViewModel LoginInfo;

            LoginInfo = new LoginViewModel();
            LoginInfo.UserInfo = new Dictionary<string, object>();

            CreateDbContext();
            VwMgrUsers LoginUser;
            MgrUsersService _mgrUsersService = new MgrUsersService();
            if (!_mgrUsersService.GetFormId(Account, worp, out LoginUser))
            {

                ResultMessage.Message = "找不到人員資訊";
                ResultMessage.Status = false;
            }
            //    帳號：PSTCOM
            //密碼：U89173703



            LoginInfo.AccountId = LoginUser.Account;
            LoginInfo.UserName = LoginUser.UserName;
            LoginInfo.Roles = LoginUser.LoginPermission;
            LoginInfo.UserInfo.Add("MgrUsers", LoginUser);
            LoginInfo.UserInfoDetail = JsonConvert.SerializeObject(LoginUser);
            SignIn(LoginInfo, timeout: 480);

            ResultMessage.Message = $"【{LoginInfo.UserName}】登入成功"; //"憑證卡號: " + signature.CardNumber + "\r\n" + "憑證資訊: " + signature.Cert.SubjectExtra;
            ResultMessage.Status = true;
        }

        /// <summary>
        /// 記錄登入、登出
        /// </summary>
        /// <param name="LoginStatusId">0：登入, 4：登出</param>
        private void LogLoginInfo(int LoginStatusId = 0)
        {
            if (LoginUser != null && LoginUser.UserId != 0)
            {
                if (DBContext == null)
                {
                    CreateDbContext();
                }
                var _logLoginService = new LogLoginService(DBContext);
                var _logLogin = new LogLogin();

                _logLogin.LogDateTime = DateTime.Now;
                _logLogin.Account = LoginUser.Account;
                _logLogin.UserID = LoginUser.UserId;
                _logLogin.UserIP = UserIP;
                if (LoginStatusId == 0)
                {
                    if (ResultMessage.Status)
                    {
                        _logLogin.LoginStatusId = 0;
                        _logLogin.Remark = "登入成功";
                    }
                    else
                    {
                        _logLogin.LoginStatusId = 1;
                        _logLogin.Remark = ResultMessage.Message;
                    }
                }
                else
                {
                    _logLogin.LoginStatusId = LoginStatusId;
                    _logLogin.Remark = "登出";
                }
                _logLoginService.Log(_logLogin);
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            LogLoginInfo(4);
            Session.Clear();
            SignOut();
            TempData["ErrMsg"] = "登出成功";
            return RedirectToAction("Login", "Home");
        }

        #region 忘記密碼
        /// <summary>
        /// 忘記密碼頁
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Forget()
        {
            return View();
        }

        /// <summary>
        /// 忘記密碼 -- 確定
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]//使用Ajax傳資料記得使用此Token && $.ajax head 記得加入Token
        [AjaxAwareHandleError(PartialViewName = "~/Views/Shared/CSRF_Error.cshtml")]
        [AllowAnonymous]
        public ActionResult ForgetPass(FormCollection formCollection)
        {
            ResultMessage = new MessageModel();
            MgrUsers _mgrUsers = new MgrUsers();
            string Email = formCollection["Email"];
            string Account = formCollection["ForgetAccount"];
            //檢查信箱
            bool isEmail = Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail)
            {
                ResultMessage = new MessageModel()
                {
                    Status = false,
                    Message = "Mail格式錯誤"
                };
            }
            else
            {
                CreateDbContext();
                MgrUsersService _mgrUsersService = new MgrUsersService(DBContext);
                //檢查Email是否為此帳號之Email
                ResultMessage = _mgrUsersService.GetDataByAccount(Account, Email, out _mgrUsers);
                if (ResultMessage.Status)
                {
                    //更新新密碼 & 發信
                    ResultMessage = _mgrUsersService.ForgetWp(_mgrUsers);
                }
            }
            return Json(ResultMessage);
        }

        #endregion

        #region 圖片驗證碼
        /// <summary>
        /// 圖片驗證碼
        /// TODO:要換我不是機器人
        /// </summary>
        [AllowAnonymous]
        public void VerificationCode()
        {
            // 是否產生驗證碼
            bool isCreate = true;
            // Session["CreateTime"]: 建立驗證碼的時間
            if (Session["CreateTime"] == null)
            {
                Session["CreateTime"] = DateTime.Now;
            }
            else
            {
                DateTime startTime = Convert.ToDateTime(Session["CreateTime"]);
                DateTime endTime = Convert.ToDateTime(DateTime.Now);
                TimeSpan ts = endTime - startTime;
                // 重新產生驗證碼的間隔
                if (ts.Minutes > 15)
                {
                    isCreate = true;
                    Session["CreateTime"] = DateTime.Now;
                }
                else
                {
                    //重新整理就更新驗證碼
                    isCreate = true;
                }
            }
            Response.ContentType = "image/gif";
            //建立 Bitmap 物件和繪製
            Bitmap basemap = new Bitmap(200, 60);
            Graphics graph = Graphics.FromImage(basemap);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, 200, 60);
            Font font = new Font(FontFamily.GenericSerif, 48, FontStyle.Bold, GraphicsUnit.Pixel);
            Random random = new Random();
            // 英數
            string letters = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefhjkmnpqrstuvwxyz2345678";
            string letter;
            StringBuilder sb = new StringBuilder();
            if (isCreate)
            {
                // 加入隨機二個字
                // 英文4 ~ 5字，中文2 ~ 3字
                for (int word = 0; word < 5; word++)
                {
                    letter = letters.Substring(random.Next(0, letters.Length - 1), 1);
                    sb.Append(letter);
                    // 繪製字串
                    graph.DrawString(letter, font, new SolidBrush(Color.Black), word * 38, random.Next(0, 15));
                }
            }
            else
            {
                // 使用先前的驗證碼來產生
                string currentCode = Session["ValidateCode"].ToString();
                sb.Append(currentCode);
                foreach (char item in currentCode)
                {
                    letter = item.ToString();
                    // 繪製字串
                    graph.DrawString(letter, font, new SolidBrush(Color.Black), currentCode.IndexOf(item) * 38, random.Next(0, 15));
                }
            }
            // 混亂背景
            Pen linePen = new Pen(new SolidBrush(Color.Black), 2);
            for (int x = 0; x < 10; x++)
            {
                graph.DrawLine(linePen, new Point(random.Next(0, 199), random.Next(0, 59)), new Point(random.Next(0, 199), random.Next(0, 59)));
            }
            // 儲存圖片並輸出至stream     
            basemap.Save(Response.OutputStream, ImageFormat.Gif);
            // 將產生字串儲存至 Sesssion
            Session["ValidateCode"] = sb.ToString();
            Response.End();
        }
        #endregion


    }
}