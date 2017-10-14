using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VlogRoom.Data.Models;
using VlogRoom.Web.Contracts;

namespace VlogRoom.Web.Models
{
    public class UserDataSearchResultViewModel: IMap<User>
    {
        public string Id { get; set; }

        public string RoomName { get; set; }
    }
}