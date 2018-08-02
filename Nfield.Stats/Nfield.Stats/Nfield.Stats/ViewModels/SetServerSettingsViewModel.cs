using Nfield.Stats.Entities;
using Nfield.Stats.Services;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Nfield.Stats.ViewModels
{
    public class SetServerSettingsViewModel : INotifyPropertyChanged
    {
        public bool _isSet { get; set; }

        public bool IsSet
        {
            get
            {
                return _isSet;
            }
            set
            {
                if (_isSet != value)
                {
                    _isSet = value;
                    OnPropertyChanged("IsSet");
                }
            }
        }

        public SetServerSettingsViewModel(ServerEntity server)
        {
            var service = DependencyService
                .Get<ISqliteService<ServerEntity>>();

            var exist = service.Get().FirstOrDefault();

            if (exist == null)
            {
                service.Add(server);
            }
            else
            {
                service.Update(server);
            }
            
            var entity = service.Get()
                .Where(x=>x.ServerName == server.ServerName);

            IsSet = entity != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
