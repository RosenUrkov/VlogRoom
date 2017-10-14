using Bytes2you.Validation;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VlogRoom.Data;
using VlogRoom.Data.Models;
using VlogRoom.Services.Common;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Areas.Administration.Models;
using VlogRoom.Web.Common.Attributes;
using VlogRoom.Web.Common.Constants;
using VlogRoom.Web.Common.Extensions;

namespace VlogRoom.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministrationRoleName)]
    public class UsersManageController : Controller
    {
        private readonly IUserDataService userDataService;
        private readonly UserManager<User> userManager;

        public UsersManageController(IUserDataService userDataService, DbContext context)
        {
            Guard.WhenArgument(userDataService, "userDataService").IsNull().Throw();
            this.userDataService = userDataService;

            var userStore = new UserStore<User>(context);
            this.userManager = new UserManager<User>(userStore);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReadUsers([DataSourceRequest] DataSourceRequest request)
        {
            var usersModel = this.userDataService
                .GetAllUsersWithDeleted()
                .Map<User, UserManageViewModel>()
                .Select(x =>
                {
                    x.IsAdmin = this.userManager.IsInRole(x.Id, GlobalConstants.AdministrationRoleName);
                    return x;
                })
                .ToDataSourceResult(request);

            return this.Json(usersModel);
        }

        [SaveChanges]
        public ActionResult UpdateUser(UserManageViewModel userModel)
        {
            if (userModel != null)
            {
                var user = this.userDataService.GetUserByIdWithDeleted(userModel.Id);
                user.UserName = userModel.UserName;
                user.Email = userModel.Email;
                user.RoomName = userModel.RoomName;

                if (user.IsDeleted && !userModel.IsDeleted)
                {
                    user.IsDeleted = userModel.IsDeleted;
                }

                if (userModel.IsAdmin)
                {
                    this.userManager.AddToRole(user.Id, GlobalConstants.AdministrationRoleName);
                }
                else
                {
                    this.userManager.RemoveFromRole(user.Id, GlobalConstants.AdministrationRoleName);
                }

                this.userDataService.UpdateUser(user);

                if (!user.IsDeleted && userModel.IsDeleted)
                {
                    this.userDataService.DeleteUser(user);
                    userModel.DeletedOn = user.DeletedOn;
                }
            }

            return this.Json(new[] { userModel });
        }
    }
}