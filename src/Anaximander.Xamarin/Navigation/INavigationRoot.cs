using Xamarin.Forms;

namespace Anaximander.Xamarin.Navigation
{
    internal interface INavigationRoot
    {
        INavigation Navigation { get; set; }
        Page MainPage { get; set; }
    }
}