using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.WebPages;
using Autofac;
using Domain.Common;
using RazorGenerator.Mvc;

namespace AspNetMvcPlugins.Infrastructure
{
    public static class PluginsInitializer
    {
        public static void RegisterPlugins(this ContainerBuilder builder)
        {
			var plugins = GetPlugins();
			foreach (var assembly in plugins)
			{
				builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
			}

			InitializeViews(plugins);
        }

		private static void InitializeViews(IList<Assembly> plugins)
		{
			var assemblies = plugins.Select(x => new PrecompiledViewAssembly(x));
			var engine = new CompositePrecompiledMvcEngine(assemblies.ToArray());
			//if (!ViewEngines.Engines.Any(x => x.GetType().Equals(typeof(CompositePrecompiledMvcEngine))))
				ViewEngines.Engines.Insert(0, engine);
			VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
		}

		private static IList<Assembly> GetPlugins()
		{
			IList<Assembly> pluginAssemblies = new List<Assembly>();
			var pluginFolder = new DirectoryInfo(HostingEnvironment.MapPath("~/Plugins"));
			var assemblies = pluginFolder.GetFiles("*.dll", SearchOption.AllDirectories).
				Select(x => Assembly.LoadFrom(x.FullName));
			foreach (var assembly in assemblies)
			{
				Type type = assembly.GetTypes().Where(t => t.GetInterface(typeof(IPluginModule).Name) != null).FirstOrDefault();
				if (type != null)
				{
					pluginAssemblies.Add(assembly);
				}
			}
			return pluginAssemblies;
		}
    }
}