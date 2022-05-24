//using EPAVRNotifyDB.Repository;
using PSTFrame.Data;
using PSTFrame.Data.Dapper;
using PSTFrame.MVC;
using PSTFrame.MVC.Gear;
using PSTFrame.MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZAI.MgrLib.Models.Service
{
    public class QueryBaseService : BaseService
    {
        public QueryBaseService(IRepositoryContext dbcontext) : base(dbcontext)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectKey">WebConfig ConnectString Name</param>
        public QueryBaseService(string connectKey = "public") : base(System.Configuration.ConfigurationManager.ConnectionStrings[connectKey].ToString())
        {

        }

        public string GetToken()
        {
            return "";
        }

        public T EncodeToken<T>(string pToken, IToken token)
        {
            IToken tokenObj = token.TokenDeCode<IToken>(pToken);
            return (T)tokenObj.Data;
        }

        public UrlToken EncodeToken(string pToken)
        {
            return EncodeToken<UrlToken>(pToken,new UrlToken());
        }

        public bool InsertFileTable<T>(T fileData) where T : class 
        {
            bool result = false;

            DapperRepository<T, int> repository = new DapperRepository<T, int>( DbContext);
            repository.Insert(fileData);

            return result;
        }



    }
}
