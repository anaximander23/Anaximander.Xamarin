using System;
using System.Reflection;
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

        public Page CreatePage<T>() where T : Page
        {
            var page = _container.GetInstance<T>();

            //var pageModel = _container.TryGetInstance<IPageModel<T>>();

            //if (pageModel != null)
            //{
            //    page.BindingContext = pageModel;
            //}

            return page;
        }

        public Page CreatePage(Type pageType)
        {
            if (!typeof(Page).GetTypeInfo().IsAssignableFrom(pageType.GetTypeInfo()))
            {
                throw new ArgumentException("The requested type is not a Page.");
            }

            return _container.GetInstance(pageType) as Page;
        }
    }
}