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
    public partial class SurveyStatisticsPage : TabbedPage
    {
        public ObservableCollection<SurveyInfo> SurveyInfo { get; set; }
        public ObservableCollection<QuotaGroup> QuotaGroup { get; set; }

        public SurveyCountsModel SurveyCounts { get; set; }
        private AccessToken Token { get; set; }
        private string ServerUrl { get; set; }
        private string SurveyId { get; set; }
        private SurveyDetails SurveyDetails { get; set; }

        public SurveyStatisticsPage(AccessToken token, string serverUrl, SurveyDetails surveyDetails)
        {
            InitializeComponent();
            Token = token;
            ServerUrl = serverUrl;
            SurveyId = surveyDetails.SurveyId;
            SurveyDetails = surveyDetails;
            GetCounts();
            Percentages();

            BindingContext = this;
        }

        private void Share(object sender, EventArgs e)
        {
            try
            {
                var body = $@"Please see the current status of {SurveyInfo[0].SurveyName}:
Completed Interviews: {SurveyInfo[0].Total}
Successful: {SurveyCounts.SuccessfulCount}
Dropped Out: {SurveyCounts.DroppedOutCount}
Screened Out: {SurveyCounts.ScreenedOutCount}
Rejected: {SurveyCounts.RejectedCount}";

                Device.OpenUri(new Uri($"mailto:?subject={SurveyInfo[0].SurveyName}%20Statistics&body={body}"));
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
                DisplayAlert("Oeps", $"No data for {SurveyInfo[0].SurveyName}", "Ok");
            }            
        }

        private void Percentages()
        {
            try
            {
                var total = (SurveyCounts.SuccessfulCount
               + SurveyCounts.DroppedOutCount
               + SurveyCounts.ScreenedOutCount
               + SurveyCounts.RejectedCount);

                if (total == null) total = 0;
                var successPerc = total != 0 ? Math.Round((((decimal)SurveyCounts.SuccessfulCount / (decimal)total) * 100), 1) : 0;
                var dropPerc = total != 0 ? Math.Round((((decimal)SurveyCounts.DroppedOutCount / (decimal)total) * 100), 1) : 0;
                var screenPerc = total != 0 ? Math.Round((((decimal)SurveyCounts.ScreenedOutCount / (decimal)total) * 100), 1) : 0;
                var rejectPerc = total != 0 ? Math.Round((((decimal)SurveyCounts.RejectedCount / (decimal)total) * 100), 1) : 0;
                var totalPerc = total != 0 ? Math.Round((successPerc + dropPerc + screenPerc + rejectPerc), 1) : 0;

                SurveyInfo = new ObservableCollection<SurveyInfo>
                {
                    new SurveyInfo
                    {
                        SurveyName = SurveyDetails.SurveyName,
                        Success = $"{SurveyCounts.SuccessfulCount} total successful interviews",
                        ActiveLive = $"{SurveyCounts.ActiveLiveCount} active live interviews",
                        ActiveTest = $"{SurveyCounts.ActiveTestCount} active test interviews",
                        Total = (SurveyCounts.SuccessfulCount
                        + SurveyCounts.DroppedOutCount
                        + SurveyCounts.ScreenedOutCount
                        + SurveyCounts.RejectedCount).ToString(),
                        PercSuccess = $"{successPerc}%",
                        PercDrop = $"{dropPerc}%",
                        PercScreen = $"{screenPerc}%",
                        PercReject = $"{rejectPerc}%",
                        PercTotal = $"{totalPerc}%",
                        SurveyCounts = SurveyCounts
                    }
                };

                if (SurveyCounts.QuotaCounts != null)
                {
                    NoQuota.IsVisible = false;
                    TargetBar.IsVisible = true;
                    SurveyInfo[0].TargetVisible = true;
                    var targetPercentage = Math.Round((((decimal)SurveyCounts.SuccessfulCount / (decimal)SurveyCounts.QuotaCounts.Target) * 100), 1);
                    SurveyInfo[0].PercOfTarget = $"{targetPercentage}% of Target";
                    SurveyInfo[0].Target = SurveyCounts.QuotaCounts.Target.ToString();
                    SetUpQuotas();
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private void Stats_Refresh(object sender, EventArgs e)
        {
            GetCounts();
            Percentages();
            SurveyStats.ItemsSource = SurveyInfo;
            SurveyStats.EndRefresh();
        }

        private void SetUpQuotas()
        {
            try
            {
                QuotaGroup = new ObservableCollection<QuotaGroup>();
                var group = new QuotaGroup("");
                QuotaGroup = Something(SurveyCounts.QuotaCounts.Attributes, group, QuotaGroup, "");
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ObservableCollection<QuotaGroup> Something(List<QuotaAttribute> quotaAttributes,
            QuotaGroup group,
            ObservableCollection<QuotaGroup> quotaCollection,
            string name,
            string levelName = "")
        {
            foreach (var attribute in quotaAttributes)
            {
                name = string.IsNullOrEmpty(levelName) ? attribute.Name : levelName;

                group = new QuotaGroup(name);
                foreach (var level in attribute.Levels)
                {                    
                    group.Add(level);
                    
                    Something(level.Attributes, group, quotaCollection, name, level.Name);
                }

                quotaCollection.Add(group);
            }

            return quotaCollection;
        }

        public void Handle_QuotaItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            var selected = ((ListView)sender).SelectedItem as QuotaLevel;

            DisplayAlert($"Extra Info {selected.Name}", $@"Dropped Out: {selected.DroppedOutCount}
Screened Out: {selected.UnsuccessfulCount}
Rejeceted: {selected.RejectedCount}", "Ok");

            ((ListView)sender).SelectedItem = null;
        }

        public void Handle_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void Tapped_Search()
        {
            if (!SearchBarQuota.IsVisible)
            {
                CurrentPage = QuotaTab;
                SearchBarQuota.IsVisible = true;
                SearchBarQuota.Focus();
            }
            else
            {
                SearchBarQuota.IsVisible = false;                
            }
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            QuotaList.ItemsSource = string.IsNullOrWhiteSpace(e.NewTextValue)
                ? QuotaList.ItemsSource = QuotaGroup
                : QuotaList.ItemsSource = QuotaGroup.Where(x => x.Any(n => n.Name.Contains(e.NewTextValue)));
        }
        
        private void Quota_Refresh(object sender, EventArgs e)
        {
            GetCounts();
            Percentages();
            QuotaList.ItemsSource = QuotaGroup;
            QuotaList.EndRefresh();
        }
    }
}