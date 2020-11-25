using CMS.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using CMS.Helpers;
using System.ComponentModel;

namespace SafetyInc.Models
{
    public class SafetyDiscussionViewModel
    {
        public int SafetyDiscussionID { get; set; }

        [Required(ErrorMessage = "Please select an observer of the discussion")]
        public int Observer { get; set; }
        
        [DisplayName("Observer")]
        public string ObserverFullName { get; set; }
        
        [Required(ErrorMessage = "Please select colleagues involved in the discussion")]
        public string[] Colleagues { get; set; }

        [DisplayName("Colleagues")]
        public string ColleagueNames { get; set; }

        [Required(ErrorMessage = "Please select time of the discussion")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please select location of the discussion")]
        public int Location { get; set; }

        [DisplayName("Location")]
        public string LocationString { get; set; }

        [Required(ErrorMessage = "Please enter the subject of the discussion")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter the outcomes of the discussion")]
        public string Outcomes { get; set; }

        public static SafetyDiscussionViewModel GetViewModel(SafetyDiscussionInfo safetyDiscussionInfo, bool resolveIDs)
        {
            if (safetyDiscussionInfo != null)
            {
                var safetyDiscViewModel = new SafetyDiscussionViewModel
                {
                    SafetyDiscussionID = safetyDiscussionInfo.SafetyDiscussionID,
                    Date = safetyDiscussionInfo.SafetyDiscussionDate,
                    Subject = safetyDiscussionInfo.SafetyDiscussionSubject,
                    Outcomes = safetyDiscussionInfo.SafetyDiscussionOutcomes,
                    Observer = safetyDiscussionInfo.SafetyDiscussionObserver,
                    Colleagues = safetyDiscussionInfo.SafetyDiscussionColleagues.Split('|'),
                    Location = safetyDiscussionInfo.SafetyDiscussionLocation
                };

                if (resolveIDs)
                {
                    var observerUser = UserInfo.Provider.Get(safetyDiscussionInfo.SafetyDiscussionObserver);
                    safetyDiscViewModel.ObserverFullName = observerUser != null ? observerUser.FullName : "";

                    var colleaguesIDs = safetyDiscussionInfo.SafetyDiscussionColleagues;
                    var colleagueNames = new List<string>();
                    if (!string.IsNullOrEmpty(colleaguesIDs))
                    {
                        var splitIDs = colleaguesIDs.Split('|');
                        if (splitIDs != null && splitIDs.Length > 0)
                        {
                            foreach (var colleagueID in splitIDs)
                            {
                                if (int.TryParse(colleagueID, out int userId))
                                {
                                    var userInfo = UserInfo.Provider.Get(userId);
                                    if (userInfo != null && !string.IsNullOrEmpty(userInfo.FullName))
                                    {
                                        colleagueNames.Add(userInfo.FullName);
                                    }
                                }
                            }
                        }
                    }
                    safetyDiscViewModel.ColleagueNames = colleagueNames.Count > 0 ? colleagueNames.Join(", ") : "";

                    var location = LocationInfo.Provider.Get(safetyDiscussionInfo.SafetyDiscussionLocation);
                    safetyDiscViewModel.LocationString = location != null ? $"{location.LocationDisplayName} - {location.LocationAddress}" : "";
                }

                return safetyDiscViewModel;
            }

            return null;
        }

        public static List<SafetyDiscussionViewModel> GetViewModels(List<SafetyDiscussionInfo> safetyDiscussions)
        {
            var safetyDiscussionViewModels = new List<SafetyDiscussionViewModel>();
            if (safetyDiscussions != null && safetyDiscussions.Count > 0)
            {
                foreach (var safetyDiscInfo in safetyDiscussions)
                {
                    var safetyDiscViewModel = GetViewModel(safetyDiscInfo, true);
                    if (safetyDiscViewModel != null)
                    {
                        safetyDiscussionViewModels.Add(safetyDiscViewModel);
                    }
                }
            }

            return safetyDiscussionViewModels;
        }
    }
}
