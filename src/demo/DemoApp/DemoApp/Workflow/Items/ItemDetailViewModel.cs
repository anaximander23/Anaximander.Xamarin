using System.Threading.Tasks;
using Anaximander.Xamarin;
using Anaximander.Xamarin.Lifecycle;
using DemoApp.Models;

namespace DemoApp.Workflow.Items
{
    public class ItemDetailPageModel : PageModel<ItemDetailPage>, IInitialisable<Item>
    {
        public ItemDetailPageModel()
        {
        }

        public Item Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        private Item _item;

        public Task Initialise(Item data)
        {
            Item = data;

            return Task.CompletedTask;
        }
    }
}