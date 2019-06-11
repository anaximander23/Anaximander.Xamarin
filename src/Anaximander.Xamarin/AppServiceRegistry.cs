using Lamar;

namespace Anaximander.Xamarin
{
    internal class AppServiceRegistry : ServiceRegistry
    {
        public AppServiceRegistry()
        {
        }

        protected void ScanForAppNavigationElements()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();

                scan.ConnectImplementationsToTypesClosing(typeof(PageModel<>));
            });
        }
    }
}