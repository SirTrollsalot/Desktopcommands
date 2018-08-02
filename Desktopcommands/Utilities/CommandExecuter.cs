using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Desktopcommands.Commands;

namespace Desktopcommands.Utilities
{
    public class CommandExecuter
    {
        private Dictionary<List<String>,String> commands = CommandDefinitionManager.GetAllCommands();

        public bool ExecuteAsync(String input)
        {
            String commandcall = input.Split(' ')[0];
            String args = input.Remove(0,commandcall.Length);
            String commandname;
            List<String> Calls = commands.Keys.Where(li => li.Contains(commandcall)).FirstOrDefault();
            if (Calls == null || !commands.TryGetValue(Calls, out commandname))
            {
                return false;
            }
            Command command = GetCommand(commandname, args);
            Task.Run(() => command.Execute());
            return true;
        }

        private Command GetCommand(String commandname, String args)
        {
            String typename = typeof(Command).AssemblyQualifiedName.Replace("Desktopcommands.Commands.Command", "Desktopcommands.Commands."+commandname);
            return (Command)Activator.CreateInstance(Type.GetType(typename),args);
        }
        
    }
}
