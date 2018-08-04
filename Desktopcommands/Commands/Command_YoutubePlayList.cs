using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Desktopcommands.Commands.CommandUtilities;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Desktopcommands.ResponseFields;

namespace Desktopcommands.Commands
{
    public class Command_YoutubePlayList : Command
    {
        string term;
        List<string[]> playlists;
        List<string> playlistnames = new List<string>();
        List<string> playlistids = new List<string>();
        public Command_YoutubePlayList(string args) : base("Command_YoutubePlayList")
        {
            term = args;
        }

        public override void Execute()
        {
            if (term.Length < 1) return;
            playlists = Youtube.SearchPlaylists(term, 10);
            foreach (string[] s in playlists)
            {
                playlistids.Add(s[0]);
                playlistnames.Add(s[1]);
            }
            ResponseBox.KeyDown(EventHandler);
            ResponseBox.SetItems(playlistnames);
        }

        public void EventHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string id = playlistids[ResponseBox.SelectedIndex()];
                string url = Youtube.GetPlaylistUrlfromID(id);
                try
                {
                    Process.Start("chrome.exe", url);
                }
                catch (Exception)
                {
                    Process.Start(url);
                }
                MainWindow.AppWindow.Done();
            }
        }
    }
}
