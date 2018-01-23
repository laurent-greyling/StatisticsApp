using Newtonsoft.Json;
using StatisticsApp.Helpers;
using StatisticsApp.Models;
using System;
using System.IO;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatisticsApp.Views
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurveyStatisticsPage : TabbedPage
    {
        public SurveyInfo SurveyInfo { get; set; }
        public bool TargetVisible { get; set; } = false;

        public SurveyCountsModel SurveyCounts {get;set;}        
        private AccessToken Token { get; set; }
        private string ServerUrl { get; set; }
        private string SurveyId { get; set; }

        public SurveyStatisticsPage(AccessToken token, string serverUrl, SurveyDetails surveyDetails)
        {
            InitializeComponent();
            Token = token;
            ServerUrl = serverUrl;
            SurveyId = surveyDetails.SurveyId;
            GetCounts();
            Percentages(surveyDetails);

            BindingContext = this;
        }

        private void Share(object sender, EventArgs e)
        {
            try
            {
                var body = $@"Please see the current status of {SurveyInfo.SurveyName}:
Completed Interviews: {SurveyInfo.Total}
Successful: {SurveyCounts.SuccessfulCount}
Dropped Out: {SurveyCounts.DroppedOutCount}
Screened Out: {SurveyCounts.ScreenedOutCount}
Rejected: {SurveyCounts.RejectedCount}";

                Device.OpenUri(new Uri($"mailto:?subject={SurveyInfo.SurveyName}%20Statistics&body={body}"));
            }
            catch (Exception w)
            {
                var ex = w;
                throw;
            }
              
        }

        private void GetCounts()
        {
            try
            {
                var url = $"{ServerUrl}/v1/Surveys/{SurveyId}/Counts";
                var request = new RestApi().Get(url, Token);

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        SurveyCounts = JsonConvert.DeserializeObject<SurveyCountsModel>(content);
                    }
                }
            }
            catch (Exception)
            {
                DisplayAlert("Oeps", $"No data for {SurveyInfo.SurveyName}", "Ok");
            }            
        }

        private void Percentages(SurveyDetails surveyDetails)
        {
            var total = (SurveyCounts.SuccessfulCount + SurveyCounts.DroppedOutCount + SurveyCounts.ScreenedOutCount + SurveyCounts.RejectedCount);
            
            var successPerc = Math.Round((((decimal)SurveyCounts.SuccessfulCount / (decimal)total)*100),1);            
            var dropPerc = Math.Round((((decimal)SurveyCounts.DroppedOutCount / (decimal)total) * 100), 1);
            var screenPerc = Math.Round((((decimal)SurveyCounts.ScreenedOutCount / (decimal)total) * 100), 1);
            var rejectPerc = Math.Round((((decimal)SurveyCounts.RejectedCount / (decimal)total) * 100), 1);
            var totalPerc = Math.Round((successPerc + dropPerc + screenPerc + rejectPerc), 1);

            SurveyInfo = new SurveyInfo
            {
                SurveyName = surveyDetails.SurveyName,
                Success = $"{SurveyCounts.SuccessfulCount} total successful interviews",
                ActiveLive = $"{SurveyCounts.ActiveLiveCount} active live interviews",
                ActiveTest = $"{SurveyCounts.ActiveTestCount} active test interviews",
                Total = (SurveyCounts.SuccessfulCount + SurveyCounts.DroppedOutCount + SurveyCounts.ScreenedOutCount + SurveyCounts.RejectedCount).ToString(),
                PercSuccess = $"{successPerc}%",
                PercDrop = $"{dropPerc}%",
                PercScreen = $"{screenPerc}%",
                PercReject = $"{rejectPerc}%",
                PercTotal = $"{totalPerc}%"
            };

            if (SurveyCounts.QuotaCounts != null)
            {
                TargetVisible = true;
                var targetPercentage = Math.Round((((decimal)SurveyCounts.SuccessfulCount / (decimal)SurveyCounts.QuotaCounts.Target) * 100), 1);
                SurveyInfo.PercOfTarget = $"{targetPercentage}% of Target";
            }            
        }
    }
}