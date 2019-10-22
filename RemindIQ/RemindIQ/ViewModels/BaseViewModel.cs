using System;
using System.Collections.Generic;
using System.Text;
using RemindIQ.Services;
using RemindIQ.Models;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RemindIQ.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDatabaseHelper<Reminder> Database => DependencyService.Get<IDatabaseHelper<Reminder>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
