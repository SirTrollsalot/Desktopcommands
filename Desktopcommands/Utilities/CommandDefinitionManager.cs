using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Desktopcommands.Utilities
{
    static class CommandDefinitionManager
    {
        private static XElement commands = XDocument.Parse(Properties.Resources.CommandDefinitions).Root;

        
        public static Dictionary<List<string>, string> GetAllCommands()
        {
            //List<string> are the calls and string is the name
            Dictionary<List<string>, string> ret = new Dictionary<List<string>, string>();
            foreach(var com in commands.Elements())
            {
                List<string> calls = new List<string>();
                calls.Add(com.Attribute("call").Value);
                if (com.Attribute("shortcall") != null) calls.Add(com.Attribute("shortcall").Value);
                ret.Add(calls, com.Attribute("name").Value);
            }
            return ret;
        }

        public static string GetProperty(string command, string property)
        {
            XElement currentcomm = commands.Elements().Where(c => c.Attribute("name").Value == command).First();
            if (currentcomm.Elements().Any())
            {
                return currentcomm.Elements().Where(p => p.Attribute("key").Value == property).First().Attribute("value").Value;
            }
            return null;
        }
        
    }
}
