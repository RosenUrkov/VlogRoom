using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Data.Models;
using VlogRoom.Data.Repository;
using VlogRoom.Services.Common;
using VlogRoom.Services.Common.Contracts;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Services.Models;

namespace VlogRoom.Services.Data
{
    public class VideoDataService : IVideoDataService
    {
        private readonly IYouTubeService youTubeService;
        private readonly IEfRepository<Video> repo;

        public VideoDataService(IEfRepository<Video> repo, IYouTubeService youTubeService)
        {
            Guard.WhenArgument(repo, "repository").IsNull().Throw();
            Guard.WhenArgument(youTubeService, "youTubeService").IsNull().Throw();

            this.repo = repo;
            this.youTubeService = youTubeService;
        }

        public async Task<IEnumerable<VideoSnippetServiceModel>> GetAllVideos(int maxResultsLength)
        {
            return await this.youTubeService.GetVideoSnippets(maxResultsLength);
        }

        public async Task AddVideo(Stream videoStream)
        {
            var videoData = await this.youTubeService.UploadVideo(videoStream);
            var video = MappingService.Provider.Map<Video>(videoData);
            this.repo.Add(video);
        }

        public async Task RemoveVideo(Video video)
        {
            var videoData = MappingService.Provider.Map<VideoDataServiceModel>(video);
            await this.youTubeService.DeleteVideo(videoData);
            this.repo.Delete(video);
        }
    }
}
