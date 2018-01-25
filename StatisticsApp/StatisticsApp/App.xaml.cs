using System;
using System.Linq;
using StatisticsApp.Services;
using StatisticsApp.Views;
using Xamarin.Forms;

namespace StatisticsApp
{
	public partial class App : Application
	{

        public DisclaimerRead _db = new DisclaimerRead();

        public App ()
		{
			InitializeComponent();


            var isRead = _db.GetIsRead().FirstOrDefault();

            if (isRead == null || !isRead.IsRead)
            {
                MainPage = new NavigationPage(new DisclaimerPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
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
