using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VlogRoom.Data.Models;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Models;

namespace VlogRoom.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IVideoDataService videoDataService;

        public UsersController(IUserDataService userDataService, IVideoDataService videoDataService)
        {
            Guard.WhenArgument(userDataService, "userDataService").IsNull().Throw();
            Guard.WhenArgument(videoDataService, "videoDataService").IsNull().Throw();

            this.videoDataService = videoDataService;
            this.userDataService = userDataService;
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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadVideo(HttpPostedFileBase video)
        {
            await this.videoDataService.AddVideo(video.InputStream);
            return this.RedirectToAction("Account");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteVideo(string videoId)
        {
            var video = new Video();

            await this.videoDataService.RemoveVideo(video);
            return this.RedirectToAction("Account");
        }
    }
}