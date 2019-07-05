using Anaximander.Xamarin.Events;
using Anaximander.Xamarin.Navigation;
using MediatR;
using Xamarin.Forms;

namespace Anaximander.Xamarin
{
    public class ApplicationCore : Application, INavigationRoot
    {
        public ApplicationCore(IMediator mediator)
        {
            _mediator = mediator;
        }

        INavigation INavigationRoot.Navigation { get; set; }

        Page INavigationRoot.MainPage
        {
            get => MainPage;
            set => MainPage = value;
        }

        private readonly IMediator _mediator;

        protected override void OnStart()
        {
            _mediator.Publish(new ApplicationStarted());
        }

        protected override void OnSleep()
        {
            _mediator.Publish(new ApplicationEnteredSleep());
        }

        protected override void OnResume()
        {
            _mediator.Publish(new ApplicationResumed());
        }
    }
}