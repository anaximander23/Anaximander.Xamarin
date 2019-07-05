using System.Threading;
using System.Threading.Tasks;
using Anaximander.Xamarin.Events;
using Anaximander.Xamarin.Navigation;
using DemoApp.Workflow;
using DemoApp.Workflow.About;
using MediatR;

namespace DemoApp
{
    public class ApplicationLifecycleDirector : INotificationHandler<ApplicationStarted>
    {
        private readonly INavigationService _navigationService;

        public ApplicationLifecycleDirector(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async Task Handle(ApplicationStarted notification, CancellationToken cancellationToken)
        {
            await _navigationService.SetMainPageAsync<MainPage>(MasterDetailNavigationBehaviour.HideWhenNavigating);
            await _navigationService.ClearNavigationToPage<AboutPage>();
        }
    }
}