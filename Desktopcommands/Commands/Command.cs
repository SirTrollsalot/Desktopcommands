using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Desktopcommands.Utilities;
using Desktopcommands.ResponseFields;

namespace Desktopcommands.Commands
{
    public abstract class Command
    {
        public string Name { get; private set; }
        public Command(string cname) {
            Name = cname;
        }

        public string GetProperty(string property)
        {
            return CommandDefinitionManager.GetProperty(Name, property);
        }

        public abstract void Execute();
    }
}
