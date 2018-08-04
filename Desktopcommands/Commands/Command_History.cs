using Desktopcommands.ResponseFields;
using Desktopcommands.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktopcommands.Commands
{
    public class Command_History : Command
    {
        private bool clearhist = false;
        public Command_History(string args) : base("Command_History")
        {
            List<string> arguments = args.Split(' ').ToList<string>();
            if (arguments.Contains("clear"))
                clearhist = true;
        }

        public override void Execute()
        {
            if (clearhist)
                HistoryManager.ClearHistory();
            ResponseBox.KeyDown(EventHandler);
            ResponseBox.SetItems(HistoryManager.GetHistory());
        }

        public void EventHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainWindow.AppWindow.Inputfield.Text = ResponseBox.SelectedItem();
                MainWindow.AppWindow.Inputfield.Focus();
            }
        }
    }
}
