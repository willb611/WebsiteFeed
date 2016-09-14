using System.Web.Mvc;
using NLog;

namespace FeedWebpage.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // GET: Home
        public ActionResult Index()
        {
            Logger.Info("Returning index");
            return View();
        }
    }
}
