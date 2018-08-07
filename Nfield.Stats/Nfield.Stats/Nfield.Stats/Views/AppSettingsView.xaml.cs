using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Nfield.Stats.Services;
using Nfield.Stats.ViewModels;

namespace Nfield.Stats.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppSettingsView : ContentPage
	{
        public INfieldServer _nfieldServer;

		public AppSettingsView ()
		{
            _nfieldServer = DependencyService
                .Get<INfieldServer>();

            InitializeComponent ();
            
            BindingContext = new GetServerSettingsViewModel();

        }

        private async Task Save_Server_Settings(object sender, EventArgs e)
        {
            var serverSelected = SelectedServer();

            if (string.IsNullOrEmpty(serverSelected))
            {
                await DisplayAlert("No Selection", "Please select a correct server to proceed", "Ok");
                return;
            }

            var serverSettings = new SetServerSettingsViewModel(serverSelected);

            if (!serverSettings.IsSet)
            {
                await DisplayAlert("Error", "Something went wrong saving setting, please try again", "Ok");
                return;
            }

            await Navigation.PushAsync(new MainPage());
        }

        private void Clear_Server_Settings(object sender, EventArgs e)
        {
            Server.SelectedIndex = -1;
            TestServer.SelectedIndex = -1;
            new ClearServerSettingsViewModel();
            BindingContext = null;
        }

        private string SelectedServer()
        {
            var isBothSelected = Server.SelectedIndex != -1 && TestServer.SelectedIndex != -1;
            var isServer = Server.SelectedIndex != -1 && TestServer.SelectedIndex == -1;
            var isTestServer = Server.SelectedIndex == -1 && TestServer.SelectedIndex != -1;

            var selectedItem = string.Empty;

            if (isBothSelected || isServer)
            {
                selectedItem = Server.Items[Server.SelectedIndex];
                TestServer.SelectedIndex = -1;
            }

            if (isTestServer)
            {
                selectedItem = TestServer.Items[TestServer.SelectedIndex];
            }

            return selectedItem;
        }
    }
}