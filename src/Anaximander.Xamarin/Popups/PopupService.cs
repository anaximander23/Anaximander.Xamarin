using System.Linq;
using System.Threading.Tasks;
using Anaximander.Xamarin.Navigation;
using Anaximander.Xamarin.UIElements;
using Xamarin.Forms;

namespace Anaximander.Xamarin.Popups
{
    internal class PopupService : IPopupService
    {
        public PopupService(INavigationRoot navigationRoot, IBusyIndicator busyIndicator)
        {
            _navigationRoot = navigationRoot;
            _busyIndicator = busyIndicator;
        }

        private readonly INavigationRoot _navigationRoot;
        private readonly IBusyIndicator _busyIndicator;

        private Page GetRootPage()
        {
            return _navigationRoot.MainPage;
        }

        public async Task ShowBusyIndicatorAsync()
        {
            await ShowBusyIndicatorAsync(null);
        }

        public async Task ShowBusyIndicatorAsync(string message)
        {
            _busyIndicator.Show(message);
            await Task.FromResult<object>(null);
        }

        public async Task DismissBusyIndicatorAsync()
        {
            _busyIndicator.Dismiss();
            await Task.FromResult<object>(null);
        }

        public async Task ShowAsync(Alert alert)
        {
            await GetRootPage().DisplayAlert(alert.Title, alert.Message, alert.Cancel);
        }

        public async Task<bool> ShowAsync(Dialog dialog)
        {
            return await GetRootPage().DisplayAlert(dialog.Title, dialog.Message, dialog.Confirm, dialog.Cancel);
        }

        public async Task<string> ShowAsync(OptionsDialog dialog)
        {
            return await GetRootPage().DisplayActionSheet(dialog.Title, dialog.Cancel, null, dialog.Options.ToArray());
        }

        public async Task<T> ShowAsync<T>(OptionsDialog<T> dialog)
        {
            string selectedLabel = await GetRootPage().DisplayActionSheet(dialog.Title, dialog.Cancel.Key, null, dialog.Options.Select(o => o.Key).ToArray());
            return dialog.GetSelectedOption(selectedLabel);
        }

        public async Task<string> ShowAsync(DangerousOptionsDialog dialog)
        {
            return await GetRootPage().DisplayActionSheet(dialog.Title, dialog.Cancel, dialog.DangerousOption, dialog.Options.ToArray());
        }

        public async Task<T> ShowAsync<T>(DangerousOptionsDialog<T> dialog)
        {
            string selectedLabel = await GetRootPage().DisplayActionSheet(dialog.Title, dialog.Cancel.Key, dialog.DangerousOption.Key, dialog.Options.Select(o => o.Key).ToArray());
            return dialog.GetSelectedOption(selectedLabel);
        }
    }
}