using Domain.Common;
using System.Web.Mvc;

namespace MvcPlugin
{
    public class TxtController : Controller
    {
        public ActionResult Index()
        {
            IFinder finder = new Finder();
            return View(finder);
        }
	}
}