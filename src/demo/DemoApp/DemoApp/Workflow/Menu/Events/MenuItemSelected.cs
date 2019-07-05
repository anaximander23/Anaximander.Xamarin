using MediatR;

namespace DemoApp.Workflow.Menu.Events
{
    public class MenuItemSelected : IRequest
    {
        public MenuItemSelected(string item)
        {
            Item = item;
        }

        public readonly string Item;
    }
}