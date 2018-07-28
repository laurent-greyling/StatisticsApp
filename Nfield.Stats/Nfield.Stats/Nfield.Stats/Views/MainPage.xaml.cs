using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nfield.Stats.Views
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
		}

        private async Task Navigate_Settings()
        {
            await Navigation.PushAsync(new AppSettingsView());
        }

    }
}
