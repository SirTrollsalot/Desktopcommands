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
    public class Command_Exit : Command
    {
        public Command_Exit(string args) : base("Command_Exit")
        {
        }

        public override void Execute()
        {
            Application.Current.Shutdown();
        }
    }
}
