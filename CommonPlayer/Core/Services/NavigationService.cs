using CommonPlayer.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CommonPlayer.Core.Services
{
    public class NavigationService : ObservableObject, INavigationService
    {
        #region Private fields
        private readonly Func<Type, BaseViewModel> _viewModelFactory;
        private BaseViewModel _currentViewModel = null;
        #endregion


        #region Public properties
        public BaseViewModel CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }
        #endregion


        #region Constructor
        public NavigationService(Func<Type, BaseViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }
        #endregion


        #region Public methods
        public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
        {
            BaseViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentViewModel = viewModel;
        }
        #endregion
    }
}
