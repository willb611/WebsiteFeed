using System.Net;
using FeedWebpage.Models;
using NLog;

namespace FeedWebpage.Feeds
{
    public class RunescapeFeedUpdater
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly WebClient _webClient = new WebClient();
        private readonly RunescapeFeedParser _runescapeFeedParser = new RunescapeFeedParser();

        public string RequestUrl { get; }

        public RunescapeFeedUpdater(string requestUrl)
        {
            RequestUrl = requestUrl;
        }

        public RunescapeFeedUpdater() : this("http://runescape.com/community")
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

        public FeedList GetLatest()
        {
            var page = DownloadPageWithRetries();
            var latestFeed =_runescapeFeedParser.ParseFeed(page);
            Logger.Info("[GetLatest] Updated successfully");
            return latestFeed;
        }
    }
}
