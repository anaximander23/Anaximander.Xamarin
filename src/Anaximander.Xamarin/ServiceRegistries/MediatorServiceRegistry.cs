using Lamar;
using MediatR;

namespace Anaximander.Xamarin.ServiceRegistries
{
    internal class MediatorServiceRegistry : ServiceRegistry
    {
        public MediatorServiceRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();

                scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>));
                scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                scan.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
            });

            For<IMediator>().Use<Mediator>().Transient();

            For<ServiceFactory>().Use(ctx => ctx.GetInstance);
        }
    }
}