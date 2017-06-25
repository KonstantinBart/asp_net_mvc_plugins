using AspNetMvcPlugins.Infrastructure;
using Domain.Common;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

        public int GetFilesCount(SearchParameters parameters)
        {
            var filesList = SearchHelper.GetFiles(parameters);
            Session["files"] = filesList;
            return filesList.Count();
        }

		public String Find(SearchParameters parameters, String SearchPattern, int number)
		{
            Debug.WriteLine("Searching for: " + number);
			IFinder action = null;
            if (!String.IsNullOrEmpty(parameters.SelectedPluginModule))
            {
                action = _actions.SingleOrDefault(m => m.FileExtension.Equals(parameters.SelectedPluginModule));
                action.SearchPattern = SearchPattern;
            }

            var files = Session["files"] as List<FileInfo>;
            if (files == null || files.Count < number)
                return null;
            FileInfo file = files[number];
            
            return SearchHelper.CheckFile(parameters, action, file) ? file.Name : null;
		}

        public JsonResult RefreshModules(string selectedValue)
        {
			PluginManager.Manager.SetSelectedAssemblies(PluginsHelper.GetPlugins("~/SelectedPlugins"));
			var modules = PluginManager.Manager.GetSelectedPlugins();

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

	}
}