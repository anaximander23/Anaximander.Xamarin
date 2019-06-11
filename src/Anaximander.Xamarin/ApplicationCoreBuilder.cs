using System;
using System.Collections.Generic;
using Anaximander.Xamarin.Navigation;
using Anaximander.Xamarin.ServiceRegistries;
using Lamar;
using MediatR;

namespace Anaximander.Xamarin
{
    public sealed class ApplicationCoreBuilder<TApp> where TApp : ApplicationCore
    {
        public ApplicationCoreBuilder()
        {
            _serviceRegistryActions = new List<Action<ServiceRegistry>>();
        }

        private AppStartup _startup;

        private List<Action<ServiceRegistry>> _serviceRegistryActions;

        public ApplicationCoreBuilder<TApp> UseStartup<TStartup>() where TStartup : AppStartup, new()
        {
            _startup = new TStartup();

            return this;
        }

        public ApplicationCoreBuilder<TApp> AddServiceRegistry<TRegistry>()
            where TRegistry : ServiceRegistry, new()
        {
            ConfigureServices(services => services.IncludeRegistry<TRegistry>());

            return this;
        }

        public ApplicationCoreBuilder<TApp> ConfigureServices(Action<ServiceRegistry> configureServices)
        {
            _serviceRegistryActions.Add(configureServices);

            return this;
        }

        public ApplicationCore Build()
        {
            IContainer container = null;

            container = new Container(services =>
            {
                services.For<IContainer>().Use(container).Singleton();

                services.For<ApplicationCore>().Use<TApp>().Singleton();

                ConfigureCoreServices(services);

                foreach (var serviceRegistryAction in _serviceRegistryActions)
                {
                    serviceRegistryAction(services);
                }

                _startup.ConfigureServices(services);

                services.Scan(scan =>
                {
                    scan.AssemblyContainingType<TApp>();
                    scan.AssemblyContainingType(_startup.GetType());

                    scan.ConnectImplementationsToTypesClosing(typeof(PageModel<>));

                    scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>));
                    scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                    scan.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                });
            });

            var navigationService = container.GetInstance<INavigationService>();
            _startup.InitialiseNavigation(navigationService);

            var appCore = container.GetInstance<ApplicationCore>();
            return appCore;
        }

        private void ConfigureCoreServices(ServiceRegistry services)
        {
            services.IncludeRegistry<NavigationRegistry>();
            services.IncludeRegistry<PopupsRegistry>();
            services.IncludeRegistry<MediatorServiceRegistry>();
        }
    }
}