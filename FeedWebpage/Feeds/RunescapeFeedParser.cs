using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedWebpage.Models;
using HtmlAgilityPack;
using NLog;

namespace FeedWebpage.Feeds
{
    public class RunescapeFeedParser : IFeedParserTemplate
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly int _maxItemsInFeed;

        public RunescapeFeedParser(int maxItemsInFeed)
        {
            _maxItemsInFeed = maxItemsInFeed;
        }

        public RunescapeFeedParser() : this(5)
        {
        }

        public PostFeed ParseHtml(string html)
        {
            if (html == null)
            {
                throw new ArgumentNullException(nameof(html));
            }
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            List<PostModel> feedList = new List<PostModel>();
            foreach (var article in SelectArticleFromDocument(document))
            {
                feedList.Add(ParseArticle(article));
            }
            return new PostFeed(feedList).LimitedToSize(_maxItemsInFeed);
        }

        private HtmlNodeCollection SelectArticleFromDocument(HtmlDocument document)
        {
            var newsElement = NewsElementFromRoot(document.DocumentNode);
            return ArticlesFromNewsSection(NewsSectionElementFromNewsElement(newsElement));
        }

        internal HtmlNode NewsElementFromRoot(HtmlNode documentNode)
        {
            return documentNode.SelectSingleNode("//div[@id='news']");
        }

        internal PostModel ParseArticle(HtmlNode article)
        {
            var copyPart = article.SelectSingleNode(".//div[@class='copy']");
            var titlePart = copyPart.SelectSingleNode(".//h4").FirstChild;
            var resultBuilder = new PostModel.Builder().WithTitle(titlePart.InnerHtml);
            var link = titlePart.Attributes["href"].Value;
            var dateTimeString = copyPart.SelectSingleNode(".//time").Attributes["datetime"].Value;
            resultBuilder = resultBuilder.WithDateTime(DateTime.Parse(dateTimeString));
            var category = copyPart.SelectSingleNode(".//h5").FirstChild.InnerHtml;
            var result = resultBuilder.WithLink(link).WithCategory(category).Build();
            Logger.Debug("Got datetime string {0}, made: {1}", dateTimeString, result.DateTime);
            Logger.Debug("Got category as {0}", result.Category);
            return result;
        }

        internal HtmlNodeCollection ArticlesFromNewsSection(HtmlNode newsElement)
        {
            if (newsElement == null)
            {
                throw new ArgumentNullException(nameof(newsElement));
            }
            return newsElement.SelectNodes(".//article");
        }

        internal HtmlNode NewsSectionElementFromNewsElement(HtmlNode newsElement)
        {
            if (newsElement == null)
            {
                throw new ArgumentNullException(nameof(newsElement));
            }
            return newsElement.SelectSingleNode(".//div[@id='newsSection']");
        }
    }
}
