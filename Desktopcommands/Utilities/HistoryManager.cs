using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktopcommands.Utilities
{
    public static class HistoryManager
    {
        private static HashSet<String> history = new HashSet<String>();

        public static HashSet<String> GetHistory()
        {
            return history;
        }

        public static void AddToHistory(String entry)
        {
            history.Add(entry);
        }

        public static void LoadHistory()
        {
            var lines = File.ReadLines(@"History.txt");
            foreach (String line in lines)
                history.Add(line);
        }

        public static void SaveHistory()
        {
            File.WriteAllLines(@"History.txt", history);
        }
        
        public static void ClearHistory()
        {
            File.Delete(@"History.txt");
            history.Clear();
        }

    }
}
