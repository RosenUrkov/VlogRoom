using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VlogRoom.Services.Data.Contracts
{
    public interface IVideoFactory
    {
        Video CreateVideo(VideoSnippet snippet, VideoStatus status);

        VideoSnippet CreateVideoSnippet(string title, string description, string categoryId);

        VideoStatus CreateVideoStatus(string privacyStatus);

        ResourceId CreateResourceId(string kind, string videoId);

        PlaylistItem CreatePlaylistItem(PlaylistItemSnippet snippet);

        PlaylistItemSnippet CreatePlaylistItemSnippet(string playListId, int position, ResourceId resourceId);
    }
}
