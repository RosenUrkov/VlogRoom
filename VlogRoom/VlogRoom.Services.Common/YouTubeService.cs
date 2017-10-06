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

namespace VlogRoom.Services.Common
{
    public class YouTubeService : IYouTubeService
    {
        private const string ApiKey = "AIzaSyCOpBHSZp8jqgImoRnY7ErzrsnMhibTGxU";
        private const string ApplicationName = "VlogRoom";
        private const string PlayListId = "PLuAZD7L_R_m1ToJtTFw9OKFopN7ZhJ30n";
        private const string ChannelId = "UCKjXXtiCQP2OXtFj-bfi61Q";

        private readonly Google.Apis.YouTube.v3.YouTubeService youTubeService;

        public YouTubeService()
        {
            this.youTubeService = new Google.Apis.YouTube.v3.YouTubeService(
                new BaseClientService.Initializer()
                {
                    ApiKey = ApiKey,
                    ApplicationName = ApplicationName
                });
        }

        public IEnumerable<VideoSnippetServiceModel> GetVideoSnippets(int maxResultsLength)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("part", "snippet");
            parameters.Add("maxResults", maxResultsLength.ToString());
            parameters.Add("playlistId", PlayListId);

            var playlistItemsListByPlaylistIdRequest = this.youTubeService.PlaylistItems.List(parameters["part"].ToString());
            if (parameters.ContainsKey("maxResults"))
            {
                playlistItemsListByPlaylistIdRequest.MaxResults = long.Parse(parameters["maxResults"].ToString());
            }

            if (parameters.ContainsKey("playlistId") && parameters["playlistId"] != "")
            {
                playlistItemsListByPlaylistIdRequest.PlaylistId = parameters["playlistId"].ToString();
            }

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
    }
}
