using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using VlogRoom.Services.Common.Contracts;
using System.IO;
using Google.Apis.Upload;
using System.Threading;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using System.Xml;
using Bytes2you.Validation;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Common.Constants;

namespace VlogRoom.Services.Common
{
    public class YouTubeService : IYouTubeService
    {  
        private Google.Apis.YouTube.v3.YouTubeService youTubeService;
        private readonly IVideoFactory videoFactory;

        public YouTubeService(IVideoFactory videoFactory)
        {
            this.videoFactory = videoFactory;
        }

        public async Task<VlogRoom.Data.Models.Video> UploadVideo(Stream videoStream, string videoTitle, string videoDescription)
        {
            Guard.WhenArgument(videoStream, "videoStream").IsNull().Throw();

            await this.Authorize();

            var snippet = this.videoFactory.CreateVideoSnippet(videoTitle, videoDescription, GlobalConstants.VideoCategoryId);
            var status = this.videoFactory.CreateVideoStatus(GlobalConstants.VideoPrivacyStatus);
            var video = this.videoFactory.CreateVideo(snippet, status);

            VideosResource.InsertMediaUpload videoInsertRequest;
            using (videoStream)
            {
                videoInsertRequest = this.youTubeService.Videos.Insert(video, "snippet,status", videoStream, "video/*");
                await videoInsertRequest.UploadAsync();
            }

            var videoServiceId = this.AddVideoToThePlaylist(videoInsertRequest.ResponseBody.Id);
            var videoModel = new VlogRoom.Data.Models.Video()
            {
                ServiceVideoId = videoInsertRequest.ResponseBody.Id,
                ServiceListItemId = videoServiceId,
                ImageUrl = videoInsertRequest.ResponseBody.Snippet.Thumbnails.High.Url,
                Description = videoInsertRequest.ResponseBody.Snippet.Description,
                Title = videoInsertRequest.ResponseBody.Snippet.Title,
                Views = 0,
            };

            return videoModel;
        }

        public async Task DeleteVideo(VlogRoom.Data.Models.Video video)
        {
            await this.Authorize();

            var playlistItemsDeleteRequest = this.youTubeService.PlaylistItems.Delete(video.ServiceListItemId);
            playlistItemsDeleteRequest.Execute();

            this.DeleteVideoFromService(video.ServiceVideoId);
        }

        private async Task Authorize()
        {
            UserCredential credential;
            using (var stream = new FileStream(
                AppDomain.CurrentDomain.BaseDirectory + "/App_Data/client_secret.json",
                FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { Google.Apis.YouTube.v3.YouTubeService.Scope.Youtube },
                    "user",
                    CancellationToken.None
                );
            }

            this.youTubeService = new Google.Apis.YouTube.v3.YouTubeService(
                new BaseClientService.Initializer()
                {
                    ApiKey = GlobalConstants.ProviderApiKey,
                    ApplicationName = GlobalConstants.ProviderApplicationName,
                    HttpClientInitializer = credential,
                });
        }

        private string AddVideoToThePlaylist(string videoId)
        {
            var resourceId = this.videoFactory.CreateResourceId("youtube#video", videoId);
            var snippet = this.videoFactory.CreatePlaylistItemSnippet(GlobalConstants.ProviderPlayListId, 1, resourceId);
            var playlistItem = this.videoFactory.CreatePlaylistItem(snippet);

            var playlistItemsInsertRequest = this.youTubeService.PlaylistItems.Insert(playlistItem, "snippet");
            var response = playlistItemsInsertRequest.Execute();

            return response.Id;
        }

        private void DeleteVideoFromService(string videoId)
        {
            var videosDeleteRequest = this.youTubeService.Videos.Delete(videoId);
            videosDeleteRequest.Execute();
        }
    }
}

