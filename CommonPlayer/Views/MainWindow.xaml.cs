using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

using CommonPlayer.Core.Window;
using CommonPlayer.ViewModels;

namespace CommonPlayer;


public partial class MainWindow
{
    #region Private fields
    private bool _isMouseDown;
    private Point _mouseDownPoint;
    #endregion
    

    #region Constructor
    public MainWindow(MainViewModel viewModel)
    {
        MaxHeight = App.MaxHeight;
        MaxWidth = App.MaxWidth;

        DataContext = viewModel;

        InitializeComponent();

        CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, OnClose));
    }
    #endregion


    #region Close event
    private void OnClose(object sender, ExecutedRoutedEventArgs e)
    {
        Close();
    }
    #endregion


    #region Top-Bar menu events
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        //if (Message.Show(
        //    "Выход",
        //    "Вы уверены, что хотите закрыть приложение?",
        //    Buttons.YesNo,
        //    Icons.QuestionMarkCircleOutline,
        //    WindowsManager.GetWindowByName("CreatingWindow") ?? WindowsManager.MainWindow) == MessageBoxResult.Yes)
        //{
            Application.Current.Shutdown();
        //}
    }

    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal;
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        if (WindowState != WindowState.Minimized)
            WindowState = WindowState.Minimized;
    }
    #endregion


    #region Drag events
    private void Window_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isMouseDown && e.LeftButton == MouseButtonState.Pressed)
        {
            Point currentPosition = e.GetPosition(this);
            var mousePosition = PointToScreen(e.GetPosition(this));
            var monitorInfo = Screen.GetMonitor(mousePosition);
            var restoredBound = RestoreBounds;

            if (Math.Abs(currentPosition.X - _mouseDownPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(currentPosition.Y - _mouseDownPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;

                    double newLeft = mousePosition.X - (restoredBound.Width / 2);
                    double newTop = mousePosition.Y - (restoredBound.Height / 2);

                    if (monitorInfo != null)
                    {
                        newLeft = Math.Max(monitorInfo.Value.rcWork.Left, Math.Min(newLeft, monitorInfo.Value.rcWork.Right - restoredBound.Width));
                        newTop = Math.Max(monitorInfo.Value.rcWork.Top, Math.Min(newTop, monitorInfo.Value.rcWork.Bottom - restoredBound.Height)) - 10;
                    }
                    else
                    {
                        newLeft = mousePosition.X - Width / 2;
                        newTop = mousePosition.Y - 10;
                    }

                    Left = newLeft;
                    Top = newTop;
                }

                try
                {
                    DragMove();
                }
                catch (InvalidOperationException)
                {
                    //на всякий случай если DragMove вызван не в том состоянии
                }

                _isMouseDown = false;
            }
        }
    }

    private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        _isMouseDown = false;
    }

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _mouseDownPoint = e.GetPosition(this);
        _isMouseDown = true;
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount >= 2)
        {
            WindowState = WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal;
        }
        else if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }
    #endregion


    #region Resize events
    private void ResizeIcon_MouseDown(object sender, MouseButtonEventArgs e)
    {
        ReleaseCapture();
        SendMessage(new WindowInteropHelper(this).Handle, 0xA1, (IntPtr)0x2, (IntPtr)0);
    }

    private void ResizeIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (PresentationSource.FromVisual((Visual)sender) is HwndSource hwndSource)
        {
            SendMessage(hwndSource.Handle, 0x112, (IntPtr)61448, IntPtr.Zero);
        }
    }

    #region dll import
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [DllImportAttribute("user32.dll")]
    private static extern bool ReleaseCapture();
    #endregion
    #endregion
}
