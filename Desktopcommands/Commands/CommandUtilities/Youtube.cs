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
            ApiKey = "AIzaSyAdceopixMkCDwPi2qSQCyUfRpw2HW17gI",
            ApplicationName = "Desktopcommands"
        });

        public static Dictionary<string,string> SearchVideos(string term, int maxresults)
        {
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = term; // Replace with your search term.
            searchListRequest.MaxResults = maxresults;
            searchListRequest.Type = "video";

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = searchListRequest.Execute();

            Dictionary<string, string> videos = new Dictionary<string, string>();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                videos.Add(searchResult.Snippet.Title, searchResult.Id.VideoId);
            }
            return videos;
        }
        
        public static Dictionary<string,string> SearchPlaylists(string term, int maxresults)
        {
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = term; // Replace with your search term.
            searchListRequest.MaxResults = maxresults;
            searchListRequest.Type = "playlist";

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = searchListRequest.Execute();

            Dictionary<string, string> playlist = new Dictionary<string, string>();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                playlist.Add(searchResult.Snippet.Title, searchResult.Id.PlaylistId);
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
