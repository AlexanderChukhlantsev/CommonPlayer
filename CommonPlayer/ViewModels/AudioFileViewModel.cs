using System.Drawing;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CommonPlayer.ViewModels
{
    public class AudioFileViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Image _cover;

        public AudioFileViewModel()
        {

        }
    }
}
