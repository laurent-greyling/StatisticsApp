
using Newtonsoft.Json;
using StatisticsApp.Helpers;
using StatisticsApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatisticsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SurveyPreviewPage : ContentPage
	{
        public string SurveyName { get; set; }

        public SurveyPreviewPage(AccessToken token, string serverUrl, SurveyDetails surveyDetails)
		{
			InitializeComponent ();
            SurveyName = surveyDetails.SurveyName;

            webview.Source = SurveyTestUrl(token, serverUrl, surveyDetails.SurveyId);

            BindingContext = this;
		}

        private string SurveyTestUrl(AccessToken token, string serverUrl, string surveyId)
        {
            try
            {
                var url = $"{serverUrl}/v1/Surveys/{surveyId}/PublicIds";
                var request = new RestApi().Get(url, token);
                var urlString = string.Empty;
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        var SurveyPublicIds = JsonConvert.DeserializeObject<List<SurveyPublicIdModel>>(content);
                        urlString = SurveyPublicIds.FirstOrDefault(x => x.LinkType == "ExternalTestId").Url;
                    }
                }

                return urlString;
            }
            catch (Exception)
            {
                DisplayAlert("Oeps", "Could not open preview", "Ok");
            }

            return string.Empty;
        }

        private void Share(object sender, EventArgs e)
        {
            //Do something here
        }

        private void WebView_OnNavigating()
        {
            progressx.IsRunning = true;
            progressx.IsVisible = true;
        }

        private void WebView_OnNavigated()
        {
            progressx.IsRunning = false;
            progressx.IsVisible = false;
        }
    }
}