using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Anaximander.Xamarin.Navigation
{
    internal class NavigationService : INavigationService
    {
        public NavigationService(IPageFactory pageFactory, INavigationRoot navigationRoot)
        {
            _pageFactory = pageFactory;
            _navigationRoot = navigationRoot;
        }

        public Page CurrentPage => GetCurrentPage();

        private readonly IPageFactory _pageFactory;

        private INavigationRoot NavigationRoot
        {
            get => _navigationRoot ?? throw new InvalidOperationException("Cannot navigate; main page has not been set.");
            set => _navigationRoot = value;
        }

        private INavigationRoot _navigationRoot;

        private MasterDetailNavigationBehaviour _masterDetailNavigationBehaviour;

        private Page GetCurrentPage()
        {
            return _navigationRoot.Navigation.NavigationStack.FirstOrDefault() ?? _navigationRoot.MainPage;
        }

        public async Task SetMainPageAsync(Type pageType)
        {
            Page mainPage = await _pageFactory.CreatePage(pageType);
            SetMainPage(mainPage);
        }

        public async Task SetMainPageAsync<T>() where T : Page
        {
            Page mainPage = await _pageFactory.CreatePage<T>();
            SetMainPage(mainPage);
        }

        public async Task SetMainPageAsync<T>(MasterDetailNavigationBehaviour navigationBehaviour) where T : MasterDetailPage
        {
            Page mainPage = await _pageFactory.CreatePage<T>();
            SetMainPage(mainPage);

            _masterDetailNavigationBehaviour = navigationBehaviour;
        }

        private void SetMainPage(Page mainPage)
        {
            switch (mainPage)
            {
                case NavigationPage navigationPage:
                    _navigationRoot.MainPage = navigationPage;
                    _navigationRoot.Navigation = navigationPage.Navigation;

                    return;

                case MasterDetailPage masterDetailPage:
                    if (masterDetailPage.Detail is NavigationPage)
                    {
                        _navigationRoot.Navigation = masterDetailPage.Detail.Navigation;
                    }
                    else
                    {
                        var detailNavPage = masterDetailPage.Detail is null ? new NavigationPage() : new NavigationPage(masterDetailPage.Detail);
                        masterDetailPage.Detail = detailNavPage;
                        _navigationRoot.Navigation = detailNavPage.Navigation;
                    }

                    _navigationRoot.MainPage = masterDetailPage;

                    return;

                default:
                    Page rootNavPage = new NavigationPage(mainPage);

                    _navigationRoot.MainPage = mainPage;
                    _navigationRoot.Navigation = rootNavPage.Navigation;

                    return;
            }
        }

        public async Task NavigateToPageAsync<T>() where T : Page
        {
            Page page = await _pageFactory.CreatePage<T>();

            if (_masterDetailNavigationBehaviour == MasterDetailNavigationBehaviour.HideWhenNavigating
                && NavigationRoot.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = false;
            }

            await NavigationRoot.Navigation.PushAsync(page, animated: true);
        }

        public async Task NavigateToPageAsync<T, TData>(TData data) where T : Page
        {
            Page page = await _pageFactory.CreatePageWithData<T, TData>(data);

            if (_masterDetailNavigationBehaviour == MasterDetailNavigationBehaviour.HideWhenNavigating
                && NavigationRoot.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = false;
            }

            await NavigationRoot.Navigation.PushAsync(page, animated: true);
        }

        public Task NavigateBackAsync()
        {
            if (_masterDetailNavigationBehaviour == MasterDetailNavigationBehaviour.HideWhenNavigating
                && NavigationRoot.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = false;
            }

            return NavigationRoot.Navigation.PopAsync(animated: true);
        }

        public async Task NavigateBackToPageAsync<T>() where T : Page
        {
            var couldNavigateBack = await TryNavigateBackToPageAsync<T>();

            if (!couldNavigateBack)
            {
                throw new InvalidOperationException("The target page type is not in the navigation stack");
            }
        }

        public async Task<bool> TryNavigateBackToPageAsync<T>() where T : Page
        {
            INavigation navController = NavigationRoot.Navigation;
            IReadOnlyList<Page> navStack = navController.NavigationStack;

            if (!navStack.Any(p => p is T))
            {
                return false;
            }

            if (!(CurrentPage is T))
            {
                IEnumerable<Page> pagesToPop = navStack
                    .Reverse()
                    .Skip(1)
                    .TakeWhile(p => !(p is T));

                foreach (Page page in pagesToPop)
                {
                    navController.RemovePage(page);
                }
                await navController.PopAsync(animated: true);
            }

            if (_masterDetailNavigationBehaviour == MasterDetailNavigationBehaviour.HideWhenNavigating
                && NavigationRoot.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = false;
            }

            return true;
        }

        public Task NavigateToMainPageAsync()
        {
            if (_masterDetailNavigationBehaviour == MasterDetailNavigationBehaviour.HideWhenNavigating
                && NavigationRoot.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = false;
            }

            return NavigationRoot.Navigation.PopToRootAsync(animated: true);
        }

        public async Task ClearNavigationToPage<T>() where T : Page
        {
            var page = await _pageFactory.CreatePage<T>();
            var rootPage = NavigationRoot.Navigation.NavigationStack.First();

            await NavigationRoot.Navigation.PushAsync(page);
            NavigationRoot.Navigation.RemovePage(rootPage);

            if (_masterDetailNavigationBehaviour == MasterDetailNavigationBehaviour.HideWhenNavigating
                && NavigationRoot.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = false;
            }
        }

        public void ClearNavigationHistory()
        {
            INavigation navController = NavigationRoot.Navigation;
            IReadOnlyList<Page> navStack = navController.NavigationStack;

            IEnumerable<Page> pagesToPop = navStack
                .Reverse()
                .Skip(1);

            foreach (Page page in pagesToPop)
            {
                navController.RemovePage(page);
            }
        }

        public async Task ShowModalAsync<T>() where T : Page
        {
            Page page = await _pageFactory.CreatePage<T>();
            await NavigationRoot.Navigation.PushModalAsync(page);
        }

        public Task RemoveModalAsync()
        {
            return NavigationRoot.Navigation.PopModalAsync();
        }
    }
}