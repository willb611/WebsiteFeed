using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedWebpage.Models
{
    public class FeedList
    {
        private readonly List<FeedItemModel> _feeds;
        private readonly DateTime _timeRetrieved;

        public List<FeedItemModel> Feeds => _feeds;
        public DateTime TimeRetrieved => _timeRetrieved;

        public FeedList(List<FeedItemModel> feeds, DateTime timeRetrieved)
        {
            _feeds = feeds;
            _timeRetrieved = timeRetrieved;
        }

        public FeedList(List<FeedItemModel> feeds) : this(feeds, DateTime.Now)
        {
        }

        public FeedList LimitedToSize(int numberOfItemsInFeed)
        {
            if (_feeds.Count <= numberOfItemsInFeed)
            {
                return this;
            }
            else
            {
                return new FeedList(_feeds.Take(numberOfItemsInFeed).ToList(), _timeRetrieved);
            }
        }
    }
}
