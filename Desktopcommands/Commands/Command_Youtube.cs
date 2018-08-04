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
    public class Command_Youtube : Command
    {
        string term;
        Dictionary<string, string> videos;
        public Command_Youtube(string args) : base("Command_Youtube")
        {
            term = args;
        }

        public override void Execute()
        {
            if (term.Length < 1) return;
            videos = Youtube.SearchVideos(term, 10);
            ResponseBox.KeyDown(EventHandler);
            ResponseBox.SetItems(videos.Keys.ToList());
        }

        public void EventHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string url = Youtube.GetVideoUrlfromID(videos[ResponseBox.SelectedItem()]);
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
