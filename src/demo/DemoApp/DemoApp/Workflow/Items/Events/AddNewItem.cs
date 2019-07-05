using DemoApp.Models;
using MediatR;

namespace DemoApp.Workflow.Items.Events
{
    public class AddNewItem : IRequest<Unit>
    {
        public AddNewItem()
        { }

        public AddNewItem(Item item)
        {
            Item = item;
        }

        public Item Item { get; set; }
    }
}