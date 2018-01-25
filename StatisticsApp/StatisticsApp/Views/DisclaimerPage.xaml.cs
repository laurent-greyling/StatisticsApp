using Newtonsoft.Json;
using StatisticsApp.Entities;
using StatisticsApp.Helpers;
using StatisticsApp.Models;
using StatisticsApp.Services;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatisticsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DisclaimerPage : ContentPage
	{
        public DisclaimerRead _db = new DisclaimerRead();

        public DisclaimerPage()
		{
            InitializeComponent();
        }

        private async Task Accept_Dislaimer(object sender, EventArgs e)
        {
            var isRead = _db.GetIsRead().FirstOrDefault();

            if (isRead == null || !isRead.IsRead)
            {
                _db.AddIsRead(true);
            }

            await Navigation.PushAsync(new MainPage());
        }

        private void Decline_Dislaimer(object sender, EventArgs e)
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}