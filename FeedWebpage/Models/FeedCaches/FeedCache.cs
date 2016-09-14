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
        protected abstract FeedList Update();
        public virtual FeedList Retrieve()
        {
            return Update();
        }
    }
}
