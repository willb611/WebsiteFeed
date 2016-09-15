using FeedWebpage.Feeds;

namespace FeedWebpage.Models.FeedCaches
{
    internal class TechCrunchFeedCache : FeedCache
    {
        private readonly TechChrunchFeedUpdater _techChrunchFeedUpdater = new TechChrunchFeedUpdater();

        private volatile PostFeed _active;

        public override void Clear()
        {
            _active = null;
            Update();
        }

        protected override PostFeed Update()
        {
            var latest = _techChrunchFeedUpdater.GetLatest();
            _active = latest;
            return _active;
        }

        public override PostFeed Retrieve()
        {
            return _active ?? Update();
        }
    }
}
