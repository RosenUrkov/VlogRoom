using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VlogRoom.Data.Models;
using VlogRoom.Services.Common;
using VlogRoom.Services.Data;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Common.Attributes;
using VlogRoom.Web.Common.Extensions;
using VlogRoom.Web.Models;

namespace VlogRoom.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IVideoDataService videoDataService;
        private readonly IUserDataService userDataService;

        public UsersController(IUserDataService userDataService, IVideoDataService videoDataService)
        {
            Guard.WhenArgument(videoDataService, "videoDataService").IsNull().Throw();
            Guard.WhenArgument(userDataService, "userDataService").IsNull().Throw();

            this.userDataService = userDataService;
            this.videoDataService = videoDataService;
        }

        public ActionResult Room(string id)
        {
            var user = this.userDataService.GetUserById(id);
            Guard.WhenArgument(user, "user").IsNull().Throw();

            var userModel = MappingService.Provider.Map<UserDataViewModel>(user);
            return View(userModel);
        }

        [Authorize]
        public ActionResult Account()
        {
            return View();
        }

        public ActionResult AllUsers()
        {
            var model = this.userDataService.GetAllUsers().Map<User, UserDataViewModel>();

            throw new NotImplementedException();
        }

        [Authorize]
        public ActionResult DailyFeed()
        {
            var currentUser = this.userDataService.GetUserByUsername(this.User.Identity.Name);
            var model = currentUser.Subscribers.SelectMany(x => x.Videos);

            throw new NotImplementedException();
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
        public ActionResult DeleteVideo(string videoId)
        {
            var video = this.videoDataService.GetVideoByServiceId(videoId);
            this.videoDataService.RemoveVideo(video);
            return this.RedirectToAction("Account");
        }

        [SaveChanges]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> HardDeleteVideo(string videoId)
        {
            var video = this.videoDataService.GetVideoByServiceId(videoId);
            await this.videoDataService.HardRemoveVideo(video);
            return this.RedirectToAction("Account");
        }

        [SaveChanges]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubscribeToUser(string userId)
        {
            var currentUser = this.userDataService.GetUserByUsername(this.User.Identity.Name);
            var userToBeSubscribedTo = this.userDataService.GetUserById(userId);

            this.userDataService.Subscribe(currentUser, userToBeSubscribedTo);
            return this.RedirectToAction("Account");
        }
    }
}