using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LZAI.Model.DB;
using LZAI.Repository.IRepository;
using PSTFrame.Data;
using PSTFrame.Data.Dapper;

namespace LZAI.Repository.Repository
{

    public class PublicCodeRepository : DapperRepository<PublicCode, int>, IPublicCodeRepository
    {
        public PublicCodeRepository(IRepositoryContext context) : base(context)
        {

        }
    }
}
