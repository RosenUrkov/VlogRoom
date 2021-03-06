﻿using Bytes2you.Validation;
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

        public VideoDataService(IEfRepository<Video> videosRepo, IYouTubeService youTubeService)
        {
            Guard.WhenArgument(videosRepo, "videos repository").IsNull().Throw();
            Guard.WhenArgument(youTubeService, "youTubeService").IsNull().Throw();

            this.videosRepo = videosRepo;
            this.youTubeService = youTubeService;
        }

        public Video GetVideoByServiceId(string serviceVideoId)
        {
            return this.videosRepo.All.FirstOrDefault(x => x.ServiceVideoId == serviceVideoId);
        }

        public Video GetVideoByServiceIdWithDeleted(string serviceVideoId)
        {
            return this.videosRepo.AllAndDeleted.FirstOrDefault(x => x.ServiceVideoId == serviceVideoId);
        }

        public IEnumerable<Video> GetAllVideos(string searchPattern = "")
        {
            searchPattern = searchPattern.ToLower();

            return this.videosRepo.All
                .Where(x => x.Title.ToLower().Contains(searchPattern) ||
                            x.Description.ToLower().Contains(searchPattern) ||
                            x.User.UserName.ToLower().Contains(searchPattern) ||
                            x.User.RoomName.ToLower().Contains(searchPattern))
                .AsEnumerable();
        }

        public IEnumerable<Video> GetAllVideosWithDeleted()
        {
            return this.videosRepo.AllAndDeleted.AsEnumerable();
        }

        public IEnumerable<Video> GetNewsFeed(User user)
        {
            Guard.WhenArgument(user, "user").IsNull().Throw();

            return user.Subscribtions
                .SelectMany(x => x.Videos)
                .Where(x => !x.IsDeleted && x.CreatedOn.Value.Day == DateService.Provider.GetCurrentDate().Day)
                .AsEnumerable();
        }

        public IEnumerable<Video> GetMostRecentVideos(int count)
        {
            return this.videosRepo.All.OrderByDescending(x => x.CreatedOn).Take(count).AsEnumerable();
        }

        public IEnumerable<Video> GetMostViralVideos(int count)
        {
            return this.videosRepo.All.OrderByDescending(x => x.Views).Take(count).AsEnumerable();
        }

        public async Task AddVideo(Stream videoStream, string videoTitle, string videoDescription, User owner)
        {
            Guard.WhenArgument(videoStream, "videoStream").IsNull().Throw();
            Guard.WhenArgument(owner, "owner").IsNull().Throw();

            var video = await this.youTubeService.UploadVideo(videoStream, videoTitle, videoDescription);

            video.User = owner;
            this.videosRepo.Add(video);
        }

        public void UpdateVideo(Video video)
        {
            Guard.WhenArgument(video, "video").IsNull().Throw();
            this.videosRepo.Update(video);
        }

        public void DeleteVideo(Video video)
        {
            Guard.WhenArgument(video, "video").IsNull().Throw();
            this.videosRepo.Delete(video);
        }

        public async Task HardRemoveVideo(Video video)
        {
            Guard.WhenArgument(video, "video").IsNull().Throw();

            await this.youTubeService.DeleteVideo(video);
            this.videosRepo.HardDelete(video);
        }
    }
}
