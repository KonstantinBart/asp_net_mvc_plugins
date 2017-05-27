using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public ActionResult Index()
        {
            var modules = DependencyResolver.Current.GetServices<IPluginModule>();
            return View(modules);
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