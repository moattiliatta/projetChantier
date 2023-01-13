using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfChantierApp1._2
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        protected virtual void SetProperty<T>(ref T property, T value,
          [CallerMemberName] string propertyName = null)

        {
            if (Equals(property, value)) return;

            property = value;

            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
