using System;
using System.Windows.Input;
using Anaximander.Xamarin;

using Xamarin.Forms;

namespace DemoApp.Workflow.About
{
    public class AboutViewModel : PageModel<AboutPage>
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ICommand OpenWebCommand { get; }

        private string _title;
    }
}