using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.ComponentModel;

namespace Desktopcommands
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 9000;
        

        //Modifiers:
        private uint MOD_CONTROL = Getconfig<uint>("ShortcutModifier"); //CTRL
        //Space:
        private uint VK_SPACE = Getconfig<uint>("ShortcutKey");

        public MainWindow()
        {
            InitializeComponent();
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
                        this.Visibility = Visibility.Hidden;
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

        protected override void OnClosed(EventArgs e)
        {
            _source.RemoveHook(HwndHook);
            UnregisterHotKey(_windowHandle, HOTKEY_ID);
            base.OnClosed(e);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            base.OnLostFocus(e);
        }

        public static T Getconfig<T>(string key)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if(converter != null)
                {
                    return (T)converter.ConvertFromString(ConfigurationManager.AppSettings.Get(key));
                }
                return default(T);
            }
            catch (NotSupportedException)
            {
                return default(T);
            }
        }

        private void HandleEnter(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                String input = Inputfield.Text;
                //TODO - EXECUTE COMMAND
            }
        }
    }
}
