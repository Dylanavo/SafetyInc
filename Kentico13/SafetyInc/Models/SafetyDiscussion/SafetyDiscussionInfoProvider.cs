using CMS.DataEngine;

namespace SafetyInc
{
    /// <summary>
    /// Class providing <see cref="SafetyDiscussionInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(ISafetyDiscussionInfoProvider))]
    public partial class SafetyDiscussionInfoProvider : AbstractInfoProvider<SafetyDiscussionInfo, SafetyDiscussionInfoProvider>, ISafetyDiscussionInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SafetyDiscussionInfoProvider"/> class.
        /// </summary>
        public SafetyDiscussionInfoProvider()
            : base(SafetyDiscussionInfo.TYPEINFO)
        {
        }
    }
}