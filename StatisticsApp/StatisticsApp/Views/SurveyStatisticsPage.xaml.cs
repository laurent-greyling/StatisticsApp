using Newtonsoft.Json;
using StatisticsApp.Helpers;
using StatisticsApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatisticsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurveyStatisticsPage : TabbedPage
    {
        public string SurveyName { get; set; }
        public string Success { get; set; }
        public string ActiveLive { get; set; }
        public string ActiveTest { get; set; }
        public string Total { get; set; }
        public string PercOfTarget { get; set; }
        public string PercSuccess { get; set; }
        public string PercDrop { get; set; }
        public string PercScreen { get; set; }
        public string PercReject { get; set; }
        public string PercTotal { get; set; }

        public bool TargetVisible { get; set; } = false;

        public SurveyCountsModel SurveyCounts {get;set;}        
        private AccessToken Token { get; set; }
        private string ServerUrl { get; set; }
        private string SurveyId { get; set; }

        public SurveyStatisticsPage(AccessToken token, string serverUrl, SurveyDetails surveyDetails)
        {
            InitializeComponent();
            SurveyName = surveyDetails.SurveyName;
            Token = token;
            ServerUrl = serverUrl;
            SurveyId = surveyDetails.SurveyId;
            GetCounts();
            Success = $"{SurveyCounts.SuccessfulCount} total successful interviews";
            ActiveLive = $"{SurveyCounts.ActiveLiveCount} active live interviews";
            ActiveTest = $"{SurveyCounts.ActiveTestCount} active test interviews";
            Total = (SurveyCounts.SuccessfulCount + SurveyCounts.DroppedOutCount + SurveyCounts.ScreenedOutCount + SurveyCounts.RejectedCount).ToString();
            Percentages(int.Parse(Total));

            BindingContext = this;
        }

        private void Share(object sender, EventArgs e)
        {
            try
            {
                var body = $@"Please see the current status of {SurveyName}:
Completed Interviews: {Total}
Successful: {SurveyCounts.SuccessfulCount}
Dropped Out: {SurveyCounts.DroppedOutCount}
Screened Out: {SurveyCounts.ScreenedOutCount}
Rejected: {SurveyCounts.RejectedCount}";

                Device.OpenUri(new Uri($"mailto:?subject={SurveyName}%20Statistics&body={body}"));
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
                DisplayAlert("Oeps", $"No data for {SurveyName}", "Ok");
            }            
        }

        private void Percentages(int total)
        {
            var successPerc = Math.Round((((decimal)SurveyCounts.SuccessfulCount / (decimal)total)*100),1);            
            var dropPerc = Math.Round((((decimal)SurveyCounts.DroppedOutCount / (decimal)total) * 100), 1);
            var screenPerc = Math.Round((((decimal)SurveyCounts.ScreenedOutCount / (decimal)total) * 100), 1);
            var rejectPerc = Math.Round((((decimal)SurveyCounts.RejectedCount / (decimal)total) * 100), 1);
            var totalPerc = Math.Round((successPerc + dropPerc + screenPerc + rejectPerc), 1);
            PercSuccess = $"{successPerc}%";
            PercDrop = $"{dropPerc}%";
            PercScreen = $"{screenPerc}%";
            PercReject = $"{rejectPerc}%";
            PercTotal = $"{totalPerc}%";

            if (SurveyCounts.QuotaCounts != null)
            {
                TargetVisible = true;
                var targetPercentage = Math.Round((((decimal)SurveyCounts.SuccessfulCount / (decimal)SurveyCounts.QuotaCounts.Target) * 100), 1);
                PercOfTarget = $"{targetPercentage}% of Target";
            }            
        }
    }
}