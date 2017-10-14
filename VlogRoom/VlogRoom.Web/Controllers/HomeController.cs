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
        private readonly IUserDataService userDataService;

        public HomeController(IUserDataService userDataService, IVideoDataService videoDataService)
        {
            Guard.WhenArgument(videoDataService, "videoDataService").IsNull().Throw();
            Guard.WhenArgument(userDataService, "userDataService").IsNull().Throw();

            this.videoDataService = videoDataService;
            this.userDataService = userDataService;
        }

        public ActionResult Index()
        {
            var videoCollectionsModel = new VideoCollectionsViewModel();
            videoCollectionsModel.RecentVideos = this.videoDataService.GetMostRecentVideos(3).Map<Video, VideoDataViewModel>();
            videoCollectionsModel.ViralVideos = this.videoDataService.GetMostViralVideos(8).Map<Video, VideoDataViewModel>().ToList();

            return View(videoCollectionsModel);
        }

        public ActionResult Search(string searchPattern)
        {
            var videosModel = this.videoDataService.GetAllVideos(searchPattern).Map<Video, VideoDataViewModel>();
            return View(videosModel);
        }
    }
}