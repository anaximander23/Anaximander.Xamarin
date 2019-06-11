using System.Threading.Tasks;
using Anaximander.Xamarin.Navigation;
using Lamar;

namespace Anaximander.Xamarin
{
    public abstract class AppStartup
    {
        public abstract void ConfigureServices(ServiceRegistry services);

        public abstract Task InitialiseNavigation(INavigationService navigationService);
    }
}