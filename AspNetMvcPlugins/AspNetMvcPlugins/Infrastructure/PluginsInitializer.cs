using Autofac;
using Domain.Common;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;

namespace AspNetMvcPlugins.Infrastructure
{
    public static class PluginsInitializer
    {
        public static void Initialize(this ContainerBuilder builder)
        {
            var pluginFolder = new DirectoryInfo(HostingEnvironment.MapPath("~/Plugins"));
            var assemblies = pluginFolder.GetFiles("*.dll", SearchOption.AllDirectories).
                Select(x => Assembly.LoadFrom(x.FullName));
            foreach (var assembly in assemblies)
            {
                Type type = assembly.GetTypes().Where(t => t.GetInterface(typeof(IFinder).Name) != null).FirstOrDefault();
                if (type != null)
                {
                    builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
                    BoC.Web.Mvc.PrecompiledViews.ApplicationPartRegistry.Register(assembly);
                }
            }
        }
        
    }
}