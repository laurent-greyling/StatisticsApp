using Nfield.Stats.Models;
using Nfield.Stats.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Nfield.Stats.ViewModels
{
    class SetFavouritesViewModel : INotifyPropertyChanged
    {
        public IEnumerable<SurveyDetails> _surveysList { get; set; }

        public IEnumerable<SurveyDetails> SurveysList
        {
            get
            {
                return _surveysList;
            }
            set
            {
                if (_surveysList != value)
                {
                    _surveysList = value;
                    OnPropertyChanged("SurveysList");
                }
            }
        }

        public SetFavouritesViewModel(string surveyId)
        {
            var service = DependencyService
                .Get<ISetFavourites>();

            var surveyService = DependencyService
                .Get<ISurveysService>();

            service.Set(surveyId);

            SurveysList = surveyService.GetList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
