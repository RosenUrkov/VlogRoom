using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VlogRoom.Data.Models;
using VlogRoom.Services.Common;
using VlogRoom.Services.Data;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Common.Attributes;
using VlogRoom.Web.Common.Constants;
using VlogRoom.Web.Common.Extensions;
using VlogRoom.Web.Models;

namespace VlogRoom.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserDataService userDataService;

        public UsersController(IUserDataService userDataService)
        {
            Guard.WhenArgument(userDataService, "userDataService").IsNull().Throw();
            this.userDataService = userDataService;
        }

        public ActionResult Room(string id)
        {
            var user = this.userDataService.GetUserById(id);
            if (user == null)
            {
                this.TempData[GlobalConstants.ErrorMessage] = GlobalConstants.InvalidRoomMessage;
                return this.RedirectToAction("Index", "Home");
            }

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
            if (newName == null ||
                newName.Length < GlobalConstants.RoomNameMinLength ||
                newName.Length > GlobalConstants.RoomNameMaxLength ||
                !Regex.IsMatch(newName, GlobalConstants.AlphaNumericalPattern))
            {
                this.TempData[GlobalConstants.ErrorMessage] = GlobalConstants.RoomNameErrorMessage;
                return this.RedirectToAction("Index", "Home");
            }

            var user = this.userDataService.GetUserByUsername(this.User.Identity.Name);
            Guard.WhenArgument(user, "user").IsNull().Throw();

            this.userDataService.RenameRoom(user, newName);
            return this.Content(newName);
        }

        [SaveChanges]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubscribeToUser(string userId)
        {
            var currentUser = this.userDataService.GetUserByUsername(this.User.Identity.Name);
            Guard.WhenArgument(currentUser, "currentUser").IsNull().Throw();

            var userToBeSubscribedTo = this.userDataService.GetUserById(userId);
            if (userToBeSubscribedTo == null)
            {
                this.TempData[GlobalConstants.ErrorMessage] = GlobalConstants.InvalidSubscriptionMessage;
                return this.Redirect(Request.UrlReferrer.ToString());
            }

            this.userDataService.Subscribe(currentUser, userToBeSubscribedTo);

            this.TempData[GlobalConstants.SuccessMessage] = $"You subscribed to {userToBeSubscribedTo.RoomName}!";
            return this.Redirect(Request.UrlReferrer.ToString());
        }

        [SaveChanges]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnsubscribeFromUser(string userId)
        {
            var currentUser = this.userDataService.GetUserByUsername(this.User.Identity.Name);
            Guard.WhenArgument(currentUser, "currentUser").IsNull().Throw();

            var userToBeUnsubscribedFrom = this.userDataService.GetUserById(userId);
            if (userToBeUnsubscribedFrom == null)
            {
                this.TempData[GlobalConstants.ErrorMessage] = GlobalConstants.InvalidSubscriptionMessage;
                return this.Redirect(Request.UrlReferrer.ToString());
            }

            this.userDataService.Unsubscribe(currentUser, userToBeUnsubscribedFrom);

            this.TempData[GlobalConstants.SuccessMessage] = $"You unsubscribed from {userToBeUnsubscribedFrom.RoomName}!";
            return this.Redirect(Request.UrlReferrer.ToString());
        }
    }
}