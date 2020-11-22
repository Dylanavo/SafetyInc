using CMS.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafetyInc.Models
{
    public class SafetyDiscussionViewModel
    {
        public int SafetyDiscussionID { get; set; }
        public string CreatedBy { get; set; }
        public string Observer { get; set; }
        public List<string> Colleagues { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Subject { get; set; }
        public string Outcomes { get; set; }

        public static SafetyDiscussionViewModel GetViewModel(SafetyDiscussionInfo safetyDiscussionInfo)
        {
            if (safetyDiscussionInfo != null)
            {
                //first create a new view model object and set all the variables
                //that don't require mapping
                var safetyDiscViewModel = new SafetyDiscussionViewModel
                {
                    SafetyDiscussionID = safetyDiscussionInfo.SafetyDiscussionID,
                    Date = safetyDiscussionInfo.SafetyDiscussionDate,
                    Subject = safetyDiscussionInfo.SafetyDiscussionSubject,
                    Outcomes = safetyDiscussionInfo.SafetyDiscussionOutcomes
                };

                //get relevant data from IDs for rest of the variables
                var createdUser = UserInfo.Provider.Get(safetyDiscussionInfo.SafetyDiscussionCreatedBy);
                if(createdUser != null)
                {
                    safetyDiscViewModel.CreatedBy = createdUser.FullName;
                }

                var observerUser = UserInfo.Provider.Get(safetyDiscussionInfo.SafetyDiscussionObserver);
                if(observerUser != null)
                {
                    safetyDiscViewModel.Observer = observerUser.FullName;
                }

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
                safetyDiscViewModel.Colleagues = colleagueNames;

                var location = LocationInfo.Provider.Get(safetyDiscussionInfo.SafetyDiscussionLocation);
                if(location != null)
                {
                    safetyDiscViewModel.Location = $"{location.LocationDisplayName} - {location.LocationAddress}";
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
                    var safetyDiscViewModel = GetViewModel(safetyDiscInfo);
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
