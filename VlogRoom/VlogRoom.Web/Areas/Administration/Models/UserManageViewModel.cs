using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using VlogRoom.Data.Models;
using VlogRoom.Web.Contracts;
using VlogRoom.Web.Common.Constants;
using Microsoft.AspNet.Identity;

namespace VlogRoom.Web.Areas.Administration.Models
{
    public class UserManageViewModel : IMap<User>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }
        
        public string RoomName { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsAdmin { get; set; }
    }
}