using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AspNetMvcPlugins.Infrastructure;
using Common;
using Domain.Core;

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
            var modules = DependencyResolver.Current.GetServices<IPluginModule>();
			ViewBag.Plugins = modules;
						
			//TODO: Get from View
			ISearchParameters searchParameters = new SearchParameters();
			searchParameters.FolderPath = @"C:\test\";
			searchParameters.IsSearchInSubfolders = true;
			searchParameters.FileLength = 10*1024;
			searchParameters.CreationDate = DateTime.Now;
			//searchParameters.FileAttributes = FileAttributes.Archive;

			List<FileAttributesForCheckBox> fileAttributesList = new List<FileAttributesForCheckBox>();
			foreach (var item in Enum.GetValues(typeof(FileAttributes)))
			{
				fileAttributesList.Add(new FileAttributesForCheckBox { Text = item.ToString(), Value = Convert.ToInt32(item), IsChecked = false });
			}
			
			searchParameters.FileAttributes = fileAttributesList;

			//searchParameters.FileAttributes = Enum.GetValues(typeof(FileAttributes)).Cast<FileAttributes>().Skip(1);

			//var action = _actions.SingleOrDefault(m => m.FileExtension == ".txt");
			//IEnumerable<string> searchedResult = SearchHelper.TestSearch(searchParameters, action);

            ViewBag.SearchedFiles = new List<string>();
			return View(searchParameters);
        }

		[HttpPost]
		[ActionName("Index")]
		public ActionResult Find(SearchParameters parameters)
		{
			var modules = DependencyResolver.Current.GetServices<IPluginModule>();
			ViewBag.Plugins = modules;
            
            //TODO: Get from View
            //parameters.FileAttributes = FileAttributes.Archive;

            IFinder action = null;
            if (!String.IsNullOrEmpty(parameters.PluginModuleId))
                action = _actions.SingleOrDefault(m => m.FileExtension.Equals(parameters.PluginModuleId));
            IEnumerable<string> searchedResult = SearchHelper.TestSearch(parameters, action);
            ViewBag.SearchedFiles = searchedResult;

            return View(parameters);
		}












        [HttpGet]
        public ActionResult Finder()
        {
            ViewBag.Actions = _actions;
            return View();
        }

        [HttpPost]
        public ActionResult Finder(String action)
        {
            ViewBag.Actions = _actions;
            return View();
        }
	}
}