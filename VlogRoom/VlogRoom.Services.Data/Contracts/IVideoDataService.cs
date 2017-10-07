using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Services.Models;

namespace VlogRoom.Services.Data.Contracts
{
    public interface IVideoDataService
    {
        IEnumerable<VideoSnippetServiceModel> GetAllVideos(int resultsLength);

        Task<string> UploadVideo(Stream videoStream);
    }
}
