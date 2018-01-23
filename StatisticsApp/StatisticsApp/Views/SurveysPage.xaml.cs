﻿using Newtonsoft.Json;
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

        private string ServerUrl { get; set; }
        private AccessToken Token { get; set; }

        public SurveysPage(AccessToken token, string serverUrl)
        {
            InitializeComponent();
            ServerUrl = serverUrl;
            Token = token;
            GetSurveys();

            BindingContext = this;
        }
        
        private void GetSurveys()
        {
            try
            {
                var url = $"{ServerUrl}/v1/Surveys";
                var request = new RestApi().Get(url, Token);

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        Surveys = JsonConvert.DeserializeObject<ObservableCollection<SurveyDetails>>(content);                        
                    }
                }

                var removeSurvey = new List<SurveyDetails>();

                foreach (var item in Surveys.ToList())
                {
                    var status = FieldWorkStatus(item.SurveyId);
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

                    item.SuccessFulCount = $"{CompletedCount(item.SurveyId)}";
                }
            }
            catch (Exception)
            {
                DisplayAlert("No Surveys", "Could not retrieve survey information", "OK");
            }
        }

        private string CompletedCount(string surveyId)
        {
            try
            {
                var url = $"{ServerUrl}/v1/Surveys/{surveyId}/Counts";
                var request = new RestApi().Get(url, Token);

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        var surveyCounts = JsonConvert.DeserializeObject<SurveyCountsModel>(content);
                        if (surveyCounts.SuccessfulCount == null) return "0";

                        return surveyCounts.SuccessfulCount.ToString();
                    }
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }

        private FieldworkStatus FieldWorkStatus(string surveyId)
        {
            try
            {
                var url = $"{ServerUrl}/v1/Surveys/{surveyId}/Fieldwork/Status";
                var request = new RestApi().Get(url, Token);
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
            
            var action = await DisplayActionSheet($"{surveyDetails.SurveyName}", "Cancel", null, "Survey Preview", "Survey Statistics");

            switch (action)
            {
                case "Survey Statistics":
                    await Navigation.PushAsync(new SurveyStatisticsPage(Token, ServerUrl, surveyDetails));
                    break;
                case "Survey Preview":
                    await Navigation.PushAsync(new SurveyPreviewPage(Token, ServerUrl, surveyDetails));
                    break;
                default:
                    break;
            }
        }

        private void Tapped_Search()
        {
            SearchBar.IsVisible = !SearchBar.IsVisible ? true : false;
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SurveyList.ItemsSource = string.IsNullOrWhiteSpace(e.NewTextValue)
                ? SurveyList.ItemsSource = Surveys
                : SurveyList.ItemsSource = Surveys.Where(n => n.SurveyName.Contains(e.NewTextValue));
        }

        private void Surveys_Refresh(object sender, EventArgs e)
        {
            GetSurveys();
            SurveyList.ItemsSource = Surveys;
            SurveyList.EndRefresh();
        }
    }
}