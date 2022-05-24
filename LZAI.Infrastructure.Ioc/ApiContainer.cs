using System;
using System.Linq;
using System.Reflection;
//
using Autofac;
using Autofac.Integration.WebApi;
//
using LZAI.Repository.Repository;


namespace LZAI.Infrastructure.Ioc
{
    /// <summary>
    /// .NET Framework WebApi容器
    /// </summary>
    public static class ApiContainer
    {
        public static IContainer Instance;

        /// <summary>
        /// 初始化容器
        /// </summary>
        /// <param name="config"></param>
        /// <param name="func"></param>
        public static void Init(System.Web.Http.HttpConfiguration config,Func<ContainerBuilder, ContainerBuilder> func = null)
        {
            var builder = new ContainerBuilder();
            MyBuild(builder);
            func?.Invoke(builder);
            Instance = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Instance);
        }

        public static void MyBuild(ContainerBuilder builder)
        {
            var assemblies = Helpers.ReflectionHelper.GetAllAssembliesWeb();

            //註冊Repository && Service
            builder.RegisterAssemblyTypes(assemblies)
                .Where(cc => cc.Name.EndsWith("Repository") |
                             cc.Name.EndsWith("Service"))
                .PublicOnly()
                .Where(cc => cc.IsClass)
                .AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(BaseRepository<,>));
            //ApiController
            Assembly mvcAssembly = assemblies.FirstOrDefault(x => x.FullName.Contains(".NetFrameworkApi"));
            builder.RegisterApiControllers(mvcAssembly);
        }
    }
}
