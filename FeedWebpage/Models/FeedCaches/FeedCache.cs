using System.Diagnostics.CodeAnalysis;

namespace FeedWebpage.Models.FeedCaches
{
    public abstract class FeedCache
    {
        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        protected FeedCache()
        {
            Update();
        }

        public abstract void Refresh();
        protected abstract PostFeed Update();
        public virtual PostFeed Get()
        {
            return Update();
        }
    }
}
