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
        private readonly IEfRepository<Video> videosRepo;
        private readonly IEfRepository<User> usersRepo;

        public VideoDataService(IEfRepository<Video> videosRepo, IEfRepository<User> usersRepo, IYouTubeService youTubeService)
        {
            Guard.WhenArgument(videosRepo, "videos repository").IsNull().Throw();
            Guard.WhenArgument(usersRepo, "users repository").IsNull().Throw();
            Guard.WhenArgument(youTubeService, "youTubeService").IsNull().Throw();

            this.videosRepo = videosRepo;
            this.usersRepo = usersRepo;
            this.youTubeService = youTubeService;
        }

        public VideoDataServiceModel GetVideo(string videoId)
        {
            var video = this.videosRepo.All.FirstOrDefault(x => x.ServiceVideoId == videoId);
            return MappingService.Provider.Map<VideoDataServiceModel>(video);
        }

        public async Task<IEnumerable<VideoSnippetServiceModel>> GetAllVideosSnippets(int maxResultsLength)
        {
            return await this.youTubeService.GetVideoSnippets(maxResultsLength);
        }       

        public async Task AddVideo(Stream videoStream, string ownerUsername)
        {
            var videoData = await this.youTubeService.UploadVideo(videoStream);

            var video = MappingService.Provider.Map<Video>(videoData);
            var user = this.usersRepo.All.FirstOrDefault(x => x.UserName == ownerUsername);
            video.User = user;

            this.videosRepo.Add(video);
        }

        public async Task RemoveVideo(VideoDataServiceModel videoData)
        {
            await this.youTubeService.DeleteVideo(videoData);
            var video = this.videosRepo.All.FirstOrDefault(x => x.ServiceVideoId == videoData.ServiceVideoId);
            this.videosRepo.Delete(video);
        }
    }
}
