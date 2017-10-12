using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VlogRoom.Data.Models;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Common.Extensions;
using VlogRoom.Web.Models;

namespace VlogRoom.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVideoDataService videoDataService;

        public HomeController(IUserDataService userDataService, IVideoDataService videoDataService)
        {
            Guard.WhenArgument(videoDataService, "videoDataService").IsNull().Throw();
            this.videoDataService = videoDataService;
        }

        public ActionResult Index()
        {
            var videoCollectionsModel = new VideoCollectionsViewModel();
            videoCollectionsModel.RecentVideos = this.videoDataService.GetMostRecentVideos(5).Map<Video, VideoDataViewModel>();
            videoCollectionsModel.ViralVideos = this.videoDataService.GetMostViralVideos(5).Map<Video, VideoDataViewModel>().ToList();

            return View(videoCollectionsModel);
        }
    }
}