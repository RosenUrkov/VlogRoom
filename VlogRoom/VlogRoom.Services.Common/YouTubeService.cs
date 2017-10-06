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

namespace VlogRoom.Services.Common
{
    public class YouTubeService : IYouTubeService
    {
        private const string ApiKey = "AIzaSyCOpBHSZp8jqgImoRnY7ErzrsnMhibTGxU";
        private const string ApplicationName = "VlogRoom";
        private const string PlayListId = "PLuAZD7L_R_m20wOxJPjRRgjMAJSbXIoeL";
        private const string ChannelId = "UCKjXXtiCQP2OXtFj-bfi61Q";
        
        private Google.Apis.YouTube.v3.YouTubeService youTubeService = new Google.Apis.YouTube.v3.YouTubeService();

        public YouTubeService()
        {
            lock (this.youTubeService)
            {
                this.Authorize();
            }
        }

        private async void Authorize()
        {
            UserCredential credential;
            using (var stream = new FileStream(
                AppDomain.CurrentDomain.BaseDirectory + "/App_Data/client_secrets.json",
                FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { Google.Apis.YouTube.v3.YouTubeService.Scope.YoutubeUpload },
                    "rosen.urkov@gmail.com",
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

        public void UploadVideo(Stream videoStream)
        {
            var snippet = new VideoSnippet();
            snippet.Title = "Video upload title";
            snippet.Description = "Description of uploaded video.";
            snippet.CategoryId = "22";

            var status = new VideoStatus();
            status.PrivacyStatus = "private";

            var video = new Video();
            video.Snippet = snippet;
            video.Status = status;

            using (videoStream)
            {
                var videosInsertRequest = this.youTubeService.Videos.Insert(video, "snippet,status", videoStream, "video/*");
                //videosInsertRequest.ProgressChanged += ProgressChanged;
                //videosInsertRequest.ResponseReceived += ResponseReceived;

                videosInsertRequest.Upload();
            }
        }

        private void ProgressChanged(IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    Console.WriteLine("{0} bytes sent.", progress.BytesSent);
                    break;

                case UploadStatus.Failed:
                    Console.WriteLine("An error prevented the upload from completing.\n{0}", progress.Exception);
                    break;
            }
        }

        private void ResponseReceived(Video video)
        {
            Console.WriteLine("Video id '{0}' was successfully uploaded.", video.Id);
        }
    }
}

