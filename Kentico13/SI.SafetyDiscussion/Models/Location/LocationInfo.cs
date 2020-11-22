using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using SafetyInc;

[assembly: RegisterObjectType(typeof(LocationInfo), LocationInfo.OBJECT_TYPE)]

namespace SafetyInc
{
    /// <summary>
    /// Data container class for <see cref="LocationInfo"/>.
    /// </summary>
    [Serializable]
    public partial class LocationInfo : AbstractInfo<LocationInfo, ILocationInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "safetyinc.location";


        /// <summary>
        /// Type information.
        /// </summary>
#warning "You will need to configure the type info."
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(LocationInfoProvider), OBJECT_TYPE, "SafetyInc.Location", "LocationID", "LocationLastModified", "LocationGuid", "LocationCodeName", "LocationDisplayName", null, null, null, null)
        {
            ModuleName = "SafetyInc",
            TouchCacheDependencies = true,
        };


        /// <summary>
        /// Location ID.
        /// </summary>
        [DatabaseField]
        public virtual int LocationID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("LocationID"), 0);
            }
            set
            {
                SetValue("LocationID", value);
            }
        }


        /// <summary>
        /// Location guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid LocationGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("LocationGuid"), Guid.Empty);
            }
            set
            {
                SetValue("LocationGuid", value);
            }
        }


        /// <summary>
        /// Location last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime LocationLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("LocationLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("LocationLastModified", value);
            }
        }


        /// <summary>
        /// Location display name.
        /// </summary>
        [DatabaseField]
        public virtual string LocationDisplayName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("LocationDisplayName"), String.Empty);
            }
            set
            {
                SetValue("LocationDisplayName", value);
            }
        }


        /// <summary>
        /// Location code name.
        /// </summary>
        [DatabaseField]
        public virtual string LocationCodeName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("LocationCodeName"), String.Empty);
            }
            set
            {
                SetValue("LocationCodeName", value);
            }
        }


        /// <summary>
        /// Location address.
        /// </summary>
        [DatabaseField]
        public virtual string LocationAddress
        {
            get
            {
                return ValidationHelper.GetString(GetValue("LocationAddress"), String.Empty);
            }
            set
            {
                SetValue("LocationAddress", value, String.Empty);
            }
        }


        /// <summary>
        /// Location latitude.
        /// </summary>
        [DatabaseField]
        public virtual double LocationLatitude
        {
            get
            {
                return ValidationHelper.GetDouble(GetValue("LocationLatitude"), 0d);
            }
            set
            {
                SetValue("LocationLatitude", value, 0d);
            }
        }


        /// <summary>
        /// Location longitude.
        /// </summary>
        [DatabaseField]
        public virtual double LocationLongitude
        {
            get
            {
                return ValidationHelper.GetDouble(GetValue("LocationLongitude"), 0d);
            }
            set
            {
                SetValue("LocationLongitude", value, 0d);
            }
        }


        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            Provider.Delete(this);
        }


        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            Provider.Set(this);
        }


        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected LocationInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="LocationInfo"/> class.
        /// </summary>
        public LocationInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="LocationInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public LocationInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}