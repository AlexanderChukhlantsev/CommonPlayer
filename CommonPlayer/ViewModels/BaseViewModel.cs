using CommunityToolkit.Mvvm.ComponentModel;

using MaterialDesignThemes.Wpf;

namespace CommonPlayer.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        #region Private properties
        [ObservableProperty]
        private bool _isDarkThemeActive = false;
        #endregion


        #region Partial method
        partial void OnIsDarkThemeActiveChanged(bool value)
        {
            ApplyTheme(value);
        }
        #endregion


        #region Private methods
        private static void ApplyTheme(bool isDarkThemeActive)
        {
            PaletteHelper paletteHelper = new();

            Theme theme = paletteHelper.GetTheme();

            if (isDarkThemeActive)
                theme.SetDarkTheme();
            else
                theme.SetLightTheme();

            paletteHelper.SetTheme(theme);
        }
        #endregion
    }
}
