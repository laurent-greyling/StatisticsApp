using Nfield.Stats.Models;
using Nfield.Stats.Utilities;
using Nfield.Stats.ViewModels;
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
                var accessToken = new SignInViewModel(new SignInModel
                {
                    Domain = Domain.Text,
                    UserName = UserName.Text,
                    Password = Password.Text
                });
                
                if (accessToken.AccessToken.IsFaulted)
                {
                    await DisplayAlert("Access Denied", "User Name or Password is Incorrect", "OK");
                }

                if (accessToken.AccessToken.IsSuccessfullyCompleted && string.IsNullOrEmpty(accessToken.AccessToken.Result))
                {
                    throw new System.Exception();
                }

                BindingContext = accessToken;
            }
            catch (System.Exception)
            {
                await DisplayAlert("Access Denied", "User Name or Password is Incorrect", "OK");
            }
        }
    }
}
