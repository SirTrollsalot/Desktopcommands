using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktopcommands.Commands.CommandUtilities
{
    public static class RegistryUtils
    {
        private const string REGISTRY_UNINSTALL_KEY = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
        private static Dictionary<string, string> installlocationscache = new Dictionary<string, string>();
        private static List<string> displaynamescache = new List<string>();

        private static void FillInternalBuffers()
        {
            using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(REGISTRY_UNINSTALL_KEY))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (Microsoft.Win32.RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        string str = (string)subkey.GetValue("DisplayName");
                        if (str != null && str != "")
                        {
                            if (!installlocationscache.ContainsKey(str))
                            {
                                installlocationscache.Add(str, (string)subkey.GetValue("InstallLocation"));
                            }
                            displaynamescache.Add(str);
                        }
                    }
                }
            }
        }

        public static List<string> GetUninstallDisplayNamesFiltered(string filter)
        {
            if(displaynamescache.Count == 0 || installlocationscache.Count == 0)
            {
                FillInternalBuffers();
            }
            List<string> results = new List<string>();
            string lowercasefilter = filter.ToLower();
            foreach(var str in displaynamescache)
            {
                if (str.ToLower().Contains(lowercasefilter))
                {
                    results.Add(str);
                }
            }
            return results;
        }

        public static List<string> GetExecutablePathsFromDisplayUninstallDisplayName(string displayname)
        {
            List<string> results = new List<string>();
            if (installlocationscache.ContainsKey(displayname))
            {
                string installpath = installlocationscache[displayname];
                if (Directory.Exists(installpath))
                {
                    results.AddRange(Directory.GetFiles(installlocationscache[displayname], "*.exe", SearchOption.AllDirectories));
                }
            }
            return results;
        }
    }
}
