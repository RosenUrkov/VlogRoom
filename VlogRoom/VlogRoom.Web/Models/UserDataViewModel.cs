using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VlogRoom.Data.Models;
using VlogRoom.Services.Common.Contracts;
using VlogRoom.Web.Contracts;

namespace VlogRoom.Web.Models
{
    public class UserDataViewModel : IMap<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public IEnumerable<VideoDataViewModel> Videos { get; set; }
    }
}