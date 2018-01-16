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
	public partial class InterviewQualityPage : ContentPage
	{
		public InterviewQualityPage (AccessToken token, string serverUrl, string surveyId)
		{
			InitializeComponent ();
		}

        private void GetSurveyQuality(AccessToken token, string serverUrl, string surveyId)
        {
            try
            {
                var url = $"{serverUrl}/v1/Surveys/{surveyId}/InterviewQuality";
                var request = new RestApi().Get(url, token);
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        //return (FieldworkStatus)int.Parse(reader.ReadToEnd());
                    }
                }
            }
            catch (Exception)
            {
                //return FieldworkStatus.UnderConstruction;
            }
        }
	}
}