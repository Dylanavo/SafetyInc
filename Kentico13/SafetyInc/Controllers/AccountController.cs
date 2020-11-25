using System;
using System.Net;
using System.Threading.Tasks;

using CMS.Activities.Loggers;
using CMS.ContactManagement;
using CMS.Core;

using SafetyInc.Models;

using Kentico.Content.Web.Mvc;
using Kentico.Membership;
using Kentico.Web.Mvc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace SafetyInc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMembershipActivityLogger membershipActivitiesLogger;
        private readonly IEventLogService eventLogService;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager,
                                 IMembershipActivityLogger membershipActivitiesLogger,
                                 IEventLogService eventLogService)
        {
            this.signInManager = signInManager;
            this.membershipActivitiesLogger = membershipActivitiesLogger;
            this.eventLogService = eventLogService;
        }

        public ActionResult Index()
        {
            return RedirectToAction(nameof(Login));
        }

        // GET: Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                //user is authenticated, no need to show the auth,
                //send them straight through to manage their discussions
                return RedirectToAction("Index", "SafetyDiscussion");
            }
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var signInResult = SignInResult.Failed;

            try
            {
                signInResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.StaySignedIn, false);
            }
            catch (Exception ex)
            {
                eventLogService.LogException("AccountController", "Login", ex);
            }

            if (signInResult.Succeeded)
            {
                ContactManagementContext.UpdateUserLoginContact(model.UserName);

                membershipActivitiesLogger.LogLogin(model.UserName);

                var decodedReturnUrl = WebUtility.UrlDecode(returnUrl);
                if (!string.IsNullOrEmpty(decodedReturnUrl) && Url.IsLocalUrl(decodedReturnUrl))
                {
                    return Redirect(decodedReturnUrl);
                }

                return Redirect(Url.Kentico().PageUrl(Resources.SharedResources.Urls.Home));
            }

            if (signInResult.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "Your account requires activation before logging in.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Your sign-in attempt was not successful. Please try again.");
            }

            return View(model);
        }

        // POST: Account/Logout
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            signInManager.SignOutAsync();
            return Redirect(Url.Kentico().PageUrl(Resources.SharedResources.Urls.Home));
        }
    }
}