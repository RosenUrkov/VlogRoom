using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Services.Common.Contracts;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Services.Models;

namespace VlogRoom.Services.Data
{
    public class VideoDataService : IVideoDataService
    {
        private readonly IYouTubeService youTubeService;

        public VideoDataService(IYouTubeService youTubeService)
        {
            Guard.WhenArgument(youTubeService, "youTubeService").IsNull().Throw();
            this.youTubeService = youTubeService;
        }

        public IEnumerable<VideoSnippetServiceModel> GetAllVideos(int maxResultsLength)
        {
            return this.youTubeService.GetVideoSnippets(maxResultsLength);
        }

        public async Task UploadVideo(Stream videoStream)
        {
            await this.youTubeService.UploadVideo(videoStream);
        }
    }
}
