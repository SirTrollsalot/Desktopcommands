using Desktopcommands.Commands.CommandUtilities;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace Desktopcommands.Commands
{
    public class Command_Run : Command
    {
        string term;
  
        public Command_Run(string arguments) : base("Command_Run")
        {
            term = arguments;
        }

        public override void Execute()
        {
            ResponseFields.ResponseBox.SetItems(RegistryUtils.GetUninstallDisplayNamesFiltered(term));
            ResponseFields.ResponseBox.KeyDown(SelectInstallationHandler);
        }

        public void SelectInstallationHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ResponseFields.ResponseBox.SetItems(RegistryUtils.GetExecutablePathsFromDisplayUninstallDisplayName(ResponseFields.ResponseBox.SelectedItem()));
                ResponseFields.ResponseBox.KeyDown(RunExecutableHandler);
            }
            if (e.Key == Key.Escape)
            {
                MainWindow.AppWindow.Done();
            }
        }

        public void RunExecutableHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Process.Start((string)ResponseFields.ResponseBox.SelectedItem());
                MainWindow.AppWindow.Done();
            }
            if (e.Key == Key.Escape)
            {
                Execute(); //https://www.youtube.com/watch?v=uACvFAm6JSM
            }
        }
    }
}
