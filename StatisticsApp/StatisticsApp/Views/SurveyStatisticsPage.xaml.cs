using StatisticsApp.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatisticsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurveyStatisticsPage : TabbedPage
    {
        public string SurveyName { get; set; }
        public SurveyStatisticsPage(AccessToken token, string serverUrl, SurveyDetails surveyDetails)
        {
            InitializeComponent();
            SurveyName = surveyDetails.SurveyName;
             

            BindingContext = this;
        }

        private void Share(object sender, EventArgs e)
        {
            //Do something here
        }
    }
}