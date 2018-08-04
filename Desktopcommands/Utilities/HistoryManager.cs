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
        private static List<string> history = new List<string>();

        public static List<string> GetHistory()
        {
            return history;
        }

        public static void AddToHistory(string entry)
        {
            history.Insert(0,entry);
            history = history.Distinct().ToList();
        }

        public static void LoadHistory()
        {
            try
            {
                var lines = File.ReadLines(@"History.txt");
                foreach (string line in lines)
                    history.Add(line);
            }catch(FileNotFoundException){}
            
        }

        public static void SaveHistory()
        {
            File.WriteAllLines(@"History.txt", history);
        }
        
        public static void ClearHistory()
        {
            try
            {
                File.Delete(@"History.txt");
            }
            catch (Exception) { }
            history.Clear();
        }

    }
}
