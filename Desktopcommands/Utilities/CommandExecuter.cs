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
        private Dictionary<List<string>,string> commands = CommandDefinitionManager.GetAllCommands();

        public Command Execute(string input)
        {
            string commandcall = input.Split(' ')[0];
            string args = input.Remove(0,commandcall.Length).Trim();
            List<string> Calls = commands.Keys.Where(li => li.Contains(commandcall)).FirstOrDefault();
            if (Calls == null || !commands.TryGetValue(Calls, out string commandname))
            {
                return null;
            }
            Command command = GetCommand(commandname, args);
            command.Execute();
            return command;
        }

        private Command GetCommand(string commandname, string args)
        {
            string typename = typeof(Command).AssemblyQualifiedName.Replace("Desktopcommands.Commands.Command", "Desktopcommands.Commands."+commandname);
            return (Command)Activator.CreateInstance(Type.GetType(typename),args);
        }
        
    }
}
