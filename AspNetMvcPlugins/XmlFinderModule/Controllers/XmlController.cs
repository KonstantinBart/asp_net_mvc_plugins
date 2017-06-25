using Domain.Common;
using System.Web.Mvc;

namespace MvcPlugin.Controllers
{
    public class XmlController : Controller
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