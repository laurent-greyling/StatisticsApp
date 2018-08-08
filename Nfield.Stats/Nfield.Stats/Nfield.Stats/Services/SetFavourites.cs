using Nfield.Stats.Models;
using System.Linq;
using Xamarin.Forms;

namespace Nfield.Stats.Services
{
    public class SetFavourites : ISetFavourites
    {
        readonly ISqliteService<SurveyDetails> _sqlite;

        public SetFavourites()
        {
            _sqlite = DependencyService
                .Get<ISqliteService<SurveyDetails>>();
        }

        public void Set(string surveyId)
        {
            var surveys = _sqlite.Get();
            var survey = surveys.FirstOrDefault(s => s.SurveyId == surveyId);

            if (survey.IsFavourite)
            {
                survey.IsFavourite = false;
                survey.Image = AppConst.UnSelectFavourite;
            }
            else
            {
                survey.IsFavourite = true;
                survey.Image = AppConst.SelectFavourite;
            }

            _sqlite.Update(survey);
        }
    }
}
