using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeedWebpage.Models;
using FeedWebpage.Models.FeedCaches;

namespace FeedWebpage.Controllers
{
    public class FeedsController : Controller
    {
        private readonly RunescapeFeedCache _runescapeFeedCache = new RunescapeFeedCache();
        private readonly TechCrunchFeedCache _techCrunchFeedCache = new TechCrunchFeedCache();

        public ActionResult Runescape()
        {
            return AsJson(_runescapeFeedCache.Retrieve());
        }

        public ActionResult TechCrunch()
        {
            return AsJson(_techCrunchFeedCache.Retrieve());
        }

        private JsonResult AsJson(FeedList feedList)
        {
            return Json(feedList, JsonRequestBehavior.AllowGet);
        }
    }
}
