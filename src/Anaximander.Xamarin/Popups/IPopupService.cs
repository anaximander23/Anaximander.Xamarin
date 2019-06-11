using System.Threading.Tasks;

namespace Anaximander.Xamarin.Popups
{
    public interface IPopupService
    {
        Task ShowBusyIndicatorAsync();
        Task ShowBusyIndicatorAsync(string message);
        Task DismissBusyIndicatorAsync();

        Task ShowAsync(Alert alert);

        Task<bool> ShowAsync(Dialog dialog);

        Task<string> ShowAsync(OptionsDialog dialog);
        Task<T> ShowAsync<T>(OptionsDialog<T> dialog);

        Task<string> ShowAsync(DangerousOptionsDialog dialog);
        Task<T> ShowAsync<T>(DangerousOptionsDialog<T> dialog);
    }
}