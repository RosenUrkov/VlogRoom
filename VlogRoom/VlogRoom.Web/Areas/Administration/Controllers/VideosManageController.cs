using Bytes2you.Validation;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Web.Mvc;
using VlogRoom.Data.Models;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Areas.Administration.Models;
using VlogRoom.Web.Common.Attributes;
using VlogRoom.Web.Common.Constants;
using VlogRoom.Web.Common.Extensions;

namespace VlogRoom.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministrationRoleName)]
    public class VideosManageController : Controller
    {
        private readonly IVideoDataService videoDataService;

        public VideosManageController(IVideoDataService videoDataService)
        {
            Guard.WhenArgument(videoDataService, "videoDataService").IsNull().Throw();
            this.videoDataService = videoDataService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReadVideos([DataSourceRequest] DataSourceRequest request)
        {
            Guard.WhenArgument(request, "request").IsNull().Throw();

            var usersModel = this.videoDataService
                .GetAllVideosWithDeleted()
                .Map<Video, VideoManageViewModel>()
                .ToDataSourceResult(request);

            return this.Json(usersModel);
        }

        [SaveChanges]
        public ActionResult UpdateVideo(VideoManageViewModel videoModel)
        {
            if (videoModel != null)
            {
                var video = this.videoDataService.GetVideoByServiceIdWithDeleted(videoModel.ServiceVideoId);
                video.Title = videoModel.Title;
                video.ServiceVideoId = videoModel.ServiceVideoId;
                video.Views = videoModel.Views;

                if (video.IsDeleted && !videoModel.IsDeleted)
                {
                    video.IsDeleted = videoModel.IsDeleted;
                }

                this.videoDataService.UpdateVideo(video);

                if (!video.IsDeleted && videoModel.IsDeleted)
                {
                    this.videoDataService.RemoveVideo(video);
                    videoModel.DeletedOn = video.DeletedOn;
                }
            }

            return this.Json(new[] { videoModel });
        }

        [SaveChanges]
        public ActionResult HardDeleteVideo(VideoManageViewModel videoModel)
        {
            if (videoModel != null)
            {
                var video = this.videoDataService.GetVideoByServiceIdWithDeleted(videoModel.ServiceVideoId);
                this.videoDataService.HardRemoveVideo(video);
            }

            return this.Json(new[] { videoModel });
        }
    }
}