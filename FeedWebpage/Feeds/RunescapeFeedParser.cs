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
    public class RunescapeFeedParser
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public FeedList ParseFeed(string html)
        {
            if (html == null)
            {
                throw new ArgumentNullException(nameof(html));
            }
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            List<FeedItemModel> feedList = new List<FeedItemModel>();
            foreach (var article in SelectArticleFromDocument(document))
            {
                feedList.Add(ParseArticle(article));
            }
            return new FeedList(feedList);
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

        internal FeedItemModel ParseArticle(HtmlNode article)
        {
            var result = new FeedItemModel();
            var copyPart = article.SelectSingleNode(".//div[@class='copy']");
            var titlePart = copyPart.SelectSingleNode(".//h4").FirstChild;
            var link = titlePart.Attributes["href"].Value;
            result.Link = link;
            result.Title = titlePart.InnerHtml;
            var dateTimeString = copyPart.SelectSingleNode(".//time").Attributes["datetime"].Value;
            result.DateTime = DateTime.Parse(dateTimeString);
            Logger.Debug("Got datetime string {0}, made: {1}", dateTimeString, result.DateTime);
            result.Category = copyPart.SelectSingleNode(".//h5").FirstChild.InnerHtml;
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
