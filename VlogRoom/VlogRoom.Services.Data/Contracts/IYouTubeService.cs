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
        Task<IEnumerable<Video>> GetVideoSnippets(int resultsLength);

        Task<Video> UploadVideo(Stream videoStream);

        Task DeleteVideo(Video video);
    }
}
