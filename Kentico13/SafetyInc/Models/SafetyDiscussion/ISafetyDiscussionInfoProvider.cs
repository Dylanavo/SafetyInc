using CMS.DataEngine;

namespace SafetyInc
{
    /// <summary>
    /// Declares members for <see cref="SafetyDiscussionInfo"/> management.
    /// </summary>
    public partial interface ISafetyDiscussionInfoProvider : IInfoProvider<SafetyDiscussionInfo>, IInfoByIdProvider<SafetyDiscussionInfo>
    {
    }
}