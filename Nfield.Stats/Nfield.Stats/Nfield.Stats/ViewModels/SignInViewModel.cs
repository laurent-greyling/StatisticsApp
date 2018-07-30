
using Newtonsoft.Json;
using Nfield.Stats.Models;
using Nfield.Stats.Services;
using Nfield.Stats.Utilities;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Nfield.Stats.ViewModels
{
    public class SignInViewModel : INotifyPropertyChanged
    {
        public NotifyTaskCompletion<string> _accessToken { get; set; }

        public NotifyTaskCompletion<string> AccessToken
        {
            get
            {
                return _accessToken;
            }
            set
            {
                if (_accessToken != value)
                {
                    _accessToken = value;
                    OnPropertyChanged("AccessToken");
                }
            }
        }

        public SignInViewModel(SignInModel signInModel)
        {
            var restService = DependencyService
                .Get<IRest>();
            var serverService = DependencyService
                .Get<INfieldServer>();

            var nfieldServer = serverService
                .Get()
                .ServerDetails
                .NfieldServer;

            var signInData = JsonConvert.SerializeObject(signInModel);

            AccessToken = new NotifyTaskCompletion<string>(restService.PostAsync($"{nfieldServer}/v1/SignIn", signInData));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
