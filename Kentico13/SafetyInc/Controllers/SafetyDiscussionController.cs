using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using SafetyInc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CMS.Membership;
using CMS.Helpers;

namespace SafetyInc.Controllers
{
    [Authorize]
    public class SafetyDiscussionController : Controller
    {
        // GET: SafetyDiscussion
        public ActionResult Index()
        {
            var userInfo = UserInfo.Provider.Get(User.Identity.Name);
            List<SafetyDiscussionInfo> safetyDiscs;
            if (userInfo.CheckPrivilegeLevel(CMS.Base.UserPrivilegeLevelEnum.Editor))
            {
                //if user is editor or above show all safety discussions
                safetyDiscs = SafetyDiscussionInfo.Provider.Get().ToList();
            }
            else
            {
                //if this is a normal user with no priviledges then just show
                //discussions where they are the creator or observer
                safetyDiscs = SafetyDiscussionInfo.Provider.Get()
                    .WhereEquals(nameof(SafetyDiscussionInfo.SafetyDiscussionCreatedBy), userInfo.UserID)
                    .Or()
                    .WhereEquals(nameof(SafetyDiscussionInfo.SafetyDiscussionObserver), userInfo.UserID)
                    .ToList();
            }
            var safetyDiscViewModels = SafetyDiscussionViewModel.GetViewModels(safetyDiscs);
            return View(safetyDiscViewModels);
        }

        // GET: SafetyDiscussion/Details/5
        public ActionResult Details(int id)
        {
            var safetyDisc = GetDiscussionInfo(User, id);
            var safetyDiscViewModel = SafetyDiscussionViewModel.GetViewModel(safetyDisc, true);
            if(safetyDiscViewModel != null)
            {
                return View(safetyDiscViewModel);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: SafetyDiscussion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SafetyDiscussion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SafetyDiscussionViewModel sdvm)
        {
            if (!ModelState.IsValid)
            {
                return View(sdvm);
            }

            try
            {
                var userInfo = UserInfo.Provider.Get(User.Identity.Name);

                //Technically we should also do validation on the user ids passed in to make
                //sure they are all associated with users who have the appropriate role. We
                //should also do some extra validation on date/location - and potentially
                //do sanitization on Subject/Outcomes (although there are issues associated
                //with allowing rich text input and then trying to clean out undesirable 
                //elements - markdown would probably be a better option)
                var newDiscussion = new SafetyDiscussionInfo
                {
                    SafetyDiscussionCreatedBy = userInfo.UserID,
                    SafetyDiscussionObserver = sdvm.Observer,
                    SafetyDiscussionDate = sdvm.Date,
                    SafetyDiscussionLocation = sdvm.Location,
                    SafetyDiscussionSubject = sdvm.Subject,
                    SafetyDiscussionOutcomes = sdvm.Outcomes
                };
                if(sdvm.Colleagues != null && sdvm.Colleagues.Length > 0)
                {
                    newDiscussion.SafetyDiscussionColleagues = sdvm.Colleagues.Join("|");
                }
                newDiscussion.Insert();
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(sdvm);
            }
        }

        // GET: SafetyDiscussion/Edit/5
        public ActionResult Edit(int id)
        {
            var safetyDisc = GetDiscussionInfo(User, id);
            var safetyDiscViewModel = SafetyDiscussionViewModel.GetViewModel(safetyDisc, false);
            if(safetyDiscViewModel != null)
            {
                return View(safetyDiscViewModel);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: SafetyDiscussion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SafetyDiscussionViewModel sdvm)
        {
            if (!ModelState.IsValid)
            {
                return View(sdvm);
            }

            try
            {
                var safetyDisc = GetDiscussionInfo(User, id);
                if(safetyDisc != null)
                {
                    safetyDisc.SafetyDiscussionObserver = sdvm.Observer;
                    safetyDisc.SafetyDiscussionDate = sdvm.Date;
                    safetyDisc.SafetyDiscussionLocation = sdvm.Location;
                    safetyDisc.SafetyDiscussionSubject = sdvm.Subject;
                    safetyDisc.SafetyDiscussionOutcomes = sdvm.Outcomes;
                }
                if (sdvm.Colleagues != null && sdvm.Colleagues.Length > 0)
                {
                    safetyDisc.SafetyDiscussionColleagues = sdvm.Colleagues.Join("|");
                }
                safetyDisc.Update();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(sdvm);
            }
        }

        // GET: SafetyDiscussion/Delete/5
        public ActionResult Delete(int id)
        {
            var safetyDisc = GetDiscussionInfo(User, id);
            var safetyDiscViewModel = SafetyDiscussionViewModel.GetViewModel(safetyDisc, true);
            if(safetyDiscViewModel != null)
            {
                return View(safetyDiscViewModel);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: SafetyDiscussion/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SafetyDiscussionViewModel sdvm)
        {
            //make sure they actually have access to this discussion
            var discussion = GetDiscussionInfo(User, id);
            if (discussion != null)
            {
                discussion.Delete();
            }

            return RedirectToAction(nameof(Index));
        }

        //gets a discussion for a user based on discussion id - only returns the object
        //if the user should actually have access to it
        private static SafetyDiscussionInfo GetDiscussionInfo(ClaimsPrincipal user, int discId)
        {
            var userInfo = UserInfo.Provider.Get(user.Identity.Name);
            if(userInfo != null)
            {
                var discInfo = SafetyDiscussionInfo.Provider.Get(discId);
                if(discInfo != null && (userInfo.CheckPrivilegeLevel(CMS.Base.UserPrivilegeLevelEnum.Editor) || discInfo.SafetyDiscussionObserver == userInfo.UserID || discInfo.SafetyDiscussionCreatedBy == userInfo.UserID))
                {
                    return discInfo;
                }
            }

            return null;
        }
    }
}