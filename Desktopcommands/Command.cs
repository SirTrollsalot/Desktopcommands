using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktopcommands
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
    }
}
