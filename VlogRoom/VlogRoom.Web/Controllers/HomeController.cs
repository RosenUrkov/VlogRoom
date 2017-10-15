using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VlogRoom.Data.Models;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Common.Constants;
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
            return View();
        }

        public ActionResult Search(string searchPattern)
        {
            if (searchPattern == null || !Regex.IsMatch(searchPattern, GlobalConstants.AlphaNumericalPattern))
            {
                this.TempData[GlobalConstants.ErrorMessage] = GlobalConstants.InvalidSearchPatternMessage;
                return this.RedirectToAction("Index");
            }

            var videosModel = this.videoDataService.GetAllVideos(searchPattern).Map<Video, VideoDataViewModel>();
            return View(videosModel);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public ActionResult GetRecentVideos()
        {
            var recentVideos = this.videoDataService.GetMostRecentVideos(3).Map<Video, VideoDataViewModel>();
            return this.PartialView("_RecentVideos", recentVideos);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public ActionResult GetViralVideos()
        {
            var viralVideos = this.videoDataService.GetMostViralVideos(8).Map<Video, VideoDataViewModel>().ToList();
            return this.PartialView("_ViralVideos", viralVideos);
        }
    }
}