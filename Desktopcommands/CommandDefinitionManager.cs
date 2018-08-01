using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Desktopcommands
{
    static class CommandDefinitionManager
    {
        private static XElement commands = XDocument.Parse(Properties.Resources.CommandDefinitions).Root;

        
        public static List<String> GetAllCommands()
        {
            List<String> ret = new List<String>();
            foreach(var com in commands.Elements())
            {
                ret.Add(com.Attribute("name").Value);
            }
            return ret;
        }

        public static String GetProperty(String command, String property)
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
