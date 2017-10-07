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
using VlogRoom.Services.Models;
using VlogRoom.Services.Common.Contracts;
using System.IO;
using Google.Apis.Upload;
using System.Threading;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;

namespace VlogRoom.Services.Common
{
    public class YouTubeService : IYouTubeService
    {
        private const string ApplicationName = "VlogRoom";
        private const string ApiKey = "AIzaSyCOpBHSZp8jqgImoRnY7ErzrsnMhibTGxU";
        private const string PlayListId = "PLuAZD7L_R_m20wOxJPjRRgjMAJSbXIoeL";

        private Google.Apis.YouTube.v3.YouTubeService youTubeService;

        public YouTubeService()
        {
            this.youTubeService = new Google.Apis.YouTube.v3.YouTubeService(
                new BaseClientService.Initializer()
                {
                    ApiKey = ApiKey,
                    ApplicationName = ApplicationName,
                });
        }

        public IEnumerable<VideoSnippetServiceModel> GetVideoSnippets(int maxResultsLength)
        {
            var playlistItemsListByPlaylistIdRequest = this.youTubeService.PlaylistItems.List("snippet");
            playlistItemsListByPlaylistIdRequest.MaxResults = maxResultsLength;
            playlistItemsListByPlaylistIdRequest.PlaylistId = PlayListId;

            var response = playlistItemsListByPlaylistIdRequest.Execute();
            return response
                .Items
                .Select(x => new VideoSnippetServiceModel()
                {
                    Id = x.Snippet.ResourceId.VideoId,
                    Description = x.Snippet.Description,
                    ImageUrl = x.Snippet.Thumbnails.Default__.Url,
                    Title = x.Snippet.Title
                });
        }

        public async Task<string> UploadVideoIntoThePlaylist(Stream videoStream)
        {
            await this.Authorize();

            var snippet = new VideoSnippet();
            snippet.Title = "Video upload title";
            snippet.Description = "Description of uploaded video.";
            snippet.CategoryId = "22";

            var status = new VideoStatus();
            status.PrivacyStatus = "private";

            var video = new Video();
            video.Snippet = snippet;
            video.Status = status;

            VideosResource.InsertMediaUpload videoInsertRequest;
            using (videoStream)
            {
                videoInsertRequest = this.youTubeService.Videos.Insert(video, "snippet,status", videoStream, "video/*");
                await videoInsertRequest.UploadAsync();
            }

            this.AddVideoToThePlaylist(videoInsertRequest.ResponseBody.Id);
            return videoInsertRequest.ResponseBody.Id;
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
                    ApiKey = ApiKey,
                    ApplicationName = ApplicationName,
                    HttpClientInitializer = credential,
                });
        }

        private void AddVideoToThePlaylist(string videoId)
        {
            ResourceId resourceId = new ResourceId();
            resourceId.Kind = "youtube#video";
            resourceId.VideoId = videoId;

            var snippet = new PlaylistItemSnippet();
            snippet.PlaylistId = PlayListId;
            snippet.Position = 1;
            snippet.ResourceId = resourceId;

            var playlistItem = new PlaylistItem();
            playlistItem.Snippet = snippet;

            var playlistItemsInsertRequest = this.youTubeService.PlaylistItems.Insert(playlistItem, "snippet");
            playlistItemsInsertRequest.Execute();
        }
    }
}

