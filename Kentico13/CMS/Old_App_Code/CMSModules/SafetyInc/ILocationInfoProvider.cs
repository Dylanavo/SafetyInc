using CMS.DataEngine;

namespace SafetyInc
{
    /// <summary>
    /// Declares members for <see cref="LocationInfo"/> management.
    /// </summary>
    public partial interface ILocationInfoProvider : IInfoProvider<LocationInfo>, IInfoByIdProvider<LocationInfo>, IInfoByNameProvider<LocationInfo>
    {
    }
}