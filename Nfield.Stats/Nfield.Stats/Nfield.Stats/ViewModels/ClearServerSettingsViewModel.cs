using Nfield.Stats.Entities;
using Nfield.Stats.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Nfield.Stats.ViewModels
{
    public class ClearServerSettingsViewModel : INotifyPropertyChanged
    {
        public ClearServerSettingsViewModel()
        {
            DependencyService.Get<IDeleteLocalData<ServerEntity>>().DeleteAll();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
