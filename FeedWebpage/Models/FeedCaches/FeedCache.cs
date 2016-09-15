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

        public abstract void Clear();
        protected abstract PostFeed Update();
        public virtual PostFeed Retrieve()
        {
            return Update();
        }
    }
}
