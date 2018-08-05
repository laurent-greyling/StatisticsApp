using Nfield.Stats.Models;
using Nfield.Stats.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Nfield.Stats.Services;

namespace Nfield.Stats.ViewModels
{
    public class GetSurveysViewModel : INotifyPropertyChanged
    {
        public NotifyTaskCompletion<IEnumerable<SurveyDetails>> _surveysList { get; set; }

        public NotifyTaskCompletion<IEnumerable<SurveyDetails>> SurveysList
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
            .Get<ISurveyService>();

            SurveysList = new NotifyTaskCompletion<IEnumerable<SurveyDetails>>(surveys.RetrieveAsync(authToken));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
