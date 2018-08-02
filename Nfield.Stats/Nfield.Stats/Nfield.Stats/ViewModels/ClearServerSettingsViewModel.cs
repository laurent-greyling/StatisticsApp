using Nfield.Stats.Entities;
using Nfield.Stats.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Nfield.Stats.ViewModels
{
    public class ClearServerSettingsViewModel : INotifyPropertyChanged
    {
        public ClearServerSettingsViewModel()
        {
            DependencyService.Get<ISqliteService<ServerEntity>>().DeleteAll();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
