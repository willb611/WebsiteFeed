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
        private readonly TimedCacheRefresher _runescapeFeedCacheRefresher;

        private readonly TechCrunchFeedCache _techCrunchFeedCache = new TechCrunchFeedCache();
        private readonly TimedCacheRefresher _techCrunchFeedCacheRefresher;

        public FeedsController()
        {
            _runescapeFeedCacheRefresher = new TimedCacheRefresher(_runescapeFeedCache);
            _techCrunchFeedCacheRefresher = new TimedCacheRefresher(_techCrunchFeedCache);
        }

        public ActionResult Runescape()
        {
            return AsJson(_runescapeFeedCache.Get());
        }

        public ActionResult TechCrunch()
        {
            return AsJson(_techCrunchFeedCache.Get());
        }

        private JsonResult AsJson(PostFeed postFeed)
        {
            return Json(postFeed, JsonRequestBehavior.AllowGet);
        }
    }
}
