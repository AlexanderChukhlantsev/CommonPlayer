using System.Windows;

namespace CommonPlayer.Core.Window
{
    public static class WindowsManager
    {
        public static System.Windows.Window MainWindow => Application.Current.Dispatcher.Invoke(() =>
        {
            return Application.Current.MainWindow;
        });

        public static System.Windows.Window? GetWindowByName(string windowName)
        {
            return Application.Current.Dispatcher.Invoke(() =>
            {
                return Application.Current.Windows.OfType<System.Windows.Window>()
                .FirstOrDefault(window => window.Name.Equals(windowName, StringComparison.OrdinalIgnoreCase));
            });
        }
    }
}