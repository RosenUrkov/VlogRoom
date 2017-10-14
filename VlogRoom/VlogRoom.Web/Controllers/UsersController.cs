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
        private readonly IUserDataService userDataService;

        public UsersController(IUserDataService userDataService, IVideoDataService videoDataService)
        {
            Guard.WhenArgument(userDataService, "userDataService").IsNull().Throw();
            this.userDataService = userDataService;
        }

        public ActionResult Room(string id)
        {
            var user = this.userDataService.GetUserById(id);
            Guard.WhenArgument(user, "user").IsNull().Throw();

            if (this.User.Identity.IsAuthenticated && user.UserName == this.User.Identity.Name)
            {
                return this.RedirectToAction("Account");
            }

            var userModel = MappingService.Provider.Map<UserDataViewModel>(user);
            return View(userModel);
        }

        [Authorize]
        public ActionResult Account()
        {
            var user = this.userDataService.GetUserByUsername(this.User.Identity.Name);
            Guard.WhenArgument(user, "user").IsNull().Throw();

            var userModel = MappingService.Provider.Map<UserDataViewModel>(user);
            return View(userModel);
        }

        [SaveChanges]
        [Authorize]
        [AjaxOnly]
        [HttpPost]
        public ActionResult RenameRoom(string newName)
        {
            var user = this.userDataService.GetUserByUsername(this.User.Identity.Name);
            var renamedUser = this.userDataService.RenameRoom(user, newName);
            return new EmptyResult();
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
            return this.Redirect(Request.UrlReferrer.ToString());
        }

        [SaveChanges]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnsubscribeFromUser(string userId)
        {
            var currentUser = this.userDataService.GetUserByUsername(this.User.Identity.Name);
            var userToBeUnsubscribedFrom = this.userDataService.GetUserById(userId);

            this.userDataService.Unsubscribe(currentUser, userToBeUnsubscribedFrom);
            return this.Redirect(Request.UrlReferrer.ToString());
        }
    }
}