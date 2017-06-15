using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AspNetMvcPlugins.Infrastructure
{
	public class PluginManager
	{
		static PluginManager _manager;

		public static PluginManager Manager
		{
			get { return _manager ?? new PluginManager(); }
		}

		internal IList<Assembly> Assemblies { get; set; }

		public PluginManager()
		{
			Assemblies = new List<Assembly>();
		}

		public IList<Assembly> GetAssemblies()
		{
			return Assemblies;
		}

		public Assembly FindAssembly(String name)
		{
			return Assemblies.Where(x => x.GetName().Name.Equals(name)).FirstOrDefault();
		}

	}
}