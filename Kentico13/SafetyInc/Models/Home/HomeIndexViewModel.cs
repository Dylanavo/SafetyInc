using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.DocumentEngine.Types.SI;
using Kentico.Content.Web.Mvc;

namespace SafetyInc.Models
{
    public class HomeIndexViewModel
    {
        public string BannerHeader { get; set; }

        public string BannerSubtext { get; set; }

        public IPageAttachmentUrl BannerImage { get; set; }

        public string LoginHeader { get; set; }

        public string LoginBody { get; set; }

        public string LoginButtonText { get; set; }

        public static HomeIndexViewModel GetViewModel(Home home, IPageUrlRetriever pageUrlRetriever, IPageAttachmentUrlRetriever attachmentUrlRetriever)
        {
            if(home == null)
            {
                return null;
            }
            else
            {
                return new HomeIndexViewModel
                {
                    BannerHeader = home.Fields.BannerHeader,
                    BannerSubtext = home.Fields.BannerSubtext,
                    BannerImage = home.Fields.BannerImage == null ? null : attachmentUrlRetriever.Retrieve(home.Fields.BannerImage),
                    LoginHeader = home.Fields.LoginHeader,
                    LoginBody = home.Fields.LoginBody,
                    LoginButtonText = home.Fields.LoginButtonText
                };
            }
        }
    }
}
