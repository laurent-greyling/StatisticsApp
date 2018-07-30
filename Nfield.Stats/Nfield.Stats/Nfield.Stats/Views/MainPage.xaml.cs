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

    }
}
