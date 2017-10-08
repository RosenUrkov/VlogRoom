using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VlogRoom.Services.Data;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Common.Attributes;
using VlogRoom.Web.Models;

namespace VlogRoom.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IVideoDataService videoDataService;

        public UsersController(IVideoDataService videoDataService)
        {
            Guard.WhenArgument(videoDataService, "videoDataService").IsNull().Throw();

            this.videoDataService = videoDataService;
        }

        public ActionResult Room(int id)
        {
            return View();
        }

        [Authorize]
        public ActionResult Account()
        {
            return View();
        }

        [SaveChanges]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadVideo(HttpPostedFileBase video)
        {
            await this.videoDataService.AddVideo(video.InputStream, this.User.Identity.Name);
            return this.RedirectToAction("Account");
        }

        [SaveChanges]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteVideo(string videoId)
        {
            var video = this.videoDataService.GetVideo(videoId);
            await this.videoDataService.RemoveVideo(video);
            return this.RedirectToAction("Account");
        }
    }
}