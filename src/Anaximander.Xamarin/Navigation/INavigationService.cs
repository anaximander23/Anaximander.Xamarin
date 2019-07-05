using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Anaximander.Xamarin.Navigation
{
    public interface INavigationService
    {
        Page CurrentPage { get; }

        Task SetMainPageAsync<T>() where T : Page;

        Task SetMainPageAsync<T>(MasterDetailNavigationBehaviour navigationBehaviour) where T : MasterDetailPage;

        Task SetMainPageAsync(Type pageType);

        Task NavigateToMainPageAsync();

        Task NavigateToPageAsync<T>() where T : Page;

        Task NavigateToPageAsync<T, TData>(TData data) where T : Page;

        Task NavigateBackAsync();

        Task NavigateBackToPageAsync<T>() where T : Page;

        Task<bool> TryNavigateBackToPageAsync<T>() where T : Page;

        void ClearNavigationHistory();

        Task ClearNavigationToPage<T>() where T : Page;

        Task ShowModalAsync<T>() where T : Page;

        Task RemoveModalAsync();
    }

    public enum MasterDetailNavigationBehaviour
    {
        HideWhenNavigating,
        StayOpen
    }
}