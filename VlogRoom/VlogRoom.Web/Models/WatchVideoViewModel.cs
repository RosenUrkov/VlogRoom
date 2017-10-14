using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VlogRoom.Web.Models
{
    public class WatchVideoViewModel
    {
        public SingleVideoViewModel Video { get; set; }

        public IEnumerable<VideoDataViewModel> WatchNext { get; set; }
    }
}