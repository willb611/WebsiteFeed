using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedWebpage.Models
{
    public class FeedItemModel
    {
        public DateTime DateTime { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Category { get; set; }
    }
}
