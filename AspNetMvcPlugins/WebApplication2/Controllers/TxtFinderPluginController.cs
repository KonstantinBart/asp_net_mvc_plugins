using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class TxtFinderPluginController : Controller
    {
        //
        // GET: /MVC/
        public ActionResult Index()
        {
            IFinder finder = new Finder();
            return View(finder);
        }
	}
}