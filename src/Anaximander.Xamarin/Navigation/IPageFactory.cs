using System;
using Xamarin.Forms;

namespace Anaximander.Xamarin.Navigation
{
    public interface IPageFactory
    {
        Page CreatePage<T>() where T : Page;

        Page CreatePage(Type pageType);
    }
}