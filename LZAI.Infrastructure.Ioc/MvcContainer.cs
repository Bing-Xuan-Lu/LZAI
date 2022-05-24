using System;
using System.Linq;
using System.Reflection;
//
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using Hangfire;
using LZAI.Repository.IRepository;
//
using LZAI.Repository.Repository;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using PSTFrame.Data;
using PSTFrame.Data.Dapper;

namespace LZAI.Infrastructure.Ioc
{
    /// <summary>
    /// .net framework MVC程序容器
    /// </summary>
    public static class MvcContainer
    {
        public static IContainer Instance;

        /// <summary>
        /// 初始化容器
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IContainer Init(System.Web.Http.HttpConfiguration config,
            Func<ContainerBuilder, ContainerBuilder> func = null)
        {
            var builder = new ContainerBuilder();
            MyBuild(builder); 
            func?.Invoke(builder);
            Instance = builder.Build();
            //將AutoFac設置為系统DI解析器
            var resolver = new Autofac.Integration.Mvc.AutofacDependencyResolver(Instance);
            System.Web.Mvc.DependencyResolver.SetResolver(resolver);

            //TODO:因為WebApi與MVC共用，如不使用需移除HttpConfiguration參數
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Instance);
            return Instance;
        }

        public static void MyBuild(ContainerBuilder builder)
        {
            Assembly[] assemblies = Helpers.ReflectionHelper.GetAllAssembliesWeb();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(cc => cc.Name.EndsWith("Repository") |
                             cc.Name.EndsWith("Service") |
                             cc.Name.StartsWith("PSTFrame"))
                .PublicOnly()
                .Where(cc => cc.IsClass)
                .AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(BaseRepository<,>))
                .As(typeof(IBaseRepository<,>));
            builder.RegisterType<DapperContext>().As<IRepositoryContext>()
                .WithParameter("connectionSeting", 
                    System.Configuration.ConfigurationManager.ConnectionStrings["LZAI"])
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            //MvcController
            Assembly mvcAssembly = assemblies.FirstOrDefault(x =>
                x.FullName.Contains("LZAIMonitor"));
            builder.RegisterControllers(mvcAssembly);
            // OPTIONAL: 註冊 web abstractions
            builder.RegisterModule<AutofacWebTypesModule>();
            // OPTIONAL: 啟用 property injection into action filters.
            builder.RegisterFilterProvider();
            //TODO:因為WebApi與MVC共用，不使用請註解
            builder.RegisterApiControllers(mvcAssembly);
            //TODO:因為SignalR與MVC相依，不使用請註解
            builder.RegisterHubs(mvcAssembly).SingleInstance();
            //TODO:利澤專案車牌辨識前後台運算
            //// Register Autofac resolver into container to be set into HubConfiguration later
            builder.RegisterType<Autofac.Integration.SignalR.AutofacDependencyResolver>().As<IDependencyResolver>().SingleInstance();
            // Register ConnectionManager as IConnectionManager so that you can get hub context via IConnectionManager injected to your service
            builder.RegisterType<ConnectionManager>().As<IConnectionManager>().SingleInstance();
            //客製化Class 若無介面 需手動注入
            var CarWatchHubHelper = mvcAssembly?.GetTypes().First(x => x.Name == "CarWatchHubHelper");
            if (CarWatchHubHelper != null)
                builder.RegisterType(CarWatchHubHelper).AsSelf();
            var GpsRealTimeRunner = mvcAssembly?.GetTypes().First(x => x.Name == "GpsRealTimeRunner");
            if (GpsRealTimeRunner != null)
                builder.RegisterType(GpsRealTimeRunner).AsSelf();

        }
    }
}
