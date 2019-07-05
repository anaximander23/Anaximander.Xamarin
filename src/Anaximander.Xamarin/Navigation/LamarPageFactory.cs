using System;
using System.Reflection;
using System.Threading.Tasks;
using Anaximander.Xamarin.Lifecycle;
using Lamar;
using Xamarin.Forms;

namespace Anaximander.Xamarin.Navigation
{
    internal class LamarPageFactory : IPageFactory
    {
        public LamarPageFactory(IContainer container)
        {
            _container = container;
        }

        private readonly IContainer _container;

        public async Task<Page> CreatePage<T>() where T : Page
        {
            var page = _container.GetInstance<T>();

            await BindMasterDetailPage(page);
            await BindPageModel(page);

            return page;
        }

        public async Task<Page> CreatePage(Type pageType)
        {
            if (!typeof(Page).GetTypeInfo().IsAssignableFrom(pageType.GetTypeInfo()))
            {
                throw new ArgumentException("The requested type is not a Page.");
            }

            var page = _container.GetInstance(pageType) as Page;

            await BindMasterDetailPage(page);
            await BindPageModel(page);

            return page;
        }

        public async Task<Page> CreatePageWithData<T, TData>(TData data) where T : Page
        {
            var page = _container.GetInstance<T>();

            await BindMasterDetailPage(page);
            await BindPageModel(page, data);

            return page;
        }

        private async Task BindMasterDetailPage<T>(T page) where T : Page
        {
            if (page is MasterDetailPage masterDetailPage)
            {
                var masterType = masterDetailPage.Master.GetType();
                var replacementMaster = await CreatePage(masterType);
                masterDetailPage.Master = replacementMaster;
            }
        }

        private async Task BindPageModel<T>(T page) where T : Page
        {
            var modelType = typeof(PageModel<>).MakeGenericType(page.GetType());

            var pageModel = _container.TryGetInstance(modelType) as PageModel;

            if (pageModel is null)
            {
                return;
            }

            if (pageModel is IInitialisable initialisablePageModel)
            {
                await initialisablePageModel.Initialise();
            }

            if (pageModel is IOnAppearing onAppearingPageModel)
            {
                page.Appearing += (obj, args) => onAppearingPageModel.OnAppearing();
            }

            page.BindingContext = pageModel;
        }

        private async Task BindPageModel<T, TData>(T page, TData data) where T : Page
        {
            var modelType = typeof(PageModel<>).MakeGenericType(page.GetType());

            var pageModel = _container.TryGetInstance(modelType) as PageModel;

            if (pageModel is null)
            {
                return;
            }

            if (pageModel is IInitialisable<TData> initialisablePageModel)
            {
                await initialisablePageModel.Initialise(data);
            }

            page.BindingContext = pageModel;
        }
    }
}