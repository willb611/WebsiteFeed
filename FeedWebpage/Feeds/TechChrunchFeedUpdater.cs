using System.Net;
using FeedWebpage.Models;
using NLog;

namespace FeedWebpage.Feeds
{
    internal class TechChrunchFeedUpdater
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly WebClient _webClient = new WebClient();
        private readonly RssFeedParser _rssFeedParser;

        public string RequestUrl { get; }

        public TechChrunchFeedUpdater(string requestUrl, int maxNumberOfPostsToRetrieve)
        {
            RequestUrl = requestUrl;
            _rssFeedParser = new RssFeedParser(maxNumberOfPostsToRetrieve);
        }

        public TechChrunchFeedUpdater() : this("http://feeds.feedburner.com/TechCrunch/startups", 3)
        {
        }

        private string DownloadPageWithRetries()
        {
            while (true)
            {
                try
                {
                    return _webClient.DownloadString(RequestUrl);
                }
                catch (WebException webException)
                {
                    Logger.Error("[Update] Unable to update due to web exception: {0}, retrying", webException);
                }
            }
        }

        public PostFeed GetLatest()
        {
            var page = DownloadPageWithRetries();
            var latestFeed = _rssFeedParser.ParseHtml(page);
            Logger.Info("[GetLatest] Updated successfully");
            return latestFeed;
        }
    }
}
