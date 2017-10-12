using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Data.Models;

namespace VlogRoom.Services.Common.Contracts
{
    public interface IYouTubeService
    {
        Task<Video> UploadVideo(Stream videoStream, string videoTitle, string videoDescription);

        Task DeleteVideo(Video video);
    }
}
