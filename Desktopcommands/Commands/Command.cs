using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Desktopcommands.Utilities;

namespace Desktopcommands.Commands
{
    public class Command
    {
        public String name;

        public Command(String cname)
        {
            name = cname;
        }

        public String GetProperty(String property)
        {
            return CommandDefinitionManager.GetProperty(name, property);
        }

        public virtual void Execute()
        {
            
        }
    }
}
