using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VlogRoom.Data.Models;
using VlogRoom.Services.Common.Contracts;
using VlogRoom.Web.Contracts;

namespace VlogRoom.Web.Models
{
    public class VideoDataViewModel : IMap<Video>
    {
        public string ServiceVideoId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}