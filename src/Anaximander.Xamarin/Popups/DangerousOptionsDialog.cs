using System.Collections.Generic;

namespace Anaximander.Xamarin.Popups
{
    public class DangerousOptionsDialog : OptionsDialog
    {
        public DangerousOptionsDialog(string title, string message, string cancel, string dangerousOption, IEnumerable<string> options)
            : base(title, message, cancel, options)
        {
            DangerousOption = dangerousOption;
        }

        public string DangerousOption { get; private set; }
    }

    public class DangerousOptionsDialog<T> : OptionsDialog<T>
    {
        public DangerousOptionsDialog(string title, string message, KeyValuePair<string, T> cancel, KeyValuePair<string, T> dangerousOption, Dictionary<string, T> options)
            : base(title, message, cancel, options)
        {
            DangerousOption = dangerousOption;
        }

        public KeyValuePair<string, T> DangerousOption { get; private set; }

        public override T GetSelectedOption(string selectedLabel)
        {
            if (selectedLabel.Equals(DangerousOption.Key))
            {
                return DangerousOption.Value;
            }
            else
            {
                return base.GetSelectedOption(selectedLabel);
            }
        }
    }
}