using System.Collections.Generic;
using System.Threading.Tasks;
using Anaximander.Xamarin;
using Anaximander.Xamarin.Lifecycle;
using DemoApp.Workflow.Menu.Events;
using MediatR;
using Xamarin.Forms;

namespace DemoApp.Workflow.Menu
{
    public class MenuPageModel : PageModel<MenuPage>, IInitialisable
    {
        public MenuPageModel(IMediator mediator)
        {
            _mediator = mediator;

            MenuItems = new string[]
            {
                "Loading..."
            };

            ItemSelectedCommand = new Command<SelectedItemChangedEventArgs>(async item => await _mediator.Send(new MenuItemSelected(item.SelectedItem as string)));
        }

        public IEnumerable<string> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        public Command ItemSelectedCommand { get; private set; }

        private readonly IMediator _mediator;
        private IEnumerable<string> _menuItems;

        public async Task Initialise()
        {
            MenuItems = await _mediator.Send(new GetMainMenuItems());
        }
    }
}