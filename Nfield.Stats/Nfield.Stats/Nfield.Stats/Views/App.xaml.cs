using Nfield.Stats.Entities;
using Nfield.Stats.Services;
using Nfield.Stats.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Nfield.Stats.Views
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            DependencyService.Register<CreateSqliteTable>();
            DependencyService.Register<SqliteService<ServerEntity>>();
            DependencyService.Register<NfieldServer>();
            DependencyService.Register<RestCalls>();

            MainPage = new NavigationPage(new MainPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
