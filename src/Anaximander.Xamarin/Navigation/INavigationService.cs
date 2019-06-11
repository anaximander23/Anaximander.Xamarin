using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Anaximander.Xamarin.Navigation
{
    public interface INavigationService
    {
        void SetMainPage<T>() where T : Page;

        void SetMainPage(Type pageType);

        Task NavigateToPageAsync<T>() where T : Page;

        Task NavigateBackAsync();

        Task NavigateBackToPageAsync<T>() where T : Page;

        Task NavigateToMainPageAsync();

        Task ShowModalAsync<T>() where T : Page;

        Task RemoveModalAsync();
    }
}