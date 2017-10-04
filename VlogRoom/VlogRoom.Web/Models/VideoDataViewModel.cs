using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VlogRoom.Web.Models
{
    public class VideoDataViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool IsValid { get; set; }
    }
}