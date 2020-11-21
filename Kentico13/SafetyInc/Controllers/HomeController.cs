using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.DocumentEngine.Types.SI;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using SafetyInc.Models;
using SafetyInc.Controllers;
using Kentico.Content.Web.Mvc.Routing;

[assembly: RegisterPageRoute(Home.CLASS_NAME, typeof(HomeController))]

namespace SafetyInc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPageDataContextRetriever pageDataContextRetriever;
        private readonly IPageUrlRetriever pageUrlRetriever;
        private readonly IPageAttachmentUrlRetriever attachmentUrlRetriever;

        public HomeController(IPageDataContextRetriever pageDataContextRetriever,
            IPageUrlRetriever pageUrlRetriever,
            IPageAttachmentUrlRetriever attachmentUrlRetriever)
        {
            this.pageDataContextRetriever = pageDataContextRetriever;
            this.pageUrlRetriever = pageUrlRetriever;
            this.attachmentUrlRetriever = attachmentUrlRetriever;
        }

        public async Task<ActionResult> Index()
        {
            var home = pageDataContextRetriever.Retrieve<Home>().Page;
            var viewModel = HomeIndexViewModel.GetViewModel(home, pageUrlRetriever, attachmentUrlRetriever);
            return View(viewModel);
        }
    }
}