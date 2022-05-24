using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace LZAI.Infrastructure.Ioc.Helpers
{
    public static class ReflectionHelper
    {
        /// <summary>
        ///  取得Asp.Net FrameWork DLL組件
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAllAssemblies()
        {
            //Ioc
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            List<Assembly> assemblies = executingAssembly.GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Where(x => x.FullName.Contains("LZAI") || x.FullName.Contains("PST"))
                .ToList();
            //ConsoleApp
            Assembly assembly = Assembly.GetEntryAssembly();
            assemblies.Add(assembly);
            return assemblies.ToArray();
        }

        /// <summary>
        ///  取得Asp.Net FrameWork Web所有組件
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAllAssembliesWeb()
        {
            Assembly[] assemblies = System.Web.Compilation.BuildManager
                .GetReferencedAssemblies()
                .Cast<Assembly>()
                .Where(x => x.FullName.Contains("LZAI") || x.FullName.Contains("PST"))
                .ToArray();
            return assemblies;
        }
    }
}
