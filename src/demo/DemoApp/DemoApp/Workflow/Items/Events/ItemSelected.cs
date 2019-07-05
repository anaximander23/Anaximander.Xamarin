using DemoApp.Models;
using MediatR;

namespace DemoApp.Workflow.Items.Events
{
    public class ItemSelected : IRequest<Unit>
    {
        public ItemSelected(Item selectedItem)
        {
            SelectedItem = selectedItem;
        }

        public Item SelectedItem { get; }
    }
}