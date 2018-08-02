using Desktopcommands.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktopcommands.Commands
{
    public class Command_History : Command
    {
        private bool clearhist = false;
        public Command_History(String args) : base("History")
        {
            List<String> arguments = args.Split(' ').ToList<String>();
            if (arguments.Contains("clear"))
                clearhist = true;
        }

        public override void Execute()
        {
            base.Execute();
            if (clearhist)
                HistoryManager.ClearHistory();
            MainWindow.AppWindow.Dispatcher.Invoke(() =>
            {
                MainWindow.AppWindow.ResponseListboxItems = new ObservableCollection<String>(HistoryManager.GetHistory());
            });
            
            //MainWindow.AppWindow.Done();
        }
    }
}
