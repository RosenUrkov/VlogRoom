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

        public Video GetVideo(string serviceVideoId)
        {
           return this.videosRepo.All.FirstOrDefault(x => x.ServiceVideoId == serviceVideoId);
        }

        public Video GetVideoWithDeleted(string serviceVideoId)
        {
            return this.videosRepo.AllAndDeleted.FirstOrDefault(x => x.ServiceVideoId == serviceVideoId);
        }

        public IEnumerable<Video> GetAllVideos()
        {
            return this.videosRepo.All.AsEnumerable();
        }

        public async Task<IEnumerable<Video>> GetAllVideosFromService(int maxResultsLength)
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

        public void RemoveVideo(Video video)
        {
            this.videosRepo.Delete(video);
        }

        public async Task HardRemoveVideo(Video video)
        {
            await this.youTubeService.DeleteVideo(video);
            this.videosRepo.HardDelete(video);
        }
    }
}
