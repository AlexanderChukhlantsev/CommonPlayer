using CommunityToolkit.Mvvm.ComponentModel;

namespace CommonPlayer.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        #region Private properties
        [ObservableProperty]
        private bool _isDarkThemeActive = false;
        #endregion

    }
}
