using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CommonPlayer.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(IncrementCountCommand))]
        private int _count;
    
        public MainViewModel()
        {

        }

        [RelayCommand(CanExecute = nameof(CanIncrementCount))]
        private void IncrementCount()
        {
            Count++;
        }

        private bool CanIncrementCount() => Count < 5;

        [RelayCommand]
        private void ClearCount()
        {
            Count = 0;
        }
    }
}
