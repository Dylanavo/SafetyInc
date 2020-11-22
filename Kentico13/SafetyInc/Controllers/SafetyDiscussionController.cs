using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using SafetyInc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CMS.Membership;

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
                //discussions where they are the creater or observer
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
            var safetyDiscViewModel = SafetyDiscussionViewModel.GetViewModel(safetyDisc);
            return View(safetyDiscViewModel);
        }

        // GET: SafetyDiscussion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SafetyDiscussion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SafetyDiscussion/Edit/5
        public ActionResult Edit(int id)
        {
            var safetyDisc = SafetyDiscussionInfo.Provider.Get(id);
            var safetyDiscViewModel = SafetyDiscussionViewModel.GetViewModel(safetyDisc);
            return View(safetyDiscViewModel);
        }

        // POST: SafetyDiscussion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SafetyDiscussion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SafetyDiscussion/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //gets a discussion for a user based on discussion id - only returns the object
        //if the user should actually have access to it
        private static SafetyDiscussionInfo GetDiscussionInfo(ClaimsPrincipal user, int discId)
        {
            var userInfo = UserInfo.Provider.Get(user.Identity.Name);
            if(userInfo != null)
            {
                var discInfo = SafetyDiscussionInfo.Provider.Get(discId);
                if(discInfo != null && (discInfo.SafetyDiscussionObserver == userInfo.UserID || discInfo.SafetyDiscussionCreatedBy == userInfo.UserID))
                {
                    return discInfo;
                }
            }

            return null;
        }
    }
}