namespace Anaximander.Xamarin.Popups
{
    public class Dialog : Alert
    {
        public Dialog(string title, string message, string confirm, string cancel)
            : base(title, message, cancel)
        {
            Confirm = confirm;
        }

        public string Confirm { get; set; }
    }
}