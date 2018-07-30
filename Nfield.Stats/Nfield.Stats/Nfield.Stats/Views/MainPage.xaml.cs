using Newtonsoft.Json;
using Nfield.Stats.Models;
using Nfield.Stats.Utilities;
using Nfield.Stats.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nfield.Stats.Views
{
	public partial class MainPage : ContentPage
	{
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

        private async Task Nfield_Signin()
        {
            try
            {
                var auth = new SignInViewModel(new SignInModel
                {
                    Domain = Domain.Text,
                    UserName = UserName.Text,
                    Password = Password.Text
                });

                BindingContext = auth;

                //if (auth.AccessToken.IsSuccessfullyCompleted)
                //{
                //    var result = JsonConvert.DeserializeObject<AuthorizationModel>(auth.AccessToken.Result);

                //    if (result.NfieldErrorCode == NfieldErrorCode.NotAuthorized)
                //    {
                //        throw new Exception();
                //    }

                //    if (string.IsNullOrEmpty(result.AuthenticationToken))
                //    {
                //        throw new Exception();
                //    }
                //}

            }
            catch (Exception)
            {
                await DisplayAlert("Access Denied", "User Name or Password is Incorrect", "OK");
            }
        }
    }
}
