using System;
using System.Collections.Generic;
using PSTFrame.Data.Dapper;
using PSTFrame.Data;
using PSTFrame.Utility.Extensions;
using LZAI.MgrLib.Model.DB;
using Dapper;

namespace LZAI.MgrLib.Repository
{
    class MgrFuncRepository : DapperRepository<Model.DB.MgrFunc, int>
    {
        public MgrFuncRepository(IRepositoryContext context) : base(context)
        {

        }
        public IEnumerable<MgrFunc> GetList(int QueryFuncTypeId, string QueryFuncName)
        {
            string Where = null;
            if (QueryFuncTypeId > 0)
                Where += "and FuncTypeId=@FuncTypeId\r\n";
            if (!string.IsNullOrEmpty(QueryFuncName))
                Where += "and FuncName like '%' +@FuncName + '%'\r\n";
            if (string.IsNullOrEmpty(Where))
                return this.GetList(new { IsDelete = false });
            else
            {
                Where = "Where IsDelete = 0\r\n" + Where;
                return this.GetList(Where, new { FuncTypeId = QueryFuncTypeId, FuncName = QueryFuncName });
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="mgrFunc"></param>
        /// <returns></returns>
        public int Insert(MgrFunc mgrFunc)
        {
            string SQLStr = @"INSERT INTO [dbo].[MgrFunc]
           ([FuncName]
           ,[FuncMemo]
           ,[FuncTypeId]
           ,[IsAddFunc]
           ,[IsModFunc]
           ,[IsCanFunc]
           ,[IsQueFunc]
           ,[IsOthPerm1]
           ,[OthPerm1Name]
           ,[IsOthPerm2]
           ,[OthPerm2Name]
           ,[IsOthPerm3]
           ,[OthPerm3Name]
           ,[IsOthPerm4]
           ,[OthPerm4Name]
           ,[IsOthPerm5]
           ,[OthPerm5Name]
           ,[InsertDateTime]
           ,[InsertUnitId]
           ,[InsertUserId]
           ,[UpdateDateTime]
           ,[UpdateUnitId]
           ,[UpdateUserId]
           ,[IsDelete])
            OUTPUT Inserted.FuncId
           VALUES (@FuncName
           ,@FuncMemo
           ,@FuncTypeId
           ,@IsAddFunc
           ,@IsModFunc
           ,@IsCanFunc
           ,@IsQueFunc
           ,@IsOthPerm1
           ,@OthPerm1Name
           ,@IsOthPerm2
           ,@OthPerm2Name
           ,@IsOthPerm3
           ,@OthPerm3Name
           ,@IsOthPerm4
           ,@OthPerm4Name
           ,@IsOthPerm5
           ,@OthPerm5Name
           ,GETDATE()
           ,1
           ,1
           ,GETDATE()
           ,1
           ,1
           ,0)";
            return int.Parse(Conn.ExecuteScalar(SQLStr, mgrFunc, Context.Tran).ToString());
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="mgrFunc"></param>
        /// <returns></returns>
        public void Update(MgrFunc mgrFunc)
        {
            string SQLStr = @"UPDATE [dbo].[MgrFunc] set
           [FuncName] =@FuncName
           ,[FuncMemo] = @FuncMemo
           ,[FuncTypeId] = @FuncTypeId
           ,[IsAddFunc] = @IsAddFunc
           ,[IsModFunc] = @IsModFunc
           ,[IsCanFunc] = @IsCanFunc
           ,[IsQueFunc] = @IsQueFunc
           ,[IsOthPerm1] =@IsOthPerm1
           ,[OthPerm1Name] = @OthPerm1Name
           ,[IsOthPerm2] = @IsOthPerm2
           ,[OthPerm2Name] = @OthPerm2Name
           ,[IsOthPerm3] = @IsOthPerm3
           ,[OthPerm3Name] = @OthPerm3Name
           ,[IsOthPerm4] = @IsOthPerm4
           ,[OthPerm4Name] = @OthPerm4Name
           ,[IsOthPerm5] = @IsOthPerm5
           ,[OthPerm5Name] = @OthPerm5Name
           ,[UpdateDateTime] = GETDATE()
           ,[UpdateUnitId] = 1
           ,[UpdateUserId] = 1
            where FuncId = @FuncId";
            Conn.Execute(SQLStr, mgrFunc, Context.Tran);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="FuncId"></param>
        public void Delete(int FuncId)
        {
            string SQLStr = @"UPDATE [dbo].[MgrFunc] set
           [IsDelete] =1 where FuncId = @FuncId";
            Conn.Execute(SQLStr, new { FuncId = FuncId }, Context.Tran);
        }

    }
}
