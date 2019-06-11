namespace Anaximander.Xamarin.Popups
{
    public class Alert
    {
        public Alert()
        {
        }

        public Alert(string title, string message, string cancel)
        {
            Title = title;
            Message = message;
            Cancel = cancel;
        }

        public string Title { get; set; }
        public string Message { get; set; }
        public string Cancel { get; set; }
    }
}