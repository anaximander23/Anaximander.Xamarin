using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Anaximander.Xamarin.Navigation;
using DemoApp.Workflow.About;
using DemoApp.Workflow.Items;
using DemoApp.Workflow.Menu.Events;
using MediatR;
using Xamarin.Forms;

namespace DemoApp.Workflow.Menu
{
    public class MenuNavigationDirector
        : IRequestHandler<GetMainMenuItems, IEnumerable<string>>,
        IRequestHandler<MenuItemSelected>
    {
        public MenuNavigationDirector(INavigationService navigationService)
        {
            _navigationService = navigationService;

            _menuItems = new Dictionary<string, Action<INavigationService>>
            {
                ["Browse"] = async nav => await ClearAndNavigateTo<ItemsPage>(),
                ["About"] = async nav => await ClearAndNavigateTo<AboutPage>()
            };
        }

        private Dictionary<string, Action<INavigationService>> _menuItems;
        private readonly INavigationService _navigationService;

        public Task<IEnumerable<string>> Handle(GetMainMenuItems request, CancellationToken cancellationToken)
        {
            return Task.FromResult<IEnumerable<string>>(_menuItems.Keys.ToList());
        }

        public Task<Unit> Handle(MenuItemSelected request, CancellationToken cancellationToken)
        {
            if (_menuItems.TryGetValue(request.Item, out var selectedItem))
            {
                selectedItem.Invoke(_navigationService);
            }

            return Unit.Task;
        }

        private async Task ClearAndNavigateTo<T>() where T : Page
        {
            await _navigationService.ClearNavigationToPage<T>();
        }
    }
}