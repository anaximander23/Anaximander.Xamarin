using Anaximander.Xamarin.Navigation;
using Lamar;

namespace Anaximander.Xamarin.ServiceRegistries
{
    internal class NavigationRegistry : ServiceRegistry
    {
        public NavigationRegistry()
        {
            For<INavigationRoot>().Use(context => context.GetInstance<ApplicationCore>()).Singleton();
            For<IPageFactory>().Use<LamarPageFactory>().Singleton();
            For<INavigationService>().Use<NavigationService>().Singleton();

            Scan(scan =>
            {
                scan.TheCallingAssembly();

                scan.ConnectImplementationsToTypesClosing(typeof(PageModel<>));
            });
        }
    }
}