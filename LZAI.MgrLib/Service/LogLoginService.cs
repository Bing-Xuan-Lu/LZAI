
using LZAI.MgrLib.Model.DB;
using LZAI.MgrLib.Repository;
using PSTFrame.Data;
using PSTFrame.MVC.Model;

namespace LZAI.MgrLib.Service
{
    public class LogLoginService:BaseService<LogLogin,int>, ILoggerService<LogLogin>
    {
        private LogLoginRepository _LogLoginRepositoryRepo;
        private IRepositoryContext DbContext;
        public LogLoginService(IRepositoryContext context)
        {
            DbContext = context;
            _LogLoginRepositoryRepo = new LogLoginRepository(context);
        }

        public void Log(LogLogin Entity)
        {
            _LogLoginRepositoryRepo.Insert(Entity);
        }
    }
}
