using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Anaximander.Xamarin.Navigation;
using DemoApp.Models;
using DemoApp.Services;
using DemoApp.Workflow.Items.Events;
using MediatR;

namespace DemoApp.Workflow.Items
{
    public class ItemsDirector
        : IRequestHandler<GetItems, IEnumerable<Item>>,
        IRequestHandler<AddNewItem>,
        IRequestHandler<ItemSelected>

    {
        public ItemsDirector(IDataStore<Item> dataStore, INavigationService navigationService)
        {
            _dataStore = dataStore;
            _navigationService = navigationService;
        }

        private readonly IDataStore<Item> _dataStore;
        private readonly INavigationService _navigationService;

        public Task<IEnumerable<Item>> Handle(GetItems request, CancellationToken cancellationToken)
        {
            return _dataStore.GetItemsAsync(true);
        }

        public async Task<Unit> Handle(AddNewItem request, CancellationToken cancellationToken)
        {
            if (request.Item is null)
            {
                await _navigationService.NavigateToPageAsync<NewItemPage>();
            }
            else
            {
                await _dataStore.AddItemAsync(request.Item);
                await _navigationService.NavigateBackAsync();
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(ItemSelected notification, CancellationToken cancellationToken)
        {
            await _navigationService.NavigateToPageAsync<ItemDetailPage, Item>(notification.SelectedItem);

            return Unit.Value;
        }
    }
}