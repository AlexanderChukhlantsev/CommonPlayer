using System.Collections.ObjectModel;

using CommonPlayer.Models;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CommonPlayer.ViewModels
{
    public partial class PlayerViewModel : BaseViewModel
    {
        #region Private fields
        [ObservableProperty]
        private ObservableCollection<TrackModel> _tracks = [];
        #endregion


        #region Constructor
        public PlayerViewModel()
        {
        }
        #endregion
    }
}
