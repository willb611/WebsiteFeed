using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeedWebpage.Models.FeedCaches;

namespace FeedWebpage.Controllers
{
    public class FeedsController : Controller
    {
        private readonly RunescapeFeedCache _runescapeFeedCache = new RunescapeFeedCache();

        public ActionResult RunescapeFeed()
        {
            return Json(_runescapeFeedCache.Retrieve(), JsonRequestBehavior.AllowGet);
        }
    }
}
