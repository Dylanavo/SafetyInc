using CMS.Helpers;
using CMS.Membership;
using CMS.SiteProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafetyInc.Resources
{
    public static class SharedData
    {
        public static class Users
        {
            public static Dictionary<int, string> GetUsers()
            {
                return CacheHelper.Cache(cs => LoadUsers(cs), new CacheSettings(30, "customdatasource|users"));
            }

            private static Dictionary<int, string> LoadUsers(CacheSettings cs)
            {
                // Gets the role from the current site
                RoleInfo role = RoleInfo.Provider.Get(Roles.SafetyDiscussionUser, SiteContext.CurrentSiteID);
                Dictionary<int, string> result = new Dictionary<int, string>();

                if (role != null)
                {
                    var roleUserIDs = UserRoleInfo.Provider.Get()
                        .Column(nameof(UserRoleInfo.UserID))
                        .WhereEquals(nameof(UserRoleInfo.RoleID), role.RoleID);

                    result = UserInfo.Provider.Get()
                        .WhereIn(nameof(UserInfo.UserID), roleUserIDs)
                        .OrderBy(nameof(UserInfo.FullName))
                        .ToDictionary(x => x.UserID, x => x.FullName);
                }

                // Checks whether the data should be cached (based on the CacheSettings)
                if (cs.Cached)
                {
                    // Sets a cache dependency for the data
                    // The data is removed from the cache if the objects represented by the dummy key are modified (all user objects and user roles in this case)
                    string[] keys = { "cms.user|all", "cms.userrole|all" };
                    cs.CacheDependency = CacheHelper.GetCacheDependency(keys);
                }

                return result;
            }
        }

        public static class Locations
        {
            public static Dictionary<int, string> GetLocations()
            {
                return CacheHelper.Cache(cs => LoadLocations(cs), new CacheSettings(30, "customdatasource|locations"));
            }

            private static Dictionary<int, string> LoadLocations(CacheSettings cs)
            {
                // Loads location objects from the database
                Dictionary<int, string> result = LocationInfo.Provider.Get()
                    .OrderBy(nameof(LocationInfo.LocationDisplayName))
                    .ToDictionary(x => x.LocationID, x => $"{x.LocationDisplayName} - {x.LocationAddress}");

                // Checks whether the data should be cached (based on the CacheSettings)
                if (cs.Cached)
                {
                    // Sets a cache dependency for the data
                    // The data is removed from the cache if the objects represented by the dummy key are modified (all location objects in this case)
                    cs.CacheDependency = CacheHelper.GetCacheDependency("safetyinc.location|all");
                }

                return result;
            }
        }

        public static class Roles
        {
            public const string SafetyDiscussionUser = "SafetyDiscussionUser";
        }

        public static class Urls
        {
            public const string Home = "/Home";
        }
    }
}
