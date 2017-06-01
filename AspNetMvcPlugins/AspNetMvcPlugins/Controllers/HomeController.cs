﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AspNetMvcPlugins.Infrastructure;
using Domain.Common;
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

			ISearchParameters searchParameters = SearchHelper.FillSearchParameters();
            ViewBag.SearchedFiles = new List<string>();
			return View(searchParameters);
        }

		[HttpPost]
		[ActionName("Index")]
		public ActionResult Find(SearchParameters parameters)
		{
			var modules = DependencyResolver.Current.GetServices<IPluginModule>();
			ViewBag.Plugins = modules;

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