using StatisticsApp.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using StatisticsApp.Interfaces;
using System.IO;
using System.Net.Mail;
using Android.Content;
using Java.IO;

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

        private async Task Share(object sender, EventArgs e)
        {
            try
            {
                var screenshotDependency = DependencyService.Get<IScreenshotManager>();
                if (screenshotDependency != null)
                {
                    var screenshotBytes = screenshotDependency.CaptureAsync();

                    var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);
                    var pictures = dir.AbsolutePath;
                    string filePath = Path.Combine(pictures, $"{SurveyName}-{Guid.NewGuid()}.png");
                    System.IO.File.WriteAllBytes(filePath, screenshotBytes);

                    //Device.OpenUri(new Uri("mailto:?attachment='"+ filePath + "'"));
                }
            }
            catch (Exception w)
            {
                var ex = w;
                throw;
            }
              
        }
    }
}