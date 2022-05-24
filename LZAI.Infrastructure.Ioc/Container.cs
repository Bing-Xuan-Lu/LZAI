using System;
using System.Reflection;
//
using Autofac;
using Autofac.Core;
//
using LZAI.Repository.Repository;

namespace LZAI.Infrastructure.Ioc
{
    /// <summary>
    /// 控制台程序容器
    /// </summary>
    public static class Container
    {
        /// <summary>
        /// 容器
        /// </summary>
        public static IContainer Instance;

        /// <summary>
        /// 初始化容器
        /// </summary>
        /// <param name="func">委托</param>
        /// <returns></returns>
        public static void Init(Func<ContainerBuilder, ContainerBuilder> func = null)
        {
            var builder = new ContainerBuilder();
            MyBuild(builder);
            func?.Invoke(builder);
            Instance = builder.Build();
        }

        /// <summary>
        /// 自定義註冊
        /// </summary>
        /// <param name="builder"></param>
        public static void MyBuild(ContainerBuilder builder)
        {
            BuildContainerFunc8(builder);
        }
        /// <summary>
        /// 反射所有Class
        /// </summary>
        /// <param name="builder"></param>
        public static void BuildContainerFunc8(ContainerBuilder builder)
        {
            Assembly[] assemblies = Helpers.ReflectionHelper.GetAllAssemblies();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(cc =>cc.Name.EndsWith("Repository")|
                            cc.Name.EndsWith("Service"))
                .PublicOnly()
                .Where(cc=>cc.IsClass)
                //.Except<TeacherRepository>()//排除
                //.As(x=>x.GetInterfaces()[0])//反射
                .AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(BaseRepository<,>));
        }
    }
}
