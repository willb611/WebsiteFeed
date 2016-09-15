using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedWebpage.Models;
using HtmlAgilityPack;
using NLog;

namespace FeedWebpage.Feeds
{
    public class RssFeedParser : IFeedParserTemplate
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly int _maxItemsInFeed;

        public RssFeedParser(int maxItemsInFeed)
        {
            _maxItemsInFeed = maxItemsInFeed;
        }

        public PostFeed ParseHtml(string html)
        {
            HtmlDocument document = new HtmlDocument();
            ModifyHtmlAgilityStaticSettings();
            document.LoadHtml(html);
            var items = GetItemsFromPage(document);
            List<PostModel> feedItemModels = new List<PostModel>();
            foreach (var item in items)
            {
                feedItemModels.Add(ParseItem(item));
            }
            return new PostFeed(feedItemModels).LimitedToSize(_maxItemsInFeed);
        }

        private void ModifyHtmlAgilityStaticSettings()
        {
            // Needed because otherwise closing <link> tag is removed. Ugly and static,
            // hopefully this won't break anything in other places
            HtmlNode.ElementsFlags.Remove("link");
        }

        internal HtmlNodeCollection GetItemsFromPage(HtmlDocument document)
        {
            return document.DocumentNode.SelectNodes("//item");
        }

        internal PostModel ParseItem(HtmlNode item)
        {
            var builder = new PostModel.Builder();
            builder = builder.WithTitle(item.SelectSingleNode("./title").InnerHtml);
            var link = LinkUrlFromItem(item);
            builder = builder.WithLink(link);
            // This should be ./pubDate but for some reason htmlAgility converts it into lowercase
            var dateTimeString = item.SelectSingleNode("./pubdate").InnerHtml;
            builder = builder.WithDateTime(DateTime.Parse(dateTimeString));
            return builder.Build();
        }
        private string LinkUrlFromItem(HtmlNode item)
        {
            var linkNode = item.SelectSingleNode("./link");
            return linkNode.InnerHtml;
        }
    }
}
