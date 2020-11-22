using CMS.DataEngine;

namespace SafetyInc
{
    /// <summary>
    /// Class providing <see cref="LocationInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(ILocationInfoProvider))]
    public partial class LocationInfoProvider : AbstractInfoProvider<LocationInfo, LocationInfoProvider>, ILocationInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationInfoProvider"/> class.
        /// </summary>
        public LocationInfoProvider()
            : base(LocationInfo.TYPEINFO)
        {
        }
    }
}