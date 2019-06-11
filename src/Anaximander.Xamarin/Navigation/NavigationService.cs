using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Anaximander.Xamarin.Navigation
{
    internal class NavigationService : INavigationService
    {
        public NavigationService(INavigationRoot navigationRoot, IPageFactory pageFactory)
        {
            _navigationRoot = navigationRoot;
            _pageFactory = pageFactory;
        }

        private readonly INavigationRoot _navigationRoot;
        private readonly IPageFactory _pageFactory;

        public void SetMainPage(Type pageType)
        {
            Page mainPage = _pageFactory.CreatePage(pageType);
            SetMainPage(mainPage);
        }

        public void SetMainPage<T>() where T : Page
        {
            Page mainPage = _pageFactory.CreatePage<T>();
            SetMainPage(mainPage);
        }

        private void SetMainPage(Page mainPage)
        {
            Page rootNavPage = new NavigationPage(mainPage);
            NavigationPage.SetHasNavigationBar(mainPage, false);

            _navigationRoot.MainPage = rootNavPage;
        }

        public async Task NavigateToPageAsync<T>() where T : Page
        {
            Page page = _pageFactory.CreatePage<T>();
            await _navigationRoot.Navigation.PushAsync(page, true);
        }

        public async Task NavigateBackAsync()
        {
            await _navigationRoot.Navigation.PopAsync();
        }

        public async Task NavigateBackToPageAsync<T>() where T : Page
        {
            var currentPage = ((NavigationPage)_navigationRoot.MainPage).CurrentPage;

            INavigation navController = _navigationRoot.Navigation;
            IReadOnlyList<Page> navStack = navController.NavigationStack;

            if (!navStack.Any(p => p is T))
            {
                throw new InvalidOperationException("The target page type is not in the navigation stack");
            }

            if (!(currentPage is T))
            {
                IEnumerable<Page> pagesToPop = navStack
                    .Reverse()
                    .Skip(1)
                    .TakeWhile(p => !(p is T));

                foreach (Page page in pagesToPop)
                {
                    navController.RemovePage(page);
                }
                await navController.PopAsync();
            }
        }

        public async Task NavigateToMainPageAsync()
        {
            await _navigationRoot.Navigation.PopToRootAsync();
        }

        public async Task ShowModalAsync<T>() where T : Page
        {
            Page page = _pageFactory.CreatePage<T>();
            await _navigationRoot.Navigation.PushModalAsync(page);
        }

        public async Task RemoveModalAsync()
        {
            await _navigationRoot.Navigation.PopModalAsync();
        }
    }
}