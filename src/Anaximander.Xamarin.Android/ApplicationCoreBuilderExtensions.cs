using Xamarin.Forms.Platform.Android;

namespace Anaximander.Xamarin.Android
{
    public static class ApplicationCoreBuilderExtensions
    {
        public static ApplicationCoreBuilder<TApp> OnAndroid<TApp>(this ApplicationCoreBuilder<TApp> applicationCoreBuilder, FormsAppCompatActivity activity)
            where TApp : ApplicationCore
        {
            applicationCoreBuilder.ConfigureServices(services => services.AddRange(new AndroidServiceRegistry(activity)));

            return applicationCoreBuilder;
        }
    }
}