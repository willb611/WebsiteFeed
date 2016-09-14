using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedWebpage.Models
{
    public class FeedList
    {
        private List<FeedItemModel> _feeds;
        private DateTime _timeRetrieved;
        private List<FeedItemModel> feedList;

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
    }
}
