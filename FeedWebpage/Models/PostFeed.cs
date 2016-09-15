using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedWebpage.Models
{
    public class PostFeed
    {
        private readonly List<PostModel> _posts;
        private readonly DateTime _timeRetrieved;

        public List<PostModel> Posts => _posts;
        public DateTime TimeRetrieved => _timeRetrieved;

        public PostFeed(List<PostModel> posts, DateTime timeRetrieved)
        {
            _posts = posts;
            _timeRetrieved = timeRetrieved;
        }

        public PostFeed(List<PostModel> posts) : this(posts, DateTime.Now)
        {
        }

        public PostFeed LimitedToSize(int numberOfItemsInFeed)
        {
            if (_posts.Count <= numberOfItemsInFeed)
            {
                return this;
            }
            else
            {
                return new PostFeed(_posts.Take(numberOfItemsInFeed).ToList(), _timeRetrieved);
            }
        }
    }
}
