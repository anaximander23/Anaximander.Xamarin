using System.Threading.Tasks;

namespace Anaximander.Xamarin.Lifecycle
{
    public interface IInitialisable
    {
        Task Initialise();
    }

    public interface IInitialisable<TData>
    {
        Task Initialise(TData data);
    }
}