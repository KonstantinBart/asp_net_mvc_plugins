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
            PopulateModules("");

			ISearchParameters searchParameters = SearchHelper.FillSearchParameters();
			ViewBag.SearchedFiles = new List<string>();
			
			return View(searchParameters);
		}

		public async Task<PartialViewResult> Find(SearchParameters parameters, CancellationToken cancellationToken = default(CancellationToken))
		{
			var modules = DependencyResolver.Current.GetServices<IPluginModule>();
			ViewBag.Plugins = modules;

			IFinder action = null;
			if (!String.IsNullOrEmpty(parameters.SelectedPluginModule))
				action = _actions.SingleOrDefault(m => m.FileExtension.Equals(parameters.SelectedPluginModule));

			//CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
			//CancellationToken token = cancelTokenSource.Token;

            //var token = CancellationTokenSource.CreateLinkedTokenSource(
            //    Response.ClientDisconnectedToken, Request.TimedOutToken);

            //var token = CancellationTokenSource.CreateLinkedTokenSource(
            //    cancellationToken, Response.ClientDisconnectedToken).Token;

			//if (token.IsCancellationRequested)
			//{
			//	return null;
			//}
            //Check cancellation
            //Thread.Sleep(1);
            //cancelTokenSource.Cancel();
			//var searchedResult = await SearchHelper.SearchWithCancel(parameters, action, token);
			
            return PartialView(await SearchHelper.AsyncSearchByParameters(parameters, action, cancellationToken));
		}

        public JsonResult RefreshModules(string selectedValue)
        {
            AutofacRegistrator.Init();
            return PopulateModules(selectedValue);
        }

        public JsonResult PopulateModules(string selectedValue)
        {
            var modules = DependencyResolver.Current.GetServices<IPluginModule>();
            List<SelectListItem> modulesDropDown = FillModulesDropDown(modules, selectedValue);
            ViewBag.ModulesDropDown = modulesDropDown;

            return Json(modulesDropDown, JsonRequestBehavior.AllowGet);
        }

        private static List<SelectListItem> FillModulesDropDown(IEnumerable<IPluginModule> modules, string selectedValue)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            result.Add(new SelectListItem() { Text = "Choose plugin...", Value = "", Selected = false });
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

	}
}