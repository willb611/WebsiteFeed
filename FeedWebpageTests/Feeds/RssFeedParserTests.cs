using Microsoft.VisualStudio.TestTools.UnitTesting;
using FeedWebpage.Feeds;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedWebpage.Models;
using FeedWebpageTests.Feeds;
using HtmlAgilityPack;
using NLog;

namespace FeedWebpage.Feeds.Tests
{
    [TestClass()]
    public class RssFeedParserTests
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private const string PostTitle = "Example post title";
        private static readonly string PublishDateTime = "Thu, 15 Sep 2016 01:17:30 +0000";
        private static readonly string PostLink = "http://feedproxy.google.com/~r/techcrunch/startups/~3/_Vxhwn_uFeI/";

        private static readonly string Item = "<item>\n" +
         "    <title>" + PostTitle + "</title>\n" +
         "    <link>" + PostLink + "</link>\n" +
         "    <comments>https://techcrunch.com/2016/09/14/the-future-of-human-computer-interaction-will-be-multimodal/#respond</comments>\n" +
         "    <pubDate>" + PublishDateTime + "</pubDate>\n" +
         "    <dc:creator><![CDATA[John Mannes]]></dc:creator>\n" +
         "        <category><![CDATA[Augmented Reality]]></category>\n" +
         "    <category><![CDATA[Entertainment]]></category>\n" +
         "    <category><![CDATA[Hardware]]></category>\n" +
         "    <category><![CDATA[Startups]]></category>\n" +
         "    <category><![CDATA[TC]]></category>\n" +
         "    <category><![CDATA[Virtual Reality]]></category>\n" +
         "    <category><![CDATA[Software]]></category>\n" +
         "    <category><![CDATA[iPhone]]></category>\n" +
         "    <category><![CDATA[touchscreen]]></category>\n" +
         "    <category><![CDATA[Computer Mouse]]></category>\n" +
         "    <category><![CDATA[Disrupt]]></category>\n" +
         "    <category><![CDATA[disrupt sf]]></category>\n" +
         "    <category><![CDATA[Disrupt SF 2016]]></category>\n" +
         "    <guid isPermaLink=\"false\">http://techcrunch.com/?p=1387649</guid>\n" +
         "    <description>&lt;img width=\"680\" height=\"453\" src=\"https://tctechcrunch2011.files.wordpress.com/2016/09/disrupt_sf16_michael_buckwald_jim_margraff-4387.jpg?w=680\" class=\"attachment-large size-large wp-post-image\" alt=\"disrupt_sf16_michael_buckwald_jim_margraff-4387\" /&gt;&amp;nbsp;We have been using the computer mouse for decades to interact with our technology. Touchscreens brought us a new way to input commands to our gadgets, but they rely on the same fundamental idea of the click. Even the new 3D touch on the iPhone 7 is just an incredibly sophisticated way of using the hand to answer a yes or no question. Both Michael Buckwald, CEO of Leap Motion and Jim Marggraff,&amp;hellip; &lt;a href=\"https://techcrunch.com/2016/09/14/the-future-of-human-computer-interaction-will-be-multimodal/?ncid=rss\"&gt;Read More&lt;/a&gt;&lt;img src=\"http://feeds.feedburner.com/~r/techcrunch/startups/~4/_Vxhwn_uFeI\" height=\"1\" width=\"1\" alt=\"\"/&gt;</description>\n" +
         "    <wfw:commentRss>https://techcrunch.com/2016/09/14/the-future-of-human-computer-interaction-will-be-multimodal/feed/</wfw:commentRss>\n" +
         "    <slash:comments>0</slash:comments>\n" +
         "  <media:thumbnail url=\"https://tctechcrunch2011.files.wordpress.com/2016/09/disrupt_sf16_michael_buckwald_jim_margraff-4387.jpg?w=210&amp;h=158&amp;crop=1\" />\n" +
         "<media:content url=\"http://tctechcrunch2011.files.wordpress.com/2016/09/disrupt_sf16_michael_buckwald_jim_margraff-4387.jpg\" type=\"image/jpeg\" medium=\"image\"><media:title type=\"html\">The%20future%20of%20human%20computer%20interaction%20will%20be%26nbsp%3Bmultimodal</media:title></media:content>\n" +
         "    <media:thumbnail url=\"https://tctechcrunch2011.files.wordpress.com/2016/09/disrupt_sf16_michael_buckwald_jim_margraff-4387.jpg\" />\n" +
         "    <media:content url=\"http://tctechcrunch2011.files.wordpress.com/2016/09/disrupt_sf16_michael_buckwald_jim_margraff-4387.jpg\" medium=\"image\">\n" +
         "      <media:title type=\"html\">disrupt_sf16_michael_buckwald_jim_margraff-4387</media:title>\n" +
         "    </media:content>\n" +
         "\n" +
         "    <media:content url=\"http://1.gravatar.com/avatar/a457b6dcacd1964d0b23ebfe8767f7d4?s=96&amp;d=identicon&amp;r=G\" medium=\"image\">\n" +
         "      <media:title type=\"html\">jmannes16</media:title>\n" +
         "    </media:content>\n" +
         "  <feedburner:origLink>https://techcrunch.com/2016/09/14/the-future-of-human-computer-interaction-will-be-multimodal/?ncid=rss</feedburner:origLink></item>";



        [TestMethod()]
        public void ParseItem_ShouldWork()
        {
            var document = new HtmlDocument();
            HtmlNode.ElementsFlags.Remove("link");
            document.LoadHtml(HtmlHelper.PageThenBodyStartHtml + HtmlEntity.DeEntitize(Item) + HtmlHelper.PageThenBodyEndHtml);
            var itemNode = document.DocumentNode.SelectSingleNode("//item");
            var expected =
                new FeedItemModel.Builder().WithTitle(PostTitle)
                    .WithDateTime(DateTime.Parse(PublishDateTime))
                    .WithLink(PostLink)
                    .Build();
            Logger.Debug("Using itemNode with html: {0}", itemNode.OuterHtml);

            var parser = new RssFeedParser(2);
            var actual = parser.ParseItem(itemNode);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Link, actual.Link);
            Assert.AreEqual(expected.Category, expected.Category);
            Assert.AreEqual(expected.Summary, actual.Summary);
            Assert.AreEqual(expected.DateTime, actual.DateTime);
            LogManager.Flush();
            LogManager.Shutdown();
        }
    }
}
