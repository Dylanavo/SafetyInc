using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using SafetyInc;

[assembly: RegisterObjectType(typeof(SafetyDiscussionInfo), SafetyDiscussionInfo.OBJECT_TYPE)]

namespace SafetyInc
{
    /// <summary>
    /// Data container class for <see cref="SafetyDiscussionInfo"/>.
    /// </summary>
    [Serializable]
    public partial class SafetyDiscussionInfo : AbstractInfo<SafetyDiscussionInfo, ISafetyDiscussionInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "safetyinc.safetydiscussion";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(SafetyDiscussionInfoProvider), OBJECT_TYPE, "SafetyInc.SafetyDiscussion", "SafetyDiscussionID", "SafetyDiscussionLastModified", "SafetyDiscussionGuid", null, null, null, null, null, null)
        {
            ModuleName = "SafetyInc",
            TouchCacheDependencies = true,
        };


        /// <summary>
        /// Safety discussion ID.
        /// </summary>
        [DatabaseField]
        public virtual int SafetyDiscussionID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("SafetyDiscussionID"), 0);
            }
            set
            {
                SetValue("SafetyDiscussionID", value);
            }
        }


        /// <summary>
        /// Safety discussion guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid SafetyDiscussionGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("SafetyDiscussionGuid"), Guid.Empty);
            }
            set
            {
                SetValue("SafetyDiscussionGuid", value);
            }
        }


        /// <summary>
        /// Safety discussion last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime SafetyDiscussionLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("SafetyDiscussionLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("SafetyDiscussionLastModified", value);
            }
        }


        /// <summary>
        /// Safety discussion created by.
        /// </summary>
        [DatabaseField]
        public virtual int SafetyDiscussionCreatedBy
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("SafetyDiscussionCreatedBy"), 0);
            }
            set
            {
                SetValue("SafetyDiscussionCreatedBy", value);
            }
        }


        /// <summary>
        /// Safety discussion observer.
        /// </summary>
        [DatabaseField]
        public virtual int SafetyDiscussionObserver
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("SafetyDiscussionObserver"), 0);
            }
            set
            {
                SetValue("SafetyDiscussionObserver", value);
            }
        }


        /// <summary>
        /// Safety discussion colleagues.
        /// </summary>
        [DatabaseField]
        public virtual string SafetyDiscussionColleagues
        {
            get
            {
                return ValidationHelper.GetString(GetValue("SafetyDiscussionColleagues"), String.Empty);
            }
            set
            {
                SetValue("SafetyDiscussionColleagues", value);
            }
        }


        /// <summary>
        /// Safety discussion date.
        /// </summary>
        [DatabaseField]
        public virtual DateTime SafetyDiscussionDate
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("SafetyDiscussionDate"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("SafetyDiscussionDate", value);
            }
        }


        /// <summary>
        /// Safety discussion location.
        /// </summary>
        [DatabaseField]
        public virtual int SafetyDiscussionLocation
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("SafetyDiscussionLocation"), 0);
            }
            set
            {
                SetValue("SafetyDiscussionLocation", value);
            }
        }


        /// <summary>
        /// Safety discussion subject.
        /// </summary>
        [DatabaseField]
        public virtual string SafetyDiscussionSubject
        {
            get
            {
                return ValidationHelper.GetString(GetValue("SafetyDiscussionSubject"), String.Empty);
            }
            set
            {
                SetValue("SafetyDiscussionSubject", value);
            }
        }


        /// <summary>
        /// Safety discussion outcomes.
        /// </summary>
        [DatabaseField]
        public virtual string SafetyDiscussionOutcomes
        {
            get
            {
                return ValidationHelper.GetString(GetValue("SafetyDiscussionOutcomes"), String.Empty);
            }
            set
            {
                SetValue("SafetyDiscussionOutcomes", value);
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
        protected SafetyDiscussionInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="SafetyDiscussionInfo"/> class.
        /// </summary>
        public SafetyDiscussionInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="SafetyDiscussionInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public SafetyDiscussionInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}