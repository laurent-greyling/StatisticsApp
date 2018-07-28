using Nfield.Stats.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Nfield.Stats.Entities;
using Nfield.Stats.Models;

namespace Nfield.Stats.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppSettingsView : ContentPage
	{
        public GetServerSettingsViewModel ServerDetails { get; set; }
		public AppSettingsView ()
		{
            ServerDetails = new GetServerSettingsViewModel();
            InitializeComponent ();
            
            BindingContext = ServerDetails;

        }

        private async Task Save_Server_Settings(object sender, EventArgs e)
        {
            var serverSelected = SelectedServer();

            if (string.IsNullOrEmpty(serverSelected))
            {
                await DisplayAlert("No Selection", "Please select a correct server to proceed", "Ok");
                return;
            }

            var server = new ServerEntity();
            switch (serverSelected)
            {
                case AppConst.America:
                    server.NfieldServer = AppConst.UrlAmerica;
                    server.ServerName = AppConst.America;
                    break;
                case AppConst.Asia:
                    server.NfieldServer = AppConst.UrlAsia;
                    server.ServerName = AppConst.Asia;
                    break;
                case AppConst.Blue:
                    server.NfieldServer = AppConst.UrlBlue;
                    server.ServerName = AppConst.Blue;
                    break;
                case AppConst.China:
                    server.NfieldServer = AppConst.UrlChina;
                    server.ServerName = AppConst.China;
                    break;
                case AppConst.Europe:
                    server.NfieldServer = AppConst.UrlEurope;
                    server.ServerName = AppConst.Europe;
                    break;
                case AppConst.Orange:
                    server.NfieldServer = AppConst.UrlOrange;
                    server.ServerName = AppConst.Orange;
                    break;
                case AppConst.Purple:
                    server.NfieldServer = AppConst.UrlPurple;
                    server.ServerName = AppConst.Purple;
                    break;
                case AppConst.Red:
                    server.NfieldServer = AppConst.UrlRed;
                    server.ServerName = AppConst.Red;
                    break;
                case AppConst.White:
                    server.NfieldServer = AppConst.UrlWhite;
                    server.ServerName = AppConst.White;
                    break;
                case AppConst.Yellow:
                    server.NfieldServer = AppConst.UrlYellow;
                    server.ServerName = AppConst.Yellow;
                    break;
                default:
                    break;
            }

            var serverSettings = new SetServerSettingsViewModel(server);

            if (!serverSettings.IsSet)
            {
                await DisplayAlert("Error", "Something went wrong saving setting, please try again", "Ok");                
            }

            await Navigation.PushAsync(new MainPage());
        }

        private void Clear_Server_Settings(object sender, EventArgs e)
        {
            Server.SelectedIndex = -1;
            TestServer.SelectedIndex = -1;
            new ClearServerSettingsViewModel();

            ServerDetails = null;
            BindingContext = ServerDetails;
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