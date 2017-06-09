using System.IO;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using AspNetMvcPlugins.App_Start;
using Autofac;

namespace AspNetMvcPlugins.Infrastructure
{
	public class RefreshModule : IHttpModule
	{
		static Timer timer;
		long interval = 5000;
		static object syncObject = new object();
		static bool isAppStarted = false;

		public void Init(HttpApplication app)
		{
			if (!isAppStarted)
			{
				lock (syncObject)
				{
					if (!isAppStarted)
					{
						timer = new Timer(new TimerCallback(Refresh), app.Context, 0, interval);
						isAppStarted = true;
					}
				}
			}
		}

		private void Refresh(object obj)
		{
			lock (syncObject)
			{
				//var logger = context.Request.GetDependencyScope().GetService(typeof(ILogger)) as ILogger;

				//using (var scope = AutofacRegistrator.Container.BeginLifetimeScope())
				//{
				//	var modules = DependencyResolver.Current.GetServices<IPluginModule>();
				//	Debug.WriteLine("Refresh in timer: found {0} modules", ((ICollection)modules).Count);
				//}
				

				//using (var timerScope = AutofacDependencyResolver.Current.ApplicationContainer.BeginLifetimeScope())
				//{
				//	var chatServices = timerScope.Resolve<IPluginModule>();
				//	chatServices.MarkInactiveUsers();
				//}



				var container = AutofacRegistrator.Container;

				var builder = new ContainerBuilder();
				var pluginFolder = new DirectoryInfo(HostingEnvironment.MapPath("~/Plugins"));
				var pluginAssemblies = pluginFolder.GetFiles("*.dll", SearchOption.AllDirectories);
				foreach (var pluginAssemblyFile in pluginAssemblies)
				{
					var asm = Assembly.LoadFrom(pluginAssemblyFile.FullName);
					builder.RegisterAssemblyTypes(asm).AsImplementedInterfaces().PreserveExistingDefaults();
				}
				builder.Update(container);

			}
		}

		public void Dispose()
		{ }
	}

	public class A
	{
	}

	public interface IB
	{
	}

	public class B:IB
	{
	}
}