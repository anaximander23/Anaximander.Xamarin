using System.Collections.Generic;

namespace Anaximander.Xamarin.Popups
{
    public class OptionsDialog : Alert
    {
        public OptionsDialog(string title, string message, string cancel, IEnumerable<string> options)
            : base(title, message, cancel)
        {
            Options = options;
        }

        protected OptionsDialog(string title, string message, string cancel)
            : base(title, message, cancel)
        { }

        public IEnumerable<string> Options { get; protected set; }
    }

    public class OptionsDialog<T> : OptionsDialog
    {
        public OptionsDialog(string title, string message, KeyValuePair<string, T> cancel, Dictionary<string, T> options)
            : base(title, message, cancel.Key)
        {
            Cancel = cancel;
            Options = options;
        }

        private KeyValuePair<string, T> _cancel;
        public new KeyValuePair<string, T> Cancel
        {
            get { return _cancel; }
            protected set
            {
                _cancel = value;
                base.Cancel = _cancel.Key;
            }
        }

        public new Dictionary<string, T> Options { get; protected set; }

        public virtual T GetSelectedOption(string selectedLabel)
        {
            if (selectedLabel.Equals(Cancel.Key))
            {
                return Cancel.Value;
            }
            else
            {
                return Options[selectedLabel];
            }
        }
    }
}