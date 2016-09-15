using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedWebpage.Models
{
    public class FeedItemModel
    {
        private readonly DateTime _dateTime;
        private readonly string _link;
        private readonly string _title;
        private readonly string _summary;
        private readonly string _category;


        public DateTime DateTime => _dateTime;
        public string Link => _link;
        public string Title => _title;
        public string Summary => _summary;
        public string Category => _category;
        
        private FeedItemModel(DateTime dateTime, string link, string title, string summary, string category)
        {
            _dateTime = dateTime;
            _link = link;
            _title = title;
            _summary = summary;
            _category = category;
        }

        public class Builder
        {
            private DateTime _dateTime;
            private string _link;
            private string _title;
            private string _summary;
            private string _category;

            public Builder WithDateTime(DateTime dateTime)
            {
                _dateTime = dateTime;
                return this;
            }
            public Builder WithLink(string link)
            {
                _link = link;
                return this;
            }
            public Builder WithTitle(string title)
            {
                _title = title;
                return this;
            }
            public Builder WithSummary(string summary)
            {
                _summary = summary;
                return this;
            }
            public Builder WithCategory(string category)
            {
                _category = category;
                return this;
            }

            public FeedItemModel Build()
            {
                return new FeedItemModel(_dateTime, _link, _title, _summary, _category);
            }
        }
    }
}
