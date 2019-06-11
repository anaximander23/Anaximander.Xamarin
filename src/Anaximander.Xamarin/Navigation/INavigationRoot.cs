using Xamarin.Forms;

namespace Anaximander.Xamarin.Navigation
{
    public interface INavigationRoot
    {
        INavigation Navigation { get; }
        Page MainPage { get; set; }
    }
}