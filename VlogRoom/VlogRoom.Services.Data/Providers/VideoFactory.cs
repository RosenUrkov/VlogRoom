using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;
using VlogRoom.Services.Data.Contracts;

namespace VlogRoom.Services.Data.Providers
{
    public class VideoFactory : IVideoFactory
    {
        public PlaylistItem CreatePlaylistItem(PlaylistItemSnippet snippet)
        {
            return new PlaylistItem() { Snippet = snippet };
        }

        public PlaylistItemSnippet CreatePlaylistItemSnippet(string playListId, int position, ResourceId resourceId)
        {
            return new PlaylistItemSnippet() { PlaylistId = playListId, Position = position, ResourceId = resourceId };
        }

        public ResourceId CreateResourceId(string kind, string videoId)
        {
            return new ResourceId() { Kind = kind, VideoId = videoId };
        }

        public Video CreateVideo(VideoSnippet snippet, VideoStatus status)
        {
            return new Video() { Snippet = snippet, Status = status };
        }

        public VideoSnippet CreateVideoSnippet(string title, string description, string categoryId)
        {
            return new VideoSnippet() { Title = title, Description = description, CategoryId = categoryId };
        }

        public VideoStatus CreateVideoStatus(string privacyStatus)
        {
            return new VideoStatus() { PrivacyStatus = privacyStatus };
        }
    }
}
