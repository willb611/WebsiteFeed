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

        private volatile FeedList _active;

        public override void Clear()
        {
            _active = null;
            Update();
        }

        protected override FeedList Update()
        {
            var latest = _runescapeFeedUpdater.GetLatest();
            _active = latest;
            return _active;
        }

        public override FeedList Retrieve()
        {
            return _active ?? Update();
        }
    }
}
