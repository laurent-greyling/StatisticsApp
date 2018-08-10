using Nfield.Stats.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Nfield.Stats.Services;
using Nfield.Stats.Entities;

namespace Nfield.Stats.ViewModels
{
    public class GetSurveysViewModel : INotifyPropertyChanged
    {
        public NotifyTaskCompletion<IEnumerable<SurveyDetailsEntity>> _surveysList { get; set; }

        public NotifyTaskCompletion<IEnumerable<SurveyDetailsEntity>> SurveysList
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

        public GetSurveysViewModel(string authToken)
        {
            var surveys = DependencyService
            .Get<ISurveysService>();

            _surveysList = new NotifyTaskCompletion<IEnumerable<SurveyDetailsEntity>>(surveys.RetrieveAsync(authToken));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
