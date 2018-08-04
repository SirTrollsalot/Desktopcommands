using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using Desktopcommands.Commands;
using Desktopcommands.Utilities;

namespace Desktopcommands
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow AppWindow;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private Command CurrentCommand;
        private const int HOTKEY_ID = 9000;
        

        //Modifiers:
        private uint MOD_CONTROL = Utils.Getconfig<uint>("ShortcutModifier"); //CTRL
        //Space:
        private uint VK_SPACE = Utils.Getconfig<uint>("ShortcutKey");
        private CommandExecuter CommExec = new CommandExecuter();
        public ObservableCollection<string> _responseListboxItems = new ObservableCollection<string>();
        public ObservableCollection<string> ResponseListBoxItems
        {
            get
            {
                return _responseListboxItems;
            }
            set
            {
                _responseListboxItems.Clear();
                foreach(string s in value)
                {
                    _responseListboxItems.Add(s);
                }
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            AppWindow = this;
            HistoryManager.LoadHistory();
        }

        public void Done()
        {
            Dispatcher.Invoke(()=> {
                this.Visibility = Visibility.Hidden;
                Inputfield.Clear();
                Inputfield.Focus();
                ResponseListBoxItems.Clear();
            });
        }
        
        private void Window_Closing(object sender, EventArgs e)
        {
            HistoryManager.SaveHistory();
            _source.RemoveHook(HwndHook);
            UnregisterHotKey(_windowHandle, HOTKEY_ID);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            Done();
            base.OnLostFocus(e);
        }
        
        private void HandleEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                
                string input = Inputfield.Text.Trim();
                CurrentCommand = CommExec.Execute(input);
                if (CurrentCommand == null)
                {
                    Done();
                }
                HistoryManager.AddToHistory(input);
            }
        }
        
        //For opening Window when Hotkey pressed (CTRL + SPACEBAR)
        private IntPtr _windowHandle;
        private HwndSource _source;

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            _windowHandle = new WindowInteropHelper(this).Handle;
            _source = HwndSource.FromHwnd(_windowHandle);
            _source.AddHook(HwndHook);
            RegisterHotKey(_windowHandle, HOTKEY_ID, MOD_CONTROL, VK_SPACE); //CTRL + Space
            this.Visibility = Visibility.Hidden;
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            if(msg == WM_HOTKEY && wParam.ToInt32() == HOTKEY_ID)
            {
                int vkey = (((int)lParam >> 16) & 0xFFFF);
                if (vkey == VK_SPACE)
                {
                    if(this.IsVisible)
                    {
                        Done();
                    }
                    else
                    {
                        this.Visibility = Visibility.Visible;
                        Inputfield.Focus();
                    }
                }
                handled = true;
            }
            return IntPtr.Zero;
        }
        
    }
}
