using Nfield.Stats.Models;
using Nfield.Stats.ViewModels;

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
            SurveysList = new GetSurveysViewModel(accessToken.AuthenticationToken);

            InitializeComponent ();

            BindingContext = SurveysList;

        }
	}
}