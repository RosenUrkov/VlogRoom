using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VlogRoom.Data.Models;
using VlogRoom.Services.Common.Contracts;
using VlogRoom.Web.Contracts;

namespace VlogRoom.Web.Models
{
    public class VideoCollectionsViewModel
    {
        public IEnumerable<VideoDataViewModel> RecentVideos { get; set; }

        public IList<VideoDataViewModel> ViralVideos { get; set; }
    }
}