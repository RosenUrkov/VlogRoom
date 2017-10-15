using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VlogRoom.Data.Models;
using VlogRoom.Web.Contracts;

namespace VlogRoom.Web.Providers
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<User> userManager;

        public UserManager(DbContext context)
        {
            var userStore = new UserStore<User>(context);
            this.userManager = new UserManager<User>(userStore);
        }

        public bool IsInRole(string id, string roleName)
        {
            return this.userManager.IsInRole(id, roleName);
        }

        public void AddToRole(string id, string roleName)
        {
            this.userManager.AddToRole(id, roleName);
        }

        public void RemoveFromRole(string id, string roleName)
        {
            this.userManager.RemoveFromRole(id, roleName);
        }
    }
}