using Domain.Common;
using System.Web.Mvc;

namespace MvcPlugin
{
    public class TxtFinderPluginController : Controller
    {
        public ActionResult Index()
        {
            IFinder finder = new Finder();
            return View(finder);
        }
	}
}