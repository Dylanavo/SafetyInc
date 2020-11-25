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

        public string BannerManageText { get; set; }

        public string BannerNewDiscussionText { get; set; }

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
                    BannerManageText = home.Fields.BannerManageText,
                    BannerNewDiscussionText = home.Fields.BannerNewDiscussionText
                };
            }
        }
    }
}
