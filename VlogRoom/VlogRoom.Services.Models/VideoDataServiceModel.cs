using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Data.Models;
using VlogRoom.Services.Common.Contracts;

namespace VlogRoom.Services.Models
{
    public class VideoDataServiceModel : IMap<Video>
    {
        public string ServiceVideoId { get; set; }

        public string ServiceListItemId { get; set; }
    }
}
