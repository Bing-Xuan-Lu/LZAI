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
    internal class MgrUserGroupRepository : DapperRepository<Model.DB.MgrUserGroup, int>
    {
        public MgrUserGroupRepository(IRepositoryContext context) : base(context)
        {

        }
        #region 群組區分資料更新(UserGrpId,UserId,GroupId,InsertDateTime,InsertUnitId,InsertUserId)
        /// <summary>
        /// 群組區分資料更新(UserName,Tel,Email,PhoneNumber,UpdateUnitId,UpdateUserId)
        /// </summary>
        /// <param name="oMgrUserGroup"></param>
        /// <returns>影響資料筆數</returns>
        internal int Update(MgrUserGroup oMgrUserGroup)
        {
            #region SQL Script
            string SQLStr = "UPDATE MgrUserGroup SET \r\n";
            SQLStr += "            UserGrpId=@UserGrpId, \r\n";
            SQLStr += "            UserId=@UserId, \r\n";
            SQLStr += "            GroupId=@GroupId, \r\n";
            SQLStr += "            InsertDateTime=@InsertDateTime, \r\n";
            SQLStr += "            InsertUnitId=@InsertUnitId, \r\n";
            SQLStr += "            InsertUserId=@InsertUserId\r\n";
            SQLStr += " WHERE UserGrpId = @UserGrpId\r\n";
            #endregion
            return Conn.Execute(SQLStr, oMgrUserGroup, Context.Tran);
        }
        #endregion


        public int Delete(int UserId)
        {
            string SQLStr = @"Delete FROM MgrUserGroup WHERE UserId = @UserId";


            return Conn.Execute(SQLStr, new { @UserId = UserId }, Context.Tran);
        }

        public int Insert(int UserId , int GroupId)
        {
            string SQLStr = @"INSERT INTO [dbo].[MgrUserGroup]
                               ([UserId]
                               ,[GroupId]
                               ,[InsertDateTime]
                               ,[InsertUnitId]
                               ,[InsertUserId])
                         VALUES(
                               @UserId
                               ,@GroupId
                               ,GETDATE()
                               ,1
                               ,1
                                )";
            return Conn.Execute(SQLStr, new { @UserId = UserId , @GroupId = GroupId }, Context.Tran);
        }

        #region 藉由使用者ID or  群組ID 取得群組權限資料
        /// <summary>
        /// 藉由使用者ID or  群組ID 取得群組權限資料
        /// UserGrpId、UserID兩個條件會用OR查詢，只會回傳一筆 Order by UserId
        /// </summary>
        /// <param name="oMgrUserGroup"></param>
        /// <returns></returns>
        //internal MgrUserGroup GetDataByPk(int P)
        //{   
        //    string SQLStr = "SELECT  * \r\n";
        //    SQLStr += "  FROM MgrUserGroup\r\n";
        //    SQLStr += "  WHERE  UserGrpId = @UserGrpId \r\n order by UserId ";

        //    return Conn.Query<MgrUserGroup>(SQLStr, oMgrUserGroup).SingleOrDefault();

        //}
        #endregion

        #region 藉由使用者ID 取得群組權限資料
        /// <summary>
        /// 藉由使用者ID  取得群組權限資料
        /// </summary>
        /// <param name="oMgrUserGroup"></param>
        /// <returns></returns>
        internal IEnumerable<MgrUserGroup> GetDataByUserID(decimal UserId)
        {
            string SQLStr = "SELECT  * \r\n";
            SQLStr += "  FROM MgrUserGroup\r\n";
            SQLStr += "  WHERE  UserID=@UserID\r\n order by UserId ";

            return Conn.Query<MgrUserGroup>(SQLStr, new { UserID = UserId });

        }
        #endregion
    }
}
