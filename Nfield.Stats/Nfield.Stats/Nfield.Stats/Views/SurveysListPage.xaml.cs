using Nfield.Stats.Models;
using Nfield.Stats.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nfield.Stats.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SurveysListPage : ContentPage
	{
        public GetSurveysViewModel SurveysList { get; set; }
		public SurveysListPage (AuthorizationModel accessToken)
		{
            GetSurveys(accessToken.AuthenticationToken);

            InitializeComponent ();

            BindingContext = SurveysList;

        }

        private void GetSurveys(string authToken)
        {
            try
            {
                SurveysList = new GetSurveysViewModel(authToken);
            }
            catch (Exception)
            {
                DisplayAlert("No Surveys", "Could not retrieve survey information", "OK");
            }
        }

        private void Tapped_Search()
        {
            SearchBar.Focus();
            SearchBar.IsVisible = !SearchBar.IsVisible ? true : false;
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SurveyList.ItemsSource = string.IsNullOrWhiteSpace(e.NewTextValue)
                ? SurveyList.ItemsSource = SurveysList.SurveysList.Result
                : SurveyList.ItemsSource = SurveysList.SurveysList
                                           .Result
                                           .Where(n => n.SurveyName.ToLowerInvariant()
                                                   .Contains(e.NewTextValue.ToLowerInvariant()));
        }
    }
}