using Newtonsoft.Json;
using StatisticsApp.Models;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatisticsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
        private string ServerUrl { get; set; }
        private AccessToken AccessToken { get; set; }

        public MainPage ()
		{
			InitializeComponent ();
        }

        private async Task SignIn()
        {
            try
            {
                var data = JsonConvert.SerializeObject(new SignInModel
                {
                    UserName = UserName.Text,
                    Domain = Domain.Text,
                    Password = Password.Text
                });

                var url = $"{ServerUrl}/v1/SignIn";
                var request = WebRequest.Create(new Uri(url));
                request.ContentType = "application/json";                
                request.Method = "POST";
                request.Timeout = 15000;

                using (var writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(data);
                    writer.Flush();
                }                

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        AccessToken = JsonConvert.DeserializeObject<AccessToken>(content);
                    }
                }

                if (string.IsNullOrEmpty(AccessToken.AuthenticationToken))
                {
                    throw new Exception();
                }

                await Navigation.PushAsync(new SurveysPage(AccessToken, ServerUrl));
            }
            catch (Exception)
            {
                await DisplayAlert("Access Denied", "User Name or Password is Incorrect", "OK");
            }
        }

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                switch (picker.Items[selectedIndex])
                {
                    case "RC":
                        ServerUrl = "https://rc.com";
                        break;
                    case "Blue":
                        ServerUrl = "https://blue.com";
                        break;
                    case "Red":
                        ServerUrl = "https://red.com";
                        break;
                    case "Orange":
                        ServerUrl = "https://orange.com";
                        break;
                    case "White":
                        ServerUrl = "https://white.com";
                        break;
                    case "Yellow":
                        ServerUrl = "https://yellow.com";
                        break;
                    default:
                        break;
                };
            }
        }
    }
}