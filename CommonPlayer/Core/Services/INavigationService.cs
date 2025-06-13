using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonPlayer.ViewModels;

namespace CommonPlayer.Core.Services
{
    public interface INavigationService
    {
        BaseViewModel CurrentViewModel { get; }

        void NavigateTo<T>() where T : BaseViewModel;
    }
}
