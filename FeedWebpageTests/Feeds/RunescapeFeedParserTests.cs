using Microsoft.VisualStudio.TestTools.UnitTesting;
using FeedWebpage.Feeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using NLog;

namespace FeedWebpage.Feeds.Tests
{

    [TestClass()]
    public class RunescapeFeedParserTests
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static string newsSection = "<div id='newsSection'>\n" +
      "                      <article>test</article>\n" +
      "                      <article class=\"video\">test2</article>\n" +
      "                    </div>";

        private static string newsElement = "<div id='news' class='tabbedContent'>" +
                                                "<div class='categories'><h6>Categories</h6></div>\n"
                                                + newsSection +
                                                "\n<div class=\"btnWrap more\">" +
                                                    "<div class=\"btn\"><a href=\"http://services.runescape.com/m=news/list\"><span>More News</span></a></div>" +
                                                "</div>" +
                                            "</div>";
        private string page = "  <!doctype html>\n" +
      "  <!--[if lt IE 7]><html class=\"no-js lt-ie10 lt-ie9 lt-ie8 lt-ie7\" lang=\"en\"><![endif]-->\n" +
      "  <!--[if (IE 7)&!(IEMobile)]><html class=\"no-js lt-ie10 lt-ie9 lt-ie8\" lang=\"en\"><![endif]-->\n" +
      "  <!--[if (IE 8)&!(IEMobile)]><html class=\"no-js lt-ie10 lt-ie9\" lang=\"en\"><![endif]-->\n" +
      "  <!--[if (IE 9)&!(IEMobile)]><html class=\"no-js lt-ie10\" lang=\"en\"><![endif]-->\n" +
      "  <!--[if gt IE 9]><!--><!-- x --> <html class=\"no-js\" lang=\"en\"> <!--<![endif]-->\n" +
      "  <head>\n" +
      "    <title>RuneScape Online Community - Forums, News, Events and more</title>\n" +
      "  </head>\n" +
      "  <body id=\"home\" class=\"home en\" itemscope itemtype=\"http://schema.org/WebPage\">\n" +
      "    <div class=\"stickyWrap\">\n" +
      "<div class='contents'>\n" +
      "  <main class='main' role='main'>\n" +
      "    <div class='promoBoxes' id='promos'>\n" +
      "      <div class='inner'>\n" +
      "      </div>\n" +
      "    </div>\n" +
      "    <p class='fifteen-years'>Celebrating 15 years of RuneScape</p>\n" +
      "    <div class='clear'>\n" +
      "      <div class='tabbedElement' id='tabs'>\n" +
      "        <div class='inner'>\n" +
      "          <div class='tabbedContents'>\n" +
      "            " + newsElement +
      "          </div>\n" +
      "        </div>\n" +
      "      </div>\n" +
      "    </div>\n" +
      "  </main>\n" +
      "</div>\n" +
      "    </div>\n" +
      "    </body>\n" +
      "  </html>\n";


        [TestMethod()]
        public void NewsElementFromPageShouldWork()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(page);

            var parser = new RunescapeFeedParser();
            var newsElementNode = parser.NewsElementFromRoot(doc.DocumentNode);
            Assert.AreEqual(newsElement.Trim(), newsElementNode.OuterHtml.Trim());
        }

        [TestMethod()]
        public void NewsSectionElementFromNewsElement_ShouldWork()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml("<html><body>\n" + newsElement + "\n</body></html>");
            var bodyNode = doc.DocumentNode.SelectSingleNode("//body");
            Logger.Info("Using bodyNode with innerhtml: {0}", bodyNode.InnerHtml);

            var parser = new RunescapeFeedParser();
            var newsSectionElementFromNewsElementNode = parser.NewsSectionElementFromNewsElement(bodyNode);
            Assert.AreEqual(newsSection, newsSectionElementFromNewsElementNode.OuterHtml);
        }

        [TestMethod()]
        public void ArticlesFromSection_ShouldWork()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml("<html><body>" + newsSection + "</body></html>");
            var bodyNode = doc.DocumentNode.SelectSingleNode("//body");
            Logger.Info("Using bodyNode with innerhtml: {0}", bodyNode.InnerHtml);

            var parser = new RunescapeFeedParser();


            var resultNodes = parser.ArticlesFromNewsSection(bodyNode);
            var node1Text = "test";
            var node2Text = "test2";
            var nodesSeen = 0;
            foreach (var resultNode in resultNodes)
            {
                if (nodesSeen == 0)
                {
                    Assert.AreEqual(node1Text, resultNode.InnerHtml);
                }
                else if (nodesSeen == 1)
                {
                    Assert.AreEqual(node2Text, resultNode.InnerHtml);
                }
                nodesSeen++;
            }
            Assert.AreEqual(2, nodesSeen);
        }
    }
}
