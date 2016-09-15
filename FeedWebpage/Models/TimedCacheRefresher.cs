using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using FeedWebpage.Models.FeedCaches;
using NLog;

namespace FeedWebpage.Models
{
    public class TimedCacheRefresher
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly FeedCache _feedCache;

        public TimedCacheRefresher(FeedCache feedCache) : this(feedCache, 5)
        {
        }

        public TimedCacheRefresher(FeedCache feedCache, int minutesBetweenEviction)
        {
            _feedCache = feedCache;
            feedCache.Refresh();
            StartTimerToEvictCacheEveryNumberOfMinutes(minutesBetweenEviction);
        }

        private void StartTimerToEvictCacheEveryNumberOfMinutes(int minutesBetweenEviction)
        {
            Timer timer = new Timer();
            timer.Elapsed += TimerOnElapsed;
            timer.Interval = minutesBetweenEviction*1000*60;
            timer.Enabled = true;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Logger.Info("Refreshing cache: {0}", _feedCache);
            _feedCache.Refresh();
        }
    }
}
