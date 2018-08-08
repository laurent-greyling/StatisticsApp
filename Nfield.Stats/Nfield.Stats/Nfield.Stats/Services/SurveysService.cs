using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nfield.Stats.Models;
using Nfield.Stats.Utilities;
using Xamarin.Forms;

namespace Nfield.Stats.Services
{
    public class SurveysService : ISurveysService
    {
        IRest _rest;
        readonly INfieldServer _server;
        readonly ISqliteService<SurveyDetails> _sqlite;
        readonly ISurveyCountsService _surveyCounts;
        public SurveysService()
        {
            _rest = DependencyService
            .Get<IRest>();

            _server = DependencyService
                .Get<INfieldServer>();

            _sqlite = DependencyService
                .Get<ISqliteService<SurveyDetails>>();

            _surveyCounts = DependencyService.Get<ISurveyCountsService>();
        }

        public SurveyDetails Get()
        {
            //to come soon
            throw new NotImplementedException();
        }

        public async Task SaveAsync(string authToken)
        {
            var currentSurveys = GetList().Select(x=>x.SurveyId).ToList();
            var surveysList = await _rest.GetAsync($"{_server.Get().NfieldServer}/v1/Surveys", authToken);
            var serverSurveys = JsonConvert.DeserializeObject<List<SurveyDetails>>(surveysList);
            var newSurveys = serverSurveys.Select(y => y.SurveyId).ToList();
            var nonLocalSurveys = newSurveys.Except(currentSurveys).ToList();

            foreach (var nonLocalSurvey in nonLocalSurveys)
            {
                _sqlite.Add(serverSurveys.FirstOrDefault(s => s.SurveyId == nonLocalSurvey));
            }           
        }

        public IEnumerable<SurveyDetails> GetList()
        {
            var surveys = _sqlite.Get();
            return surveys.OrderBy(o => o.IsFavourite);
        }

        public async Task<IEnumerable<SurveyDetails>> RetrieveAsync(string authToken)
        {
            await SaveAsync(authToken);
            var surveysList = GetList();

            foreach (var survey in surveysList)
            {
                var count = await _surveyCounts.SuccessfulCounts(authToken, survey.SurveyId);
                survey.SuccessFulCount = count.ToString();
                survey.Icon = survey.SurveyType == SurveyType.OnlineBasic.ToString()
                    ? AppConst.OnlineSurveyIcon 
                    : AppConst.MobileSurveyIcon;
                survey.Image = survey.Image ?? AppConst.UnSelectFavourite;
            }

            return surveysList.OrderBy(o=>o.IsFavourite);
        } 
    }
}
