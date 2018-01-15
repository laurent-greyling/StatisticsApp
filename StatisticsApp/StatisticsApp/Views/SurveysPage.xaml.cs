using Newtonsoft.Json;
using StatisticsApp.Helpers;
using StatisticsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatisticsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SurveysPage : ContentPage
	{
        public ObservableCollection<SurveyDetails> Surveys { get; set; }
        
        public SurveysPage(AccessToken token, string serverUrl)
        {
            InitializeComponent();
            GetSurveys(token, serverUrl);

            BindingContext = this;
        }
        
        private void GetSurveys(AccessToken token, string serverUrl)
        {
            try
            {
                var url = $"{serverUrl}/v1/Surveys";
                var request = new RestApi().Get(url, token);

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        Surveys = JsonConvert.DeserializeObject<ObservableCollection<SurveyDetails>>(content);
                        Surveys.OrderByDescending(x => x.SurveyName);
                    }
                }

                var removeSurvey = new List<SurveyDetails>();

                foreach (var item in Surveys.ToList())
                {
                    var status = FieldWorkStatus(token, serverUrl, item.SurveyId);
                    if (status == FieldworkStatus.UnderConstruction)
                    {
                        Surveys.Remove(item);
                        continue;
                    }

                    item.Icon = "smartphone.png";

                    if (item.SurveyType == "OnlineBasic")
                    {
                        item.Icon = "analytics.png";
                    }

                    item.SuccessFulCount = $"{CompletedCount(token, serverUrl, item.SurveyId)}";
                }

            }
            catch (Exception)
            {
                DisplayAlert("No Surveys", "Could not retrieve survey information", "OK");
            }
        }

        private string CompletedCount(AccessToken token, string serverUrl, string surveyId)
        {
            try
            {
                var url = $"{serverUrl}/v1/Surveys/{surveyId}/Counts";
                var request = new RestApi().Get(url, token);

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        var surveyCounts = JsonConvert.DeserializeObject<SurveyCountsModel>(content);
                        return surveyCounts.SuccessfulCount.ToString();
                    }
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }

        private FieldworkStatus FieldWorkStatus(AccessToken token, string serverUrl, string surveyId)
        {
            try
            {
                var url = $"{serverUrl}/v1/Surveys/{surveyId}/Fieldwork/Status";
                var request = new RestApi().Get(url, token);
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        return (FieldworkStatus) int.Parse(reader.ReadToEnd());
                    }
                }
            }
            catch (Exception)
            {
                return FieldworkStatus.UnderConstruction;
            }
        }

        private async Task Show_Options(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var surveyDetails = e.Item as SurveyDetails;
            
            var action = await DisplayActionSheet($"{surveyDetails.SurveyName}", "Cancel", null, "Stats", "Location", "Survey");
                        
        }
    }
}