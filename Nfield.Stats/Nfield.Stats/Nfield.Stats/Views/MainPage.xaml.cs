using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nfield.Stats.Views
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private async Task Navigate_Settings()
        {
            try
            {

                await Navigation.PushModalAsync(new AppSettingsView());
            }
            catch (System.Exception e)
            {

                throw;
            }
        }

    }
}
