using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Anaximander.Xamarin;
using Anaximander.Xamarin.Lifecycle;
using DemoApp.Models;
using DemoApp.Workflow.Items.Events;
using MediatR;
using Xamarin.Forms;

namespace DemoApp.Workflow.Items
{
    public class ItemsPageModel : PageModel<ItemsPage>,
        IInitialisable,
        IOnAppearing
    {
        public ItemsPageModel(IMediator mediator)
        {
            _mediator = mediator;

            Items = new ObservableCollection<Item>();

            LoadItemsCommand = new Command(async () => await LoadItems());
            ItemSelectedCommand = new Command<SelectedItemChangedEventArgs>(async x => await ItemSelected(x.SelectedItem as Item));
            AddItemCommand = new Command(async () => await AddNewItem());
        }

        public Command LoadItemsCommand { get; }
        public Command ItemSelectedCommand { get; }
        public Command AddItemCommand { get; }

        public ObservableCollection<Item> Items { get; set; }

        public Item SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private readonly IMediator _mediator;

        private bool _isBusy;
        private Item _selectedItem;

        public Task Initialise()
        {
            return LoadItems();
        }

        public Task OnAppearing()
        {
            SelectedItem = null;

            return LoadItems();
        }

        private async Task LoadItems()
        {
            IsBusy = true;

            try
            {
                var newItems = await _mediator.Send(new GetItems());

                Items.Clear();

                foreach (var item in newItems)
                {
                    Items.Add(item);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ItemSelected(Item selectedItem)
        {
            if (selectedItem is null)
            {
                return;
            }

            await _mediator.Send(new ItemSelected(selectedItem));
        }

        private Task AddNewItem()
        {
            return _mediator.Send(new AddNewItem());
        }
    }
}