using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Data.Models;

namespace VlogRoom.Services.Data.Contracts
{
    public interface IVideoDataService
    {
        Video GetVideoByServiceId(string videoId);

        IEnumerable<Video> GetAllVideos();

        Task<IEnumerable<Video>> GetAllVideosFromService(int resultsLength);

        Task AddVideo(Stream videoStream, string ownerUsername);

        void RemoveVideo(Video video);

        Task HardRemoveVideo(Video video);
    }
}
