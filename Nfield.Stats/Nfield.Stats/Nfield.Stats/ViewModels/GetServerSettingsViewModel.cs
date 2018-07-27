﻿
using Nfield.Stats.Entities;
using Nfield.Stats.Services;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Nfield.Stats.ViewModels
{
    public class GetServerSettingsViewModel : INotifyPropertyChanged
    {
        public ServerEntity _serverDetails { get; set; }

        public ServerEntity ServerDetails
        {
            get
            {
                return _serverDetails;
            }
            set
            {
                if (_serverDetails != value)
                {
                    _serverDetails = value;
                    OnPropertyChanged("ServerDetails");
                }
            }
        }

        public GetServerSettingsViewModel()
        {
            var test = DependencyService
                .Get<IGetLocalData<ServerEntity>>()
                .Get();

            var entity = DependencyService
                .Get<IGetLocalData<ServerEntity>>()
                .Get().FirstOrDefault();

            var ServerDetails = entity != null 
                ? DependencyService
                .Get<IGetLocalData<ServerEntity>>()
                .Get().FirstOrDefault()
                : new ServerEntity();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
