﻿using AspNetMvcPlugins.Infrastructure;
using Autofac;
using Autofac.Integration.Mvc;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;

[assembly: PreApplicationStartMethod(typeof(AspNetMvcPlugins.App_Start.AutofacRegistrator), "Init")]
namespace AspNetMvcPlugins.App_Start
{
    public static class AutofacRegistrator
    {
		/// <summary>
		/// Инициализируем Autofac.
		/// </summary>
        public static void Init()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            //builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            //builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            //builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            //// OPTIONAL: Enable action method parameter injection (RARE).
            //builder.InjectActionInvoker();

            PluginManager.Manager.Fill(PluginsHelper.GetPlugins("~/Plugins"));
			foreach (var assembly in PluginManager.Manager.Assemblies)
			{
				builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
				BuildManager.AddReferencedAssembly(assembly);
			}
            
            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            
        }
    }

}

