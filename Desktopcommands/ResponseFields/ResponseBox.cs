using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Desktopcommands.ResponseFields
{
    public static class ResponseBox
    {
        private static KeyEventHandler currentHandler;

        public static void KeyDown(KeyEventHandler eve)
        {
            if (currentHandler != null)
                MainWindow.AppWindow.ResponseListBox.KeyDown -= currentHandler;
            currentHandler = eve;
            MainWindow.AppWindow.ResponseListBox.KeyDown += eve;
        }

        public static void SetItems(IEnumerable<String> items)
        {
            MainWindow.AppWindow.Dispatcher.Invoke(() =>
            {
                MainWindow.AppWindow.ResponseListBoxItems = new ObservableCollection<String>(items);
                MainWindow.AppWindow.ResponseListBox.Focus();
                if (MainWindow.AppWindow.ResponseListBox.HasItems)
                    MainWindow.AppWindow.ResponseListBox.SelectedIndex = 0;
            });
        }
        public static List<String> Items()
        {
            return MainWindow.AppWindow.ResponseListBoxItems.ToList<String>();
        }

        public static int SelectedIndex()
        {
            return MainWindow.AppWindow.ResponseListBox.SelectedIndex;
        }
        public static String SelectedItem()
        {
            return MainWindow.AppWindow.ResponseListBox.SelectedItem.ToString();
        }
    }
}
