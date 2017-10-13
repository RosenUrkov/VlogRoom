using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VlogRoom.Web.Models
{
    public class SearchCollectionsViewModel
    {
        public IEnumerable<UserDataSearchResult> FoundUsers { get; set; }

        public IEnumerable<VideoDataViewModel> FoundVideos { get; set; }
    }
}