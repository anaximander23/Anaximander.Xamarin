using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Anaximander.Xamarin
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

                field = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}