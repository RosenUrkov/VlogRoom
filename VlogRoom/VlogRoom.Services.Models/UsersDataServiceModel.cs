using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Data.Models;
using VlogRoom.Services.Common.Contracts;

namespace VlogRoom.Services.Models
{
    public class UsersDataServiceModel : IMap<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public IEnumerable<VideoDataServiceModel> Videos { get; set; }
    }
}
