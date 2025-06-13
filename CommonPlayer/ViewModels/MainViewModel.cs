using CommonPlayer.Core.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CommonPlayer.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        #region Private fields
        [ObservableProperty]
        private INavigationService _navigation;
        #endregion


        #region Constructor
        public MainViewModel(INavigationService navigationService)
        {
            Navigation = navigationService;

            NavigateToHomeCommand();
        }
        #endregion


        #region Private methods
        [RelayCommand]
        public void NavigateToHomeCommand()
        {
            Navigation.NavigateTo<PlayerViewModel>();
        }

        [RelayCommand]
        public void NavigateToSettingsCommand()
        {
            Navigation.NavigateTo<SettingsViewModel>();
        }
        #endregion
    }
}
