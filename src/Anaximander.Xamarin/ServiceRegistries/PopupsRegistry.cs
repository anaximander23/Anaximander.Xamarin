using Anaximander.Xamarin.Popups;
using Lamar;

namespace Anaximander.Xamarin.ServiceRegistries
{
    internal class PopupsRegistry : ServiceRegistry
    {
        public PopupsRegistry()
        {
            For<IPopupService>().Use<PopupService>().Singleton();
        }
    }
}