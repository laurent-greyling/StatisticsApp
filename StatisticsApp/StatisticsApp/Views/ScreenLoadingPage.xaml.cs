using StatisticsApp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatisticsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScreenLoadingPage : ContentPage
	{
		public ScreenLoadingPage(AccessToken token, string serverUrl)
		{
			InitializeComponent();
            Navigation.PushAsync(new SurveysPage(token, serverUrl)).Wait();
		}
	}
}