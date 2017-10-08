using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Services.Models;
using VlogRoom.Web.Common.Extensions;
using VlogRoom.Web.Models;

namespace VlogRoom.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVideoDataService videoDataService;

        public HomeController(IVideoDataService videoDataService)
        {
            Guard.WhenArgument(videoDataService, "videoDataService").IsNull().Throw();
            this.videoDataService = videoDataService;
        }

        public async Task<ActionResult> Index()
        {
            var videos = await this.videoDataService.GetAllVideos(5);
            var videosData = videos.Map<VideoSnippetServiceModel, VideoDataViewModel>();

            return View(videosData);
        }
    }
}