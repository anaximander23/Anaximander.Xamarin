using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Anaximander.Xamarin.Navigation
{
    public interface IPageFactory
    {
        Task<Page> CreatePage<T>() where T : Page;
        Task<Page> CreatePage(Type pageType);
        Task<Page> CreatePageWithData<T, TData>(TData data) where T : Page;
    }
}