using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VlogRoom.Data.Models;
using VlogRoom.Services.Common;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Common.Attributes;
using VlogRoom.Web.Common.Constants;
using VlogRoom.Web.Common.Extensions;
using VlogRoom.Web.Models;

namespace VlogRoom.Web.Controllers
{
    public class VideosController : Controller
    {
        private readonly IVideoDataService videoDataService;
        private readonly IUserDataService userDataService;

        public VideosController(IUserDataService userDataService, IVideoDataService videoDataService)
        {
            Guard.WhenArgument(videoDataService, "videoDataService").IsNull().Throw();
            Guard.WhenArgument(userDataService, "userDataService").IsNull().Throw();

            this.userDataService = userDataService;
            this.videoDataService = videoDataService;
        }

        [SaveChanges]
        public ActionResult Watch(string id)
        {
            var video = this.videoDataService.GetVideoByServiceId(id);
            video.Views += 1;
            this.videoDataService.UpdateVideo(video);

            var watchModel = new WatchVideoViewModel();
            watchModel.Video = MappingService.Provider.Map<SingleVideoViewModel>(video);
            watchModel.WatchNext = this.videoDataService.GetMostViralVideos(5)
                                        .Where(x => x.Id != video.Id)
                                        .Map<Video, VideoDataViewModel>();

            return this.View("Video", watchModel);
        }

        public ActionResult NewsFeed()
        {
            var currentUser = this.userDataService.GetUserByUsername(this.User.Identity.Name);
            var newsFeed = this.videoDataService.GetNewsFeed(currentUser).Map<Video, VideoDataViewModel>();

            return View(newsFeed);
        }

        [Authorize]
        [HttpGet]
        public ActionResult UploadVideo()
        {
            return this.View();
        }

        [SaveChanges]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadVideo(HttpPostedFileBase video, string videoTitle, string videoDescription)
        {
            await this.videoDataService.AddVideo(video.InputStream, videoTitle, videoDescription, this.User.Identity.Name);

            this.TempData[GlobalConstants.SuccessMessage] = "Video uploaded successfully!";
            return this.RedirectToAction("Account", "Users");
        }

        [SaveChanges]
        [Authorize]
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteVideo(string videoId)
        {
            var video = this.videoDataService.GetVideoByServiceId(videoId);
            this.videoDataService.RemoveVideo(video);
            
            return new EmptyResult();
        }

        [SaveChanges]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> HardDeleteVideo(string videoId)
        {
            var video = this.videoDataService.GetVideoByServiceId(videoId);
            await this.videoDataService.HardRemoveVideo(video);
            return this.RedirectToAction("Account", "Users");
        }
    }
}