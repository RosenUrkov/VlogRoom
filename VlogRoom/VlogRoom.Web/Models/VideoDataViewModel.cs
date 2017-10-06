﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VlogRoom.Services.Models;
using VlogRoom.Web.Contracts;

namespace VlogRoom.Web.Models
{
    public class VideoDataViewModel : IMap<VideoSnippetServiceModel>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}