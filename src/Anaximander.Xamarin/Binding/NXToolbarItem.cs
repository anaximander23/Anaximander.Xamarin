using System;
using Xamarin.Forms;

namespace Anaximander.Xamarin.Binding
{
    public class NXToolbarItem : ToolbarItem
    {
        public static readonly BindableProperty ClickedCommandProperty = BindableProperty.Create(nameof(ClickedCommand), typeof(Command), typeof(EventToCommandBehavior));

        public NXToolbarItem()
        {
            Clicked += ExecuteClickedCommand;
        }

        public NXToolbarItem(string name, string icon, Action activated, ToolbarItemOrder order = ToolbarItemOrder.Default, int priority = 0)
            : base(name, icon, activated, order, priority)
        {
        }

        public Command ClickedCommand
        {
            get => (Command)GetValue(ClickedCommandProperty);
            set => SetValue(ClickedCommandProperty, value);
        }

        private void ExecuteClickedCommand(object sender, EventArgs e)
        {
            var command = ClickedCommand;

            if (command?.CanExecute(e) == true)
            {
                command.Execute(e);
            }
            else if (command?.CanExecute(null) == true)
            {
                command.Execute(null);
            }
        }
    }
}