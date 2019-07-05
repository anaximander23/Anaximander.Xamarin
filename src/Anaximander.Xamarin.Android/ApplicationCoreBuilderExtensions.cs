using Xamarin.Forms.Platform.Android;

namespace Anaximander.Xamarin.Android
{
    public static class ApplicationCoreBuilderExtensions
    {
        public static IApplicationCoreBuilder<TApp> OnAndroid<TApp>(this IApplicationCoreBuilder<TApp> applicationCoreBuilder, FormsAppCompatActivity activity)
            where TApp : ApplicationCore
        {
            applicationCoreBuilder.ConfigureServices(services => services.AddRange(new AndroidServiceRegistry(activity)));

            return applicationCoreBuilder;
        }
    }
}