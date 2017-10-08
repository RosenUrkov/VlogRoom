using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Data.Models;
using VlogRoom.Services.Models;

namespace VlogRoom.Services.Data.Contracts
{
    public interface IVideoDataService
    {
        VideoDataServiceModel GetVideo(string videoId);

        Task<IEnumerable<VideoSnippetServiceModel>> GetAllVideosSnippets(int resultsLength);

        Task AddVideo(Stream videoStream, string ownerUsername);

        Task RemoveVideo(VideoDataServiceModel video);
    }
}
