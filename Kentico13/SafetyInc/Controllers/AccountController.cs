using System;
using System.Net;
using System.Threading.Tasks;

using CMS.Activities.Loggers;
using CMS.Base;
using CMS.Base.UploadExtensions;
using CMS.ContactManagement;
using CMS.Core;
using CMS.Helpers;
using CMS.Membership;

using SafetyInc.Models;

using Kentico.Content.Web.Mvc;
using Kentico.Membership;
using Kentico.Web.Mvc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace SafetyInc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMembershipActivityLogger membershipActivitiesLogger;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IEventLogService eventLogService;
        private readonly ApplicationUserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ISiteService siteService;

        public AccountController(ApplicationUserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 ISiteService siteService,
                                 IMembershipActivityLogger membershipActivitiesLogger,
                                 IStringLocalizer<SharedResources> localizer,
                                 IEventLogService eventLogService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.siteService = siteService;
            this.membershipActivitiesLogger = membershipActivitiesLogger;
            this.localizer = localizer;
            this.eventLogService = eventLogService;
        }

        // GET: Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                //user is authenticated, no need to show the auth
                return Redirect(Url.Kentico().PageUrl("/Home"));
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

                return Redirect(Url.Kentico().PageUrl("/Home"));
            }

            if (signInResult.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, localizer["Your account requires activation before logging in."]);
            }
            else
            {
                ModelState.AddModelError(string.Empty, localizer["Your sign-in attempt was not successful. Please try again."].ToString());
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
            return Redirect(Url.Kentico().PageUrl("/Home"));
        }
    }
}