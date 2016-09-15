using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedWebpage.Models;

namespace FeedWebpage.Feeds
{
    public interface IFeedParserTemplate
    {
        FeedList ParseHtml(string html);
    }
}
