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
        Video GetVideoByServiceId(string serviceVideoId);

        Video GetVideoByServiceIdWithDeleted(string serviceVideoId);

        IEnumerable<Video> GetAllVideos(string searchPattern = "");

        IEnumerable<Video> GetAllVideosWithDeleted();

        IEnumerable<Video> GetMostRecentVideos(int count);

        IEnumerable<Video> GetMostViralVideos(int count);

        IEnumerable<Video> GetRecommendedVideos(User currentUser, int count);

        Task AddVideo(Stream videoStream, string videoTitle, string videoDescription, string ownerUsername);

        void UpdateVideo(Video video);

        void RemoveVideo(Video video);

        Task HardRemoveVideo(Video video);
    }
}
