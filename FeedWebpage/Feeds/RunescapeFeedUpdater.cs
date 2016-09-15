using System.Net;
using FeedWebpage.Models;
using NLog;

namespace FeedWebpage.Feeds
{
    public class RunescapeFeedUpdater
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly WebClient _webClient = new WebClient();
        private readonly RunescapeFeedParser _runescapeFeedParser;

        public string RequestUrl { get; }

        public RunescapeFeedUpdater(string requestUrl, int maxNumberOfPostsToRetrieve)
        {
            RequestUrl = requestUrl;
            _runescapeFeedParser = new RunescapeFeedParser(maxNumberOfPostsToRetrieve);
        }

        public RunescapeFeedUpdater() : this("http://runescape.com/community", 5)
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
            var latestFeed =_runescapeFeedParser.ParseHtml(page);
            Logger.Info("[GetLatest] Updated successfully");
            return latestFeed;
        }
    }
}
