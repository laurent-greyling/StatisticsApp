using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StatisticsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatisticsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SurveysPage : ContentPage
	{
        public ObservableCollection<SurveyDetails> Surveys { get; set; }
        
        public SurveysPage (AccessToken token, string serverUrl)
		{
			InitializeComponent ();
            GetSurveys(token, serverUrl);

            BindingContext = this;
        }

        private void GetSurveys(AccessToken token, string serverUrl)
        {
            try
            {
                var url = $"{serverUrl}/v1/Surveys";
                var request = WebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";
                request.Timeout = 15000;
                request.Headers.Add("Authorization", $"Basic {token.AuthenticationToken}");

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        Surveys = JsonConvert.DeserializeObject<ObservableCollection<SurveyDetails>>(content);                 
                    }
                }

                foreach (var item in Surveys)
                {
                    item.Icon = "smartphone.png";

                    if (item.SurveyType == "OnlineBasic")
                    {
                        item.Icon = "analytics.png";
                    }
                }
            }
            catch (Exception e)
            {
                var t = e;
            }
        }
	}
}