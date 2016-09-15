using FeedWebpage.Feeds;

namespace FeedWebpage.Models.FeedCaches
{
    internal class TechCrunchFeedCache : FeedCache
    {
        private readonly TechChrunchFeedUpdater _techChrunchFeedUpdater = new TechChrunchFeedUpdater();

        private volatile FeedList _active;

        public override void Clear()
        {
            _active = null;
            Update();
        }

        protected override FeedList Update()
        {
            var latest = _techChrunchFeedUpdater.GetLatest();
            _active = latest;
            return _active;
        }

        public override FeedList Retrieve()
        {
            return _active ?? Update();
        }
    }
}
