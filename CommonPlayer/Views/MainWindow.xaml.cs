using System.Windows.Input;

using CommonPlayer.ViewModels;

namespace CommonPlayer;


public partial class MainWindow
{
    public MainWindow(MainViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();

        CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, OnClose));
    }

    private void OnClose(object sender, ExecutedRoutedEventArgs e)
    {
        Close();
    }
}
