using Desktopcommands.ResponseFields;
using Desktopcommands.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Desktopcommands.Commands
{
    public class Command_Shutdown : Command
    {
        public Command_Shutdown(string args) : base("Command_Shutdown")
        {
        }

        public override void Execute()
        {
            var psi = new ProcessStartInfo("shutdown", "/s /t 0")
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            Process.Start(psi);
        }
    }
}
