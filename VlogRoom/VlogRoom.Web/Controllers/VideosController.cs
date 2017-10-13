using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VlogRoom.Services.Common;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Common.Attributes;
using VlogRoom.Web.Models;

namespace VlogRoom.Web.Controllers
{
    public class VideosController : Controller
    {
        private readonly IVideoDataService videoDataService;

        public VideosController(IVideoDataService videoDataService)
        {
            Guard.WhenArgument(videoDataService, "videoDataService").IsNull().Throw();
            this.videoDataService = videoDataService;
        }

        public ActionResult Single(string id)
        {
            var video = MappingService.Provider.Map<SingleVideoViewModel>(this.videoDataService.GetVideoByServiceId(id));
            return this.View("Video", video);
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
            return this.RedirectToAction("Account", "Users");
        }

        [SaveChanges]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteVideo(string videoId)
        {
            var video = this.videoDataService.GetVideoByServiceId(videoId);
            this.videoDataService.RemoveVideo(video);
            return this.RedirectToAction("Account", "Users");
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