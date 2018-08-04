using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktopcommands.Commands.CommandUtilities
{
    public static class Youtube
    {
        private static YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
        {
            ApiKey = Utilities.Utils.Getconfig<string>("APIKey"),
            ApplicationName = "Desktopcommands"
        });

        public static List<string[]> SearchVideos(string term, int maxresults)
        {
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = term; // Replace with your search term.
            searchListRequest.MaxResults = maxresults;
            searchListRequest.Type = "video";

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = searchListRequest.Execute();

            List<string[]> videos = new List<string[]>();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                videos.Add(new String[] { searchResult.Id.VideoId, searchResult.Snippet.Title });
            }
            return videos;
        }
        
        public static List<string[]> SearchPlaylists(string term, int maxresults)
        {
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = term; // Replace with your search term.
            searchListRequest.MaxResults = maxresults;
            searchListRequest.Type = "playlist";

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = searchListRequest.Execute();

            List<string[]> playlist = new List<string[]>();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                playlist.Add(new String[] { searchResult.Id.PlaylistId, searchResult.Snippet.Title });
            }
            return playlist;
        }

        public static string GetVideoUrlfromID(string id)
        {
            return @"https://www.youtube.com/watch?v=" + id;
        }

        public static string GetPlaylistUrlfromID(string id)
        {
            var searchListRequest = youtubeService.PlaylistItems.List("snippet,contentDetails");
            searchListRequest.PlaylistId = id;
            searchListRequest.MaxResults = 1;
            var response = searchListRequest.Execute();
            return @"https://www.youtube.com/watch?v=" + response.Items.First().ContentDetails.VideoId + "&list=" + id;
        }
    }
}
