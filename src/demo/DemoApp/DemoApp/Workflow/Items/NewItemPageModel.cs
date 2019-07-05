using System.Threading.Tasks;
using Anaximander.Xamarin;
using DemoApp.Models;
using DemoApp.Workflow.Items.Events;
using MediatR;
using Xamarin.Forms;

namespace DemoApp.Workflow.Items
{
    public class NewItemPageModel : PageModel<NewItemPage>
    {
        private Item _item;
        private readonly IMediator _mediator;

        public NewItemPageModel(IMediator mediator)
        {
            _mediator = mediator;

            Item = new Item();

            SaveCommand = new Command(async () => await Save());
            CancelCommand = new Command(async () => await Cancel());
        }

        public Item Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private Task Save()
        {
            return _mediator.Send(new AddNewItem(Item));
        }

        private Task Cancel()
        {
            return Task.CompletedTask;
        }
    }
}