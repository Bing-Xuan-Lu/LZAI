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
    internal class MgrOrgUnitRepository : DapperRepository<Model.DB.MgrOrgUnit, int>
    {
        public MgrOrgUnitRepository(IRepositoryContext context) : base(context)
        {

        }

        internal int Delete(int OrgUnitId)
        {
            string StrSQL = @"UPDATE MgrOrgUnit SET [IsDelete] = 1 WHERE OrgUnitId = @OrgUnitId";
            int result = 0;

            result = Conn.Execute(StrSQL , new { @OrgUnitId= OrgUnitId });


            return result;
        }



        //#region 更新單位名稱(UnitName,DispOrder,InsertDateTime,InsertUnitId,InsertUserId,UpdateDateTime,UpdateUnitId,UpdateUserId,IsDelete)
        ///// <summary>
        ///// 更新單位名稱(UnitName,DispOrder,InsertDateTime,InsertUnitId,InsertUserId,UpdateDateTime,UpdateUnitId,UpdateUserId,IsDelete)
        ///// </summary>
        ///// <param name="oMgrOrgUnit"></param>
        ///// <returns>影響資料筆數</returns>
        //internal int Update(MgrOrgUnit oMgrOrgUnit)
        //{
        //    #region SQL Script
        //    string SQLStr = "UPDATE MgrOrgUnit SET \r\n";
        //    SQLStr += "            UnitName=@UnitName, \r\n";
        //    SQLStr += "            DispOrder=@DispOrder, \r\n";
        //    SQLStr += "            InsertDateTime=@InsertDateTime, \r\n";
        //    SQLStr += "            InsertUnitId=@InsertUnitId, \r\n";
        //    SQLStr += "            InsertUserId=@InsertUserId, \r\n";
        //    SQLStr += "            UpdateDateTime=@UpdateDateTime, \r\n";
        //    SQLStr += "            UpdateUnitId=@UpdateUnitId, \r\n";
        //    SQLStr += "            UpdateUserId=@UpdateUserId, \r\n";
        //    SQLStr += "            IsDelete=@IsDelete\r\n";
        //    SQLStr += " WHERE OrgUnitId = @OrgUnitId\r\n";
        //    return Conn.Execute(SQLStr, oMgrOrgUnit, Context.Tran);
        //    #endregion
        //}
        //#endregion
        #region 取得未刪除掉的清單 IsDelete=0
        /// <summary>
        /// 取得未刪除掉的清單 IsDelete=0
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<MgrOrgUnit> List()
        {
            string SQLStr = "SELECT * \r\n";
            SQLStr += "  FROM MgrOrgUnit\r\n";
            SQLStr += " WHERE IsDelete=0\r\n";

            return Conn.Query<MgrOrgUnit>(SQLStr);
        }
        #endregion

        #region 取得對應PK值得一筆資料
        /// <summary>
        /// 取得對應PK值得一筆資料
        /// </summary>
        /// <param name="OrgUnitId"></param>
        /// <returns></returns>
        internal MgrOrgUnit GetDataByPk(int OrgUnitId)
        {
            string SQLStr = "SELECT * \r\n";
            SQLStr += "  FROM MgrOrgUnit\r\n";
            SQLStr += " WHERE OrgUnitId = @OrgUnitId\r\n";

            return Conn.Query<MgrOrgUnit>(SQLStr, new { OrgUnitId = OrgUnitId }).SingleOrDefault();            
        }
        #endregion

        #region 取得所有與UnitName相同的物件(不含刪除的) 且不含自己(OrgUnitId)的數量
        /// <summary>
        /// 取得所有與UnitName相同的物件(不含刪除的) 且不含自己(OrgUnitId) 的數量
        /// </summary>
        /// <param name="oMgrOrgUnit"></param>
        /// <returns></returns>
        internal int GetDataListByUnitNameNoIncludeSelf(MgrOrgUnit oMgrOrgUnit)
        {
            string SQLStr = "SELECT Count(*)\r\n";
            SQLStr += "  FROM MgrOrgUnit\r\n";
            SQLStr += " WHERE IsDelete = 0 AND UnitName=@UnitName AND OrgUnitID <> @OrgUnitID ORDER BY DispOrder";
            int SqlResult = (int)Conn.ExecuteScalar(SQLStr, oMgrOrgUnit);
            return SqlResult;
        }
        #endregion
    }
}
