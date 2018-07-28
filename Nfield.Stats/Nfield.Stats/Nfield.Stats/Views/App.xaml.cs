using Nfield.Stats.Entities;
using Nfield.Stats.Services;
using System;
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
            DependencyService.Register<GetLocalData<ServerEntity>>();
            DependencyService.Register<AddLocalData<ServerEntity>>();
            DependencyService.Register<UpdateLocalData<ServerEntity>>();
            DependencyService.Register<DeleteLocalData<ServerEntity>>();

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
