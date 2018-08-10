using Newtonsoft.Json;
using Nfield.Stats.Models;
using Nfield.Stats.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nfield.Stats.Views
{
	public partial class MainPage : ContentPage
	{
        public SignInViewModel Auth { get; set; }
		public MainPage()
		{
            InitializeComponent();

            var serverSelected = new GetServerSettingsViewModel();

            if (string.IsNullOrEmpty(serverSelected.ServerDetails.ServerName))
            {
                Navigation.PushAsync(new AppSettingsView());
            }
        }

        private async Task Navigate_Settings()
        {
            await Navigation.PushAsync(new AppSettingsView());
        }

        private void Nfield_Signin()
        {
            Auth = new SignInViewModel(new SignInModel
            {
                Domain = Domain.Text,
                UserName = UserName.Text,
                Password = Password.Text
            });

            BindingContext = Auth;
        }

        private async Task Navigate_On_Success()
        {
            try
            {
                if (Auth == null || Auth.AccessToken.IsNotCompleted)
                {
                    return;
                }

                if (Auth.AccessToken.IsFaulted)
                {
                    await DisplayAlert("Error", "Oeps, something went wrong, please try again later", "Ok");
                    return;
                }

                if (Auth.AccessToken.IsSuccessfullyCompleted)
                {
                    var accessToken = JsonConvert.DeserializeObject<AuthorizationModel>(Auth.AccessToken.Result);

                    if (accessToken.NfieldErrorCode == NfieldErrorCode.NotAuthorized)
                    {
                        throw new Exception();
                    }

                    if (string.IsNullOrEmpty(accessToken.AuthenticationToken))
                    {
                        throw new Exception();
                    }

                    await Navigation.PushAsync(new SurveysListPage(accessToken));
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Access Denied", "User Name, Domain or Password is Incorrect", "OK");
            }
        }
    }
}
