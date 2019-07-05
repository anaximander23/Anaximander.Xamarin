using System.Threading.Tasks;
using Anaximander.Xamarin;
using Anaximander.Xamarin.Navigation;
using DemoApp.Models;
using DemoApp.Services;
using DemoApp.Workflow;
using Lamar;

namespace DemoApp
{
    public class Startup : AppStartup
    {
        public override void ConfigureServices(ServiceRegistry services)
        {
            services.For<IDataStore<Item>>().Use<MockDataStore>().Singleton();
        }

        public override async Task InitialiseNavigation(INavigationService navigationService)
        {
            await navigationService.SetMainPageAsync<WelcomePage>();
        }
    }
}