using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace AspNetMvcPlugins.Infrastructure
{
	public class PluginManager
	{
		static PluginManager _manager;

		public static PluginManager Manager
		{
			get { return _manager ?? (_manager = new PluginManager()); }
		}

		/// <summary>
		/// Все доступные для отображения модули.
		/// </summary>
		internal IEnumerable<Assembly> Assemblies { get; set; }

		public PluginManager()
		{
			Assemblies = new List<Assembly>();
		}

		public IEnumerable<Assembly> GetAssemblies()
		{
			return Assemblies;
		}

		public Assembly FindAssembly(String name)
		{
			return Assemblies.Where(x => x.GetName().Name.Equals(name)).FirstOrDefault();
		}

		internal void Fill(IEnumerable<Assembly> list)
		{
			Assemblies = from v in list select v;
		}

		/// <summary>
		/// Модули, выбранные для отображения в данный момент.
		/// </summary>
		internal IEnumerable<Assembly> SelectedAssemblies { get; set; }

		public void SetSelectedAssemblies(IEnumerable<Assembly> selectedAssemblies)
		{
			SelectedAssemblies = from v in selectedAssemblies where Assemblies.Contains(v) select v;
		}

		/// <summary>
		/// Возвращает список выбранных в данный момент модулей (из общего списка).
		/// </summary>
		/// <returns>Список модулей.</returns>
		public IEnumerable<IPluginModule> GetSelectedPlugins()
		{
			var modules = DependencyResolver.Current.GetServices<IPluginModule>();
			foreach (var assembly in SelectedAssemblies)
			{
				foreach (var module in modules)
				{
					if (assembly.GetName().Name.Equals(module.Name))
						yield return module;
				}

			}
		}
	}
}