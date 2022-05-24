using System;
using System.Collections.Generic;
using LZAI.MgrLib.Model;
using LZAI.MgrLib.Model.DB;
using LZAI.MgrLib.Repository;
using PSTFrame.Security.Cryptography;
using PSTFrame.MVC.Model;
using System.Linq;
using PSTFrame.Data;
using PSTFrame.MVC;
using LZAI.MgrLib.Utility;
using PSTFrame.Data.Dapper;
using BaseRepository = LZAI.MgrLib.Repository.BaseRepository;

namespace LZAI.MgrLib.Service
{
    public class MgrUsersService : BaseService<MgrUsers, int>
    {

        private readonly MgrUsersRepository _MgrUsersRepo;
        private readonly MgrGrpFuncRepository _mgrGrpFuncRepo;
        private readonly VwMgrUsersRepository _vwMgrUsersRepo;
        private readonly MgrUserGroupRepository _MgrUserGroupRepo;
        private IRepositoryContext DbContext;


        public MgrUsersService(IRepositoryContext context)
        {
            DbContext = context;
            _MgrUsersRepo = new MgrUsersRepository(context);
            _vwMgrUsersRepo = new VwMgrUsersRepository(context);
            _mgrGrpFuncRepo = new MgrGrpFuncRepository(context);
            _MgrUserGroupRepo = new MgrUserGroupRepository(context);
        }
        public MgrUsersService()
        {
            var context = BaseRepository.GetContext();
            _MgrUsersRepo = new MgrUsersRepository(context);
            _vwMgrUsersRepo = new VwMgrUsersRepository(context);
            _mgrGrpFuncRepo = new MgrGrpFuncRepository(context);
            _MgrUserGroupRepo = new MgrUserGroupRepository(context);
        }

        /// <summary>
        /// 取得使用者的功能權限List
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <returns></returns>
        public IEnumerable<MgrGrpFunc> GetGroupFuncsByUserId(decimal userId)
        {
            return _mgrGrpFuncRepo.GetsByUserId(userId);
        }


        /// <summary>
        /// 新增人員
        /// </summary>
        /// <param name="oMgrUsers"></param>
        /// <param name="GroupIdList"></param>
        /// <param name="worp"></param>
        /// <returns></returns>
        public MessageModel Insert(MgrUsers oMgrUsers, int GroupId, string worp)
        {
            MessageModel ResultMessage = new MessageModel();
            try
            {
                if (_MgrUsersRepo.GetList().Any(x => x.Account == oMgrUsers.Account))
                {
                    return new MessageModel()
                    {
                        Status = false,
                        Message = "帳號重複，請輸入其他帳號名稱"
                    };
                }
                DbContext.BeginTran();
                #region 設定密碼
                oMgrUsers.worpas = "A" + Cryptor.CreateRandomPasswordOnlyNumber();//隨機產生密碼，前面固定加A，只是想加，沒什麼理由
                string hashPassword, hashPasswordSalt;
                PSTFrame.Security.Cryptography.SaltedHash saltedHash = new PSTFrame.Security.Cryptography.SaltedHash();
                saltedHash.GetHashAndSalt(oMgrUsers.worpas, out hashPassword, out hashPasswordSalt);
                oMgrUsers.PasswordHash = hashPassword;
                oMgrUsers.PasswordSalt = hashPasswordSalt;
                #endregion
                oMgrUsers.IsNeedChangePW = true;//初次新增
                oMgrUsers.UserId = _MgrUsersRepo.Insert(oMgrUsers);
                _MgrUserGroupRepo.Insert((int)oMgrUsers.UserId, GroupId);//新增權限               
                SendNewUserMail(oMgrUsers);//發信
                DbContext.Commit();
                ResultMessage.Status = true;
                ResultMessage.Message = "新增成功";
            }
            catch (Exception ex)
            {
                DbContext.Rollback();
                ResultMessage.Status = false;
                if (ex.Message.IndexOf("IX_MgrUsers") > 0)
                {
                    ResultMessage.Message = "帳號已被使用，請更換！！";
                }
                else
                {
                    ResultMessage.Message = $"新增失敗，原因{ex.Message}";
                }
            }

            return ResultMessage;
        }

        /// <summary>
        /// 更新人員
        /// </summary>
        /// <param name="oMgrUsers"></param>
        /// <param name="GroupIdList"></param>
        /// <param name="worp"></param>
        /// <returns></returns>
        public MessageModel Update(MgrUsers oMgrUsers, int GroupId, string worp)
        {
            MessageModel ResultMessage = new MessageModel();
            DbContext.BeginTran();
            try
            {
                if (!string.IsNullOrEmpty(worp))
                {
                    string NewHash, NewSalt;
                    new SaltedHash().GetHashAndSalt(worp, out NewHash, out NewSalt);
                    oMgrUsers.PasswordHash = NewHash;
                    oMgrUsers.PasswordSalt = NewSalt;

                    _MgrUserGroupRepo.Delete((int)oMgrUsers.UserId);
                    _MgrUsersRepo.Update(oMgrUsers);
                    _MgrUserGroupRepo.Insert((int)oMgrUsers.UserId, GroupId);//新增權限
                }
                else
                {
                    _MgrUserGroupRepo.Delete((int)oMgrUsers.UserId);
                    _MgrUsersRepo.UpdateEdit(oMgrUsers);
                    _MgrUserGroupRepo.Insert((int)oMgrUsers.UserId, GroupId);//新增權限
                }
                DbContext.Commit();
                ResultMessage.Status = true;
                ResultMessage.Message = "修改成功";
            }
            catch (Exception ex)
            {
                DbContext.Rollback();
                ResultMessage.Status = false;
                if (ex.Message.IndexOf("IX_MgrUsers") > 0)
                {
                    ResultMessage.Message = "帳號已被使用，請更換！！";
                }
                else
                {
                    ResultMessage.Message = $"刪除失敗，原因{ex.Message}";
                }
            }

            return ResultMessage;
        }


        public bool Insert(MgrUsers oMgrUsers, List<string> GroupIdList, out string Message)
        {
            //Check Data
            if (!CheckData(oMgrUsers, out Message)) { return false; }

            //Check 帳號重覆
            if (IsAccoutRepart(oMgrUsers.Account))
            {
                Message = "帳號已被使用，請更換";
                return false;
            }
            //oMgrUserGroup
            MgrUserGroupRepository _MgrUsersGroupRepo = new MgrUserGroupRepository(DbContext);

            DbContext.BeginTran();
            try
            {
                #region 設定密碼
                oMgrUsers.worpas = "A" + Cryptor.CreateRandomPasswordOnlyNumber();//隨機產生密碼，前面固定加A，只是想加，沒什麼理由
                string hashPassword, hashPasswordSalt;
                PSTFrame.Security.Cryptography.SaltedHash saltedHash = new PSTFrame.Security.Cryptography.SaltedHash();
                saltedHash.GetHashAndSalt(oMgrUsers.worpas, out hashPassword, out hashPasswordSalt);
                oMgrUsers.PasswordHash = hashPassword;
                oMgrUsers.PasswordSalt = hashPasswordSalt;
                #endregion
                oMgrUsers.IsNeedChangePW = true;//初次新增
                oMgrUsers.UserId = _MgrUsersRepo.Insert(oMgrUsers);

                MgrUserGroup oMgrUserGroup = new MgrUserGroup
                {
                    InsertUserId = (long)oMgrUsers.InsertUserId,
                    InsertUnitId = oMgrUsers.InsertUnitId,
                    UserId = (long)oMgrUsers.UserId
                };

                //新增人員權限
                foreach (string GroupId in GroupIdList)
                {
                    //oMgrUserGroup.GroupId = int.Parse(GroupId);

                    _MgrUsersGroupRepo.Insert(oMgrUserGroup);//新增權限
                }
                DbContext.Commit();
            }
            catch
            {
                DbContext.Rollback();
                throw;
            }


            return true;
        }

        #region 個人資料維護更新(不含密碼)
        public bool UpdatePersonalEdit(MgrUsers oMgrUsers, out string Message)
        {
            Message = "";
            //Check
            if (!CheckDataPersonalEdit(oMgrUsers, out Message)) { return false; }
            _MgrUsersRepo.UpdatePersonalEdit(oMgrUsers);
            Message = "個人資料更新成功";
            return true;
        }
        #endregion

        #region 更新密碼
        public MessageModel UpdatePwd(MgrUsers oMgrUsers)
        {
            MessageModel ResultMessage = new MessageModel();
            //check
            try
            {
                #region 資料檢查
                if (string.IsNullOrEmpty(oMgrUsers.OldPwd))
                {
                    return new MessageModel()
                    {
                        Message = "請輸入舊密碼",
                        Status = false
                    };
                }

                if (string.IsNullOrEmpty(oMgrUsers.NewPwd))
                {
                    return new MessageModel()
                    {
                        Message = "請輸入新密碼",
                        Status = false
                    };
                }
                if (string.IsNullOrEmpty(oMgrUsers.NewPwdCheck))
                {
                    return new MessageModel()
                    {
                        Message = "請輸入確認新密碼",
                        Status = false
                    };
                }
                if (oMgrUsers.NewPwd.Length < 4)
                {
                    return new MessageModel()
                    {
                        Message = "密碼長度必須大於4",
                        Status = false
                    };
                }
                if (oMgrUsers.NewPwd.Length > 10)
                {
                    return new MessageModel()
                    {
                        Message = "密碼長度不可大於10",
                        Status = false
                    };
                }
                if (!oMgrUsers.NewPwd.Equals(oMgrUsers.NewPwdCheck))
                {
                    return new MessageModel()
                    {
                        Message = "您輸入的兩次新密碼不相符，請重新輸入",
                        Status = false
                    };
                }
                if (oMgrUsers.OldPwd.Equals(oMgrUsers.NewPwd))
                {
                    return new MessageModel()
                    {
                        Message = "舊密碼與新密碼不能重複",
                        Status = false
                    };
                }
                #endregion
                string OldPwdHash = "", OldPwdSalt = "";
                OldPwdHash = oMgrUsers.PasswordHash;
                OldPwdSalt = oMgrUsers.PasswordSalt;

                //Check 舊密碼正確性           
                if (!(new SaltedHash()).VerifyHash(oMgrUsers.OldPwd, OldPwdHash, OldPwdSalt))
                {
                    return new MessageModel()
                    {
                        Message = "舊密碼錯誤!!",
                        Status = false
                    };
                }

                //New 加密
                string NewHash, NewSalt;
                new SaltedHash().GetHashAndSalt(oMgrUsers.NewPwd, out NewHash, out NewSalt);

                //寫入資料庫
                oMgrUsers.PasswordHash = NewHash;
                oMgrUsers.PasswordSalt = NewSalt;
                _MgrUsersRepo.UpdatePwd(oMgrUsers);

                ResultMessage.Message = "修改密碼成功";
                ResultMessage.Status = true;
            }
            catch (Exception ex)
            {
                ResultMessage.Status = false;
                ResultMessage.Message = $"修改失敗，原因{ex.Message}";
            }
            return ResultMessage;
        }
        #endregion

        public bool Update(MgrUsers oMgrUsers, out string Message)
        {
            Message = "";
            //Check
            if (!CheckData(oMgrUsers, out Message)) { return false; }
            _MgrUsersRepo.Update(oMgrUsers);
            return true;
        }

        public MgrUsers Get(int id)
        {
            return _MgrUsersRepo.Get(id);
        }

        public MessageModel DeleteData(int UserId)
        {
            MessageModel ResultMessage = new MessageModel();
            try
            {
                _MgrUsersRepo.Delete(UserId);
                ResultMessage.Status = true;
                ResultMessage.Message = "刪除成功";
            }
            catch (Exception ex)
            {
                ResultMessage.Status = false;
                ResultMessage.Message = $"刪除失敗，原因{ex.Message}";
            }
            return ResultMessage;
        }


        /// <summary>
        /// 檢查帳號密碼
        /// </summary>
        /// <param name="oLoginUser"></param>
        /// <returns></returns>
        /// 
        public bool GetFormId(string Account, string worp, out VwMgrUsers MgrUsersList)
        {
            try
            {
                MgrUsersList = _vwMgrUsersRepo.GetList("WHERE Account=@Account", new { @Account = Account }).FirstOrDefault();
                if (MgrUsersList == null)
                {
                    throw new Exception("帳號密碼錯誤!!");
                }
                else if (!new SaltedHash().VerifyHash(worp, MgrUsersList.PasswordHash, MgrUsersList.PasswordSalt))
                {
                    throw new Exception("帳號密碼錯誤!!");
                }

            }
            catch
            {
                throw;
            }

            return true;
        }

        #region 登入成功 更新WpErrCount=0
        public void UpdateWpErrCount(long userId)
        {
            _MgrUsersRepo.UpdateWpErrCount(userId);
        }
        #endregion

        #region 資料檢查
        private bool CheckData(MgrUsers oMgrUsers, out string Message)
        {
            Message = "";
            if (string.IsNullOrEmpty(oMgrUsers.Account)) { Message = "請輸入註冊者帳號！！"; return false; }
            if (string.IsNullOrEmpty(oMgrUsers.UserName)) { Message = "請輸入註冊者姓名！！"; return false; }
            if (string.IsNullOrEmpty(oMgrUsers.Email)) { Message = "請輸入註冊E-Mail！！"; return false; }
            //Check Email
            if (!PSTFrame.Utility.ValidatorHelper.IsEmail(oMgrUsers.Email)) { Message = "E-Mail格式不符"; return false; }
            if (string.IsNullOrEmpty(oMgrUsers.UnitId.ToString())) { Message = "請選擇單位！！"; return false; }
            return true;
        }
        #endregion

        #region 個人資料維護更新前檢查(不含密碼)
        private bool CheckDataPersonalEdit(MgrUsers oMgrUsers, out string Message)
        {
            Message = "";
            //if (string.IsNullOrEmpty(oMgrUsers.UnitId.ToString())) { Message = "請選擇單位！！"; return false; }                                    
            if (string.IsNullOrEmpty(oMgrUsers.UserName)) { Message = "請輸入使用者姓名！！"; return false; }
            if (string.IsNullOrEmpty(oMgrUsers.Email)) { Message = "請輸入E-Mail！！"; return false; }
            //Check Email
            if (!PSTFrame.Utility.ValidatorHelper.IsEmail(oMgrUsers.Email)) { Message = "E-Mail格式不符"; return false; }
            //if (!string.IsNullOrEmpty(oMgrUsers.PhoneNumber))
            //{
            //    //Check 手機格式
            //    if (!Utility.ValidatorUtility.IsMobilePhone(oMgrUsers.PhoneNumber)) { Message = "手機格式不符"; return false; }
            //}
            //if (string.IsNullOrEmpty(oMgrUsers.Tel)) { Message = "請輸入電話！！"; return false; }
            //Check 電話格式
            //if (!PSTFrame.Utility.ValidatorHelper.IsTelephone(oMgrUsers.Tel)) { Message = "電話格式不符"; return false; }
            //2016.06.02 Kent 不Check電話格式         
            return true;
        }
        #endregion

        #region 帳號是否重覆
        public bool IsAccoutRepart(string Account)
        {

            return _MgrUsersRepo.IsAccoutRepart(Account);
        }
        #endregion

        #region 取得資料By Pk
        public bool GetDataByPk(ref MgrUsers oMgrUsers)
        {
            oMgrUsers = _MgrUsersRepo.Get((int)oMgrUsers.UserId);//抓資料
            if (oMgrUsers == null) return false;
            //if (dt.Rows.Count == 0) return false;
            ////塞資料
            //oMgrUsers.UserId = decimal.Parse(dt.Rows[0]["UserId"].ToString().Trim());
            //oMgrUsers.UnitId = decimal.Parse(dt.Rows[0]["UnitId"].ToString().Trim());
            //oMgrUsers.Account = dt.Rows[0]["Account"].ToString().Trim();
            //oMgrUsers.UserName = dt.Rows[0]["UserName"].ToString().Trim();
            //oMgrUsers.AgentName = dt.Rows[0]["AgentName"].ToString().Trim();
            //oMgrUsers.Tel = dt.Rows[0]["Tel"].ToString().Trim();
            //oMgrUsers.PhoneNumber = dt.Rows[0]["PhoneNumber"].ToString().Trim();
            //oMgrUsers.Email = dt.Rows[0]["Email"].ToString().Trim();
            //oMgrUsers.IsDelete = bool.Parse(dt.Rows[0]["IsDelete"].ToString().Trim());
            //oMgrUsers.PasswordHash = dt.Rows[0]["PasswordHash"].ToString().Trim();
            //oMgrUsers.PasswordSalt = dt.Rows[0]["PasswordSalt"].ToString().Trim();
            //oMgrUsers.IsNeedChangePW = bool.Parse(dt.Rows[0]["IsNeedChangePW"].ToString().Trim());
            return true;
        }
        #endregion


        #region 記錄 密碼輸入錯誤次數
        public void GetWpErrCount(LoginInfo oLoginUser)
        {
            _MgrUsersRepo.GetWpErrCount(oLoginUser);
        }
        #endregion

        #region 解鎖
        public void Unlock(MgrUsers oMgrUsers)
        {
            _MgrUsersRepo.Unlock(oMgrUsers);
        }
        #endregion

        /// <summary>
        /// 檢查帳號 & Email是否存在
        /// </summary>
        /// <param name="Account">帳號</param>
        ///<param name="Email">信箱</param>
        public MessageModel GetDataByAccount(string Account, string Email, out MgrUsers data)
        {
            data = _MgrUsersRepo.GetDataByAccount(Account);
            if (data == null)
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = "帳號不存在"
                };
            }
            else if (data.Email != Email)
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = "Email非本人帳號之信箱，請重新輸入"
                };
            }
            else
            {
                return new MessageModel()
                {
                    Status = true,
                };
            }
        }


        /// <summary>
        /// 忘記密碼更新 & 發信
        /// </summary>
        /// <param name="mgrUsers"></param>
        /// <returns></returns>
        public MessageModel ForgetWp(MgrUsers mgrUsers)
        {
            try
            {
                //TODO:PSTFrame密碼類型待測試
                mgrUsers.UpdateDateTime = DateTime.Now;
                mgrUsers.UpdateUnitId = mgrUsers.UnitId;
                mgrUsers.UpdateUserId = mgrUsers.UserId;//點忘了密碼的施主他自己
                DbContext.BeginTran();
                #region 設定密碼
                mgrUsers.worpas = "U" + Cryptor.CreateRandomPasswordOnlyNumber();//隨機產生密碼，前面固定加U，只是想加，沒什麼理由
                string hashPassword, hashPasswordSalt;
                PSTFrame.Security.Cryptography.SaltedHash saltedHash = new PSTFrame.Security.Cryptography.SaltedHash();
                saltedHash.GetHashAndSalt(mgrUsers.worpas, out hashPassword, out hashPasswordSalt);
                mgrUsers.PasswordHash = hashPassword;
                mgrUsers.PasswordSalt = hashPasswordSalt;
                #endregion
                _MgrUsersRepo.Update(mgrUsers);
                SendForgetUserMail(mgrUsers);
                DbContext.Commit();
                return new MessageModel()
                {
                    Status = true,
                    Message = "已將新密碼寄至您的信箱"
                };
            }
            catch (Exception ex)
            {
                DbContext.Rollback();
                return new MessageModel()
                {
                    Status = false,
                    Message = $"發信失敗，原因:{ex.Message}"
                };
            }

        }

        /// <summary>
        /// 發申請通過信
        /// </summary>
        /// <param name="oMgrUsers"></param>
        private void SendNewUserMail(MgrUsers oMgrUsers)
        {
            string mailText;

            string mailSubject = "利澤AI-帳號密碼通知";

            mailText = "<div style='font-family:Microsoft JhengHei'>{3}您好：<br/><br/>\n";
            mailText += "　系統於{0}通過您申請的帳號要求。<br/>\n";
            mailText += "  帳號：{1}<br/>\n";
            mailText += "  密碼：{2}<br/>\n";
            mailText += "<br/><br/>\n";
            mailText += "        利澤AI平台<br/>\n";
            mailText += "        </div>\n";
            mailText = string.Format(mailText
                , DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                , oMgrUsers.Account
                , oMgrUsers.worpas
                , oMgrUsers.UserName
            );
            mailText += "此信件為系統自動發信，請勿回信，謝謝。";

            new SendMail(DbContext).SendNewUserMail(oMgrUsers.Email, mailSubject, mailText, true);
        }
        /// <summary>
        /// 發申請通過信
        /// </summary>
        /// <param name="oMgrUsers"></param>
        private void SendForgetUserMail(MgrUsers oMgrUsers)
        {
            string mailText;

            string mailSubject = "利澤AI-忘記密碼通知";

            mailText = "<div style='font-family:Microsoft JhengHei'>{3}您好：<br/><br/>\n";
            mailText += "　系統於{0}通過您忘記密碼要求。<br/>\n";
            mailText += "  帳號：{1}<br/>\n";
            mailText += "  密碼：{2}<br/>\n";
            mailText += "<br/><br/>\n";
            mailText += "        利澤AI平台<br/>\n";
            mailText += "        </div>\n";
            mailText = string.Format(mailText
                , DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                , oMgrUsers.Account
                , oMgrUsers.worpas
                , oMgrUsers.UserName
            );
            mailText += "此信件為系統自動發信，請勿回信，謝謝。";

            new SendMail(DbContext).SendNewUserMail(oMgrUsers.Email, mailSubject, mailText, true);
        }

        /// <summary>
        /// 個人資料維護
        /// </summary>
        /// <param name="oMgrUsers"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public MessageModel UpdatePersonal(MgrUsers oMgrUsers)
        {
            try
            {
                _MgrUsersRepo.UpdatePersonal(oMgrUsers);
                var result = new MessageModel()
                {
                    Status = true,
                    Message = "儲存成功"
                };
                return result;
            }
            catch (Exception ex)
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = $"修改失敗，原因{ex.Message}"
                };
            }
        }
    }
}
