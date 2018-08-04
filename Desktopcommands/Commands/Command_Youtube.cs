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
        List<string[]> videos;
        List<string> videonames = new List<string>();
        List<string> videoids = new List<string>();
        public Command_Youtube(string args) : base("Command_Youtube")
        {
            term = args;
        }

        public override void Execute()
        {
            if (term.Length < 1) return;
            videos = Youtube.SearchVideos(term, 10);
            foreach(string[] s in videos){
                videoids.Add(s[0]);
                videonames.Add(s[1]);
            }
            ResponseBox.KeyDown(EventHandler);
            ResponseBox.SetItems(videonames);
        }

        public void EventHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                string url = Youtube.GetVideoUrlfromID( videoids[ResponseBox.SelectedIndex()]);
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
