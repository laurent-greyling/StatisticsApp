using Nfield.Stats.Entities;
using Nfield.Stats.Services;
using Nfield.Stats.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
            var exist = DependencyService
                .Get<IGetLocalData<ServerEntity>>()
                .Get()
                .FirstOrDefault();

            if (exist == null)
            {
                DependencyService.Get<IAddLocalData<ServerEntity>>().Add(server);
            }
            else
            {
                DependencyService.Get<IUpdateLocalData<ServerEntity>>().Update(server);
            }
            
            var entity = DependencyService
                .Get<IGetLocalData<ServerEntity>>()
                .Get()
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
