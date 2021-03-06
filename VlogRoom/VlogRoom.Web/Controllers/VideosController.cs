﻿using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VlogRoom.Data.Models;
using VlogRoom.Services.Common;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Common.Attributes;
using VlogRoom.Web.Common.Constants;
using VlogRoom.Web.Common.Extensions;
using VlogRoom.Web.Models;

namespace VlogRoom.Web.Controllers
{
    public class VideosController : Controller
    {
        private readonly IVideoDataService videoDataService;
        private readonly IUserDataService userDataService;

        public VideosController(IUserDataService userDataService, IVideoDataService videoDataService)
        {
            Guard.WhenArgument(videoDataService, "videoDataService").IsNull().Throw();
            Guard.WhenArgument(userDataService, "userDataService").IsNull().Throw();

            this.userDataService = userDataService;
            this.videoDataService = videoDataService;
        }

        [SaveChanges]
        public ActionResult Watch(string id)
        {
            var video = this.videoDataService.GetVideoByServiceId(id);
            if (video == null)
            {
                this.TempData[GlobalConstants.ErrorMessage] = GlobalConstants.InvalidVideoMessage;
                return this.RedirectToAction("Index", "Home");
            }

            video.Views += 1;
            this.videoDataService.UpdateVideo(video);

            var watchModel = new WatchVideoViewModel();
            watchModel.Video = MappingService.Provider.Map<SingleVideoViewModel>(video);
            watchModel.WatchNext = this.videoDataService.GetMostViralVideos(6)
                                        .Where(x => x.Id != video.Id)
                                        .Map<Video, VideoDataViewModel>();

            return this.View("Video", watchModel);
        }

        public ActionResult NewsFeed()
        {
            var currentUser = this.userDataService.GetUserByUsername(this.User.Identity.Name);
            Guard.WhenArgument(currentUser, "currentUser").IsNull().Throw();

            var newsFeed = this.videoDataService.GetNewsFeed(currentUser).Map<Video, VideoDataViewModel>();
            return View(newsFeed);
        }

        [Authorize]
        [HttpGet]
        public ActionResult UploadVideo()
        {
            return this.View();
        }

        [SaveChanges]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadVideo(UploadVideoViewModel video)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData[GlobalConstants.ErrorMessage] = GlobalConstants.InvalidUploadVideoMessage;
                return this.RedirectToAction("Index", "Home");
            }

            var currentUser = this.userDataService.GetUserByUsername(this.User.Identity.Name);
            Guard.WhenArgument(currentUser, "currentUser").IsNull().Throw();

            await this.videoDataService.AddVideo(video.VideoFile.InputStream, video.VideoTitle, video.VideoDescription, currentUser);

            this.TempData[GlobalConstants.SuccessMessage] = "Video uploaded successfully!";
            return this.RedirectToAction("Account", "Users");
        }

        [SaveChanges]
        [Authorize]
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteVideo(string videoId)
        {
            var video = this.videoDataService.GetVideoByServiceId(videoId);
            if (video == null)
            {
                this.TempData[GlobalConstants.ErrorMessage] = GlobalConstants.InvalidDeleteVideoMessage;
                return this.RedirectToAction("Account", "Users");
            }

            this.videoDataService.DeleteVideo(video);
            return new EmptyResult();
        }
    }
}