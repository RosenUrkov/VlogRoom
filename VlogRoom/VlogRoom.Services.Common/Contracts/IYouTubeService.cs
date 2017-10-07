using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Services.Models;

namespace VlogRoom.Services.Common.Contracts
{
    public interface IYouTubeService
    {
        IEnumerable<VideoSnippetServiceModel> GetVideoSnippets(int resultsLength);

        Task<string> UploadVideoIntoThePlaylist(Stream videoStream);
    }
}
