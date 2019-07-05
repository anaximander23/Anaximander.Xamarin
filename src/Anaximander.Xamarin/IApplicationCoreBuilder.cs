namespace Anaximander.Xamarin
{
    public interface IApplicationCoreBuilder<TApp>
        where TApp : ApplicationCore
    {
        IApplicationCoreBuilder<TApp> UseStartup<TStartup>() where TStartup : AppStartup, new();

        IApplicationCoreBuilder<TApp> AddServiceRegistry<TRegistry>() where TRegistry : Lamar.ServiceRegistry, new();

        IApplicationCoreBuilder<TApp> ConfigureServices(System.Action<Lamar.ServiceRegistry> configureServices);

        ApplicationCore Build();
    }

    public interface IApplicationCoreBuilder
        : IApplicationCoreBuilder<ApplicationCore>
    {
    }
}