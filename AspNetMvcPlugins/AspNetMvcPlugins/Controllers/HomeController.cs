using AspNetMvcPlugins.App_Start;
using AspNetMvcPlugins.Infrastructure;
using Domain.Common;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AspNetMvcPlugins.Controllers
{
	public class HomeController : Controller
	{
		private readonly IEnumerable<IFinder> _actions;

		public HomeController(IEnumerable<IFinder> actions)
		{
			_actions = actions;
		}

		[HttpGet]
		public ActionResult Index()
		{
			RefreshModules("");

			ISearchParameters searchParameters = SearchHelper.FillSearchParameters();
			ViewBag.SearchedFiles = new List<string>();
			
			return View(searchParameters);
		}

		public async Task<PartialViewResult> Find(SearchParameters parameters, String SearchPattern, CancellationToken cancellationToken = default(CancellationToken))
		{
			//var modules = DependencyResolver.Current.GetServices<IPluginModule>();
            //ViewBag.Plugins = modules;

			IFinder action = null;
            if (!String.IsNullOrEmpty(parameters.SelectedPluginModule))
            {
                action = _actions.SingleOrDefault(m => m.FileExtension.Equals(parameters.SelectedPluginModule));
                action.SearchPattern = SearchPattern;
            }

            //TODO: Fix cancellation
            //var token = CancellationTokenSource.CreateLinkedTokenSource(
            //    Response.ClientDisconnectedToken, Request.TimedOutToken);
			
            return PartialView(await SearchHelper.AsyncSearchByParameters(parameters, action, cancellationToken));
		}

        public JsonResult RefreshModules(string selectedValue)
        {
			PluginManager.Manager.SetSelectedAssemblies(PluginsHelper.GetPlugins("~/SelectedPlugins"));
			var modules = PluginManager.Manager.GetSelectedPlugins();

			//var modules = DependencyResolver.Current.GetServices<IPluginModule>();
			List<SelectListItem> modulesDropDown = FillModulesDropDown(modules, selectedValue);
			ViewBag.ModulesDropDown = modulesDropDown;

			return Json(modulesDropDown, JsonRequestBehavior.AllowGet);
        }

        private static List<SelectListItem> FillModulesDropDown(IEnumerable<IPluginModule> modules, string selectedValue)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            result.Add(new SelectListItem() { Text = "Выберите модуль...", Value = "", Selected = false });
            result.AddRange( 
                modules.Select(i => new SelectListItem()
                    {
                        Text = i.Name,
                        Value = i.ToString(),
                        Selected = selectedValue.Equals(i.ToString())
                    })
            );
            return result;
        }

        //public ActionResult PluginChange(string name)
        //{
        //    var modules = PluginManager.Manager.GetSelectedPlugins();
        //    var selectedModule = from v in modules where v.Name.Equals(name) select v;
        //    if (!selectedModule.Any())
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView(selectedModule);
        //}

	}
}