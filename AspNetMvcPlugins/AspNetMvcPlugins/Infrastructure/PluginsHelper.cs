using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.WebPages;
using Domain.Common;
using RazorGenerator.Mvc;

namespace AspNetMvcPlugins.Infrastructure
{
    public static class PluginsHelper
    {
		internal static IList<Assembly> GetPlugins(string path)
		{
			IList<Assembly> pluginAssemblies = new List<Assembly>();
			var pluginFolder = new DirectoryInfo(HostingEnvironment.MapPath(path));
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

		internal static void RegisterViews()
		{
			var assemblies = PluginManager.Manager.Assemblies.Select(x => new PrecompiledViewAssembly(x));
			var engine = new CompositePrecompiledMvcEngine(assemblies.ToArray());
			ViewEngines.Engines.Insert(0, engine);
			VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
		}
	}
}