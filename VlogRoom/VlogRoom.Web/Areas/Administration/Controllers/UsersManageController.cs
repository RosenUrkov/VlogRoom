using Bytes2you.Validation;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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

        public UsersManageController(IUserDataService userDataService)
        {
            Guard.WhenArgument(userDataService, "userDataService").IsNull().Throw();
            this.userDataService = userDataService;
        }

        public ActionResult Index()
        {      
            return View();
        }

        public ActionResult ReadUsers([DataSourceRequest] DataSourceRequest request)
        {
            var usersModel = this.userDataService.GetAllUsersWithDeleted().Map<User, UserManageViewModel>();
            //foreach (var user in usersModel)
            //{
            //    user.IsAdmin = Roles.IsUserInRole(user.UserName, GlobalConstants.AdministrationRoleName);
            //}

            return this.Json(usersModel);
        }

        [SaveChanges]
        public ActionResult UpdateUser(UserManageViewModel userModel)
        {
            if (userModel != null)
            {
                var user = MappingService.Provider.Map<User>(userModel);
                this.userDataService.UpdateUser(user);
            }

            return this.Json(new [] { userModel });
        }

        [SaveChanges]
        public ActionResult DeleteUser(UserManageViewModel userModel)
        {
            if (userModel != null)
            {
                var user = MappingService.Provider.Map<User>(userModel);
                this.userDataService.DeleteUser(user);
            }

            return this.Json(new[] { userModel });
        }        
    }
}