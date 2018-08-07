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
    public class Command_Help : Command
    {
        public Command_Help(string args) : base("Command_Help")
        {

        }

        public override void Execute()
        {
            ResponseBox.KeyDown(EventHandler);
            List<List<string>> commands = CommandDefinitionManager.GetAllCommands().Keys.ToList();
            List<string> commandstrings = new List<string>();
            commands.ForEach(calls =>
            {
                string callstring = "";
                calls.ForEach(call =>
                {
                    callstring += call + " ";
                });
                commandstrings.Add(callstring);
            });
            ResponseBox.SetItems(commandstrings);
        }
        

        public void EventHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainWindow.AppWindow.Inputfield.Text = ResponseBox.SelectedItem().Split(' ')[0];
                MainWindow.AppWindow.Inputfield.Focus();
                MainWindow.AppWindow.Inputfield.CaretIndex = MainWindow.AppWindow.Inputfield.Text.Length;
            }
        }
    }
}
