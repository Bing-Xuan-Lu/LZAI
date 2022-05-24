using System.Data;
using PSTFrame.Data.Helepr;
using System.Data.SqlClient;
using System;
using LZAI.MgrLib.Model;
using LZAI.MgrLib.Model.DB;
using PSTFrame.Data.Dapper;
using PSTFrame.Data;
using Dapper;
using System.Collections.Generic;
using System.Linq;


namespace LZAI.MgrLib.Repository
{
    
    internal class MgrUsersRepository : DapperRepository<Model.DB.MgrUsers, int>
    {
        public MgrUsersRepository(IRepositoryContext context) : base(context)
        {
            
        }


        #region 個人資料維護更新(UserName,Tel,Email,PhoneNumber,UpdateUnitId,UpdateUserId)
        /// <summary>
        /// 個人資料維護更新(UserName,Tel,Email,PhoneNumber,UpdateUnitId,UpdateUserId)
        /// </summary>
        /// <param name="oMgrUsers"></param>
        /// <returns>影響資料筆數</returns>
        internal int UpdatePersonalEdit(MgrUsers oMgrUsers)
        {
            #region SQL Script

            string SQLStr = "UPDATE MgrUsers SET \r\n";
            SQLStr += "            UserName=@UserName, \r\n";
            SQLStr += "            Tel=@Tel, \r\n";
            SQLStr += "            Email=@Email, \r\n";
            SQLStr += "            CityId=@CityId, \r\n";
            SQLStr += "            PhoneNumber=@PhoneNumber, \r\n";
            SQLStr += "            UpdateDateTime=GETDATE(), \r\n";
            SQLStr += "            UpdateUnitId=@UpdateUnitId, \r\n";
            SQLStr += "            UpdateUserId=@UpdateUserId \r\n";
            SQLStr += " WHERE UserId = @UserId\r\n";
            #endregion

            return Conn.Execute(SQLStr, oMgrUsers, Context.Tran);
        }
        #endregion
        /// <summary>
        /// 個人資料維護更新
        /// </summary>
        /// <param name="oMgrUsers"></param>
        /// <returns>影響資料筆數</returns>
        internal int UpdateEdit(MgrUsers oMgrUsers)
        {
            #region SQL Script

            string SQLStr = @"UPDATE MgrUsers SET 
                                    UnitId = @UnitId,
                                    Account = @Account,
                                    UserName=@UserName,  
                                 
                                    Tel=@Tel, 
                                    Email=@Email, 
                                    PhoneNumber=@PhoneNumber,
                                    UpdateDateTime=GETDATE(), 
                                    UpdateUnitId=@UpdateUnitId, 
                                    UpdateUserId=@UpdateUserId,
                                    Department=@Department,
                                    Job=@Job
                                    WHERE UserId = @UserId";
             
            #endregion

            return Conn.Execute(SQLStr, oMgrUsers, Context.Tran);
        }

        #region 藉由帳號 取得個人資料
        /// <summary>
        /// 藉由帳號 取得個人資料
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        internal MgrUsers GetDataByAccount(string Account)
        {
            string SQLStr = "SELECT * \r\n";
            SQLStr += "  FROM MgrUsers\r\n";
            SQLStr += " WHERE IsDelete = 0 AND Account = @Account\r\n";

            return Conn.Query<MgrUsers>(SQLStr, new { Account = Account }).SingleOrDefault();
        }
        #endregion

        #region 確認帳號是否存在
        /// <summary>
        /// 確認帳號是否存在
        /// </summary>
        /// <param name="Account"></param>
        /// <returns>ture表示存在</returns>
        internal bool IsAccoutRepart(string Account)
        {
            string SQLStr = "SELECT COUNT(*) \r\n";
            SQLStr += "  FROM MgrUsers\r\n";
            SQLStr += " WHERE IsDelete = 0 AND Account = @Account\r\n";
            int SqlResult = (int) Conn.ExecuteScalar(SQLStr, new { Account = Account });
            return SqlResult > 0;
        }
        #endregion

        #region 記錄 密碼輸入錯誤次數
        /// <summary>
        /// 記錄 密碼輸入錯誤次數
        /// </summary>
        /// <param name="oLoginUser"></param>
        /// <returns>影響資料筆數</returns>
        internal int GetWpErrCount(LoginInfo oLoginUser)
        {
            #region SQL Script
            string SQLStr = "UPDATE MgrUsers SET \r\n";
            SQLStr += "            WpErrCount=@WpErrCount, \r\n";
            SQLStr += "            WpErrDateTime=GETDATE() \r\n";
            SQLStr += "  OUTPUT  INSERTED.WpErrCount \r\n";
            SQLStr += " WHERE Account = @Account\r\n";
            #endregion
            return Conn.Execute(SQLStr, oLoginUser);
        }
        #endregion

        #region 解鎖
        /// <summary>
        /// 解鎖
        /// </summary>
        /// <param name="oMgrUsers"></param>
        /// <returns>影響資料筆數</returns>
        internal int Unlock(MgrUsers oMgrUsers)
        {
            #region SQL Script  
            string SQLStr = "UPDATE MgrUsers SET \r\n";
            SQLStr += "            WpErrCount = 0, \r\n";
            SQLStr += "            UpdateDateTime=GETDATE(), \r\n";
            SQLStr += "            UpdateUnitId=@UpdateUnitId, \r\n";
            SQLStr += "            UpdateUserId=@UpdateUserId \r\n";
            SQLStr += " WHERE UserId = @UserId\r\n";
            #endregion
            return Conn.Execute(SQLStr, oMgrUsers);
        }
        #endregion

        #region 登入成功 更新WpErrCount
        /// <summary>
        /// 登入成功 更新WpErrCount
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>影響資料筆數</returns>
        internal int UpdateWpErrCount(long UserId)
        {
            #region SQL Script  
            string SQLStr = "UPDATE MgrUsers SET \r\n";
            SQLStr += "            WpErrCount = 0 \r\n";
            SQLStr += " WHERE UserId = @UserId\r\n";
            #endregion
            return Conn.Execute(SQLStr, new { UserId  = UserId });
        }
        #endregion

        #region 更新密碼
        /// <summary>
        /// 更新密碼
        /// </summary>
        /// <param name="oMgrUsers"></param>
        /// <returns>影響資料筆數</returns>
        internal int UpdatePwd(MgrUsers oMgrUsers)
        {
            #region SQL Script 
            string SQLStr = "UPDATE MgrUsers SET \r\n";
            SQLStr += "            PasswordHash=@PasswordHash, \r\n";
            SQLStr += "            PasswordSalt=@PasswordSalt, \r\n";
            SQLStr += "            UpdateDateTime=GETDATE(), \r\n";
            SQLStr += "            UpdateUnitId=@UpdateUnitId, \r\n";
            SQLStr += "            UpdateUserId=@UpdateUserId, \r\n";
            SQLStr += "            IsNeedChangePW=0, \r\n";
            SQLStr += "            ChangePwDateTime=GETDATE() \r\n";
            SQLStr += " WHERE UserId = @UserId\r\n";
            #endregion
            return Conn.Execute(SQLStr, oMgrUsers);
        }
        #endregion

        #region 忘記密碼
        /// <summary>
        /// 忘記密碼
        /// </summary>
        /// <param name="oMgrUsers"></param>
        /// <returns>影響資料筆數</returns>
        internal int UpdateWp( MgrUsers oMgrUsers)
        {
            string SQLStr = "UPDATE MgrUsers SET \r\n";
            SQLStr += "            PasswordEncrypt=@PasswordEncrypt, \r\n";   //桃噪補特別加
            SQLStr += "            PasswordHash=@PasswordHash, \r\n";
            SQLStr += "            PasswordSalt=@PasswordSalt, \r\n";
            SQLStr += "            UpdateDateTime=GETDATE(), \r\n";
            SQLStr += "            UpdateUnitId=@UpdateUnitId, \r\n";
            SQLStr += "            UpdateUserId=@UpdateUserId, \r\n";
            SQLStr += "            WpErrCount=0\r\n";//密碼錯誤次數歸0
            SQLStr += " WHERE UserId = @UserId\r\n";

            return Conn.Execute(SQLStr, oMgrUsers, Context.Tran);
        }
        #endregion

        internal int UpdatePersonal(MgrUsers oMgrUsers)
        {
            #region SQL Script

            string SQLStr = "UPDATE MgrUsers SET \r\n";
            SQLStr += "            UserName=@UserName, \r\n";
            SQLStr += "            Tel=@Tel, \r\n";
            SQLStr += "            Email=@Email, \r\n";
            SQLStr += "            PhoneNumber=@PhoneNumber, \r\n";
            SQLStr += "            UpdateDateTime=GETDATE(), \r\n";
            SQLStr += "            UpdateUnitId=@UpdateUnitId, \r\n";
            SQLStr += "            UpdateUserId=@UpdateUserId, \r\n";
            SQLStr += "            Department=@Department, \r\n";
            SQLStr += "            Job=@Job \r\n";
            
            SQLStr += " WHERE UserId = @UserId\r\n";
            #endregion

            return Conn.Execute(SQLStr, oMgrUsers, Context.Tran);
        }
    }

}
