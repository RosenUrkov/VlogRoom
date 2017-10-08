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
        Task<IEnumerable<VideoSnippetServiceModel>> GetAllVideos(int resultsLength);

        Task AddVideo(Stream videoStream);

        Task RemoveVideo(Video video);
    }
}
