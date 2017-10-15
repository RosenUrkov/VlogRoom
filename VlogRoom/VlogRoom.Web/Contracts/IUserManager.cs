using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VlogRoom.Web.Contracts
{
    public interface IUserManager
    {
        bool IsInRole(string id, string roleName);

        void AddToRole(string id, string roleName);

        void RemoveFromRole(string id, string roleName);
    }
}