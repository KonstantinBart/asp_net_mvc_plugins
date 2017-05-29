using Autofac;
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace AspNetMvcPlugins.Infrastructure
{
    public static class PluginsInitializer
    {
        public static void Initialize(this ContainerBuilder builder)
        {
            var pluginFolder = new DirectoryInfo(HostingEnvironment.MapPath("~/Plugins"));
            var pluginAssemblies = pluginFolder.GetFiles("*.dll", SearchOption.AllDirectories);
            foreach (var pluginAssemblyFile in pluginAssemblies)
            {
                var asm = Assembly.LoadFrom(pluginAssemblyFile.FullName);
                builder.RegisterAssemblyTypes(asm).AsImplementedInterfaces();
            }
        }
        
    }
}