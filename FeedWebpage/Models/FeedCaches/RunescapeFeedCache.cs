using System;
using System.Net;
using System.Threading;
using FeedWebpage.Feeds;
using NLog;

namespace FeedWebpage.Models.FeedCaches
{
    public class RunescapeFeedCache : FeedCache
    {
        private readonly RunescapeFeedUpdater _runescapeFeedUpdater = new RunescapeFeedUpdater();

        private volatile PostFeed _active;

        public override void Refresh()
        {
            _active = null;
            Update();
        }

        protected override PostFeed Update()
        {
            var latest = _runescapeFeedUpdater.GetLatest();
            _active = latest;
            return _active;
        }

        public override PostFeed Get()
        {
            return _active ?? Update();
        }
    }
}
