using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nfield.Stats.Entities;
using Nfield.Stats.Models;
using Nfield.Stats.Utilities;
using Xamarin.Forms;

namespace Nfield.Stats.Services
{
    public class SurveysService : ISurveysService
    {
        IRest _rest;
        readonly INfieldServer _server;
        readonly ISqliteService<SurveyDetailsEntity> _sqlite;
        readonly ISurveyCountsService _surveyCounts;
        public SurveysService()
        {
            _rest = DependencyService
            .Get<IRest>();

            _server = DependencyService
                .Get<INfieldServer>();

            _sqlite = DependencyService
                .Get<ISqliteService<SurveyDetailsEntity>>();

            _surveyCounts = DependencyService.Get<ISurveyCountsService>();
        }

        public SurveyDetailsEntity Get()
        {
            //to come soon
            throw new NotImplementedException();
        }

        public async Task SaveAsync(string authToken)
        {
            var currentSurveys = GetList().Select(x=>x.SurveyId).ToList();
            var surveysList = await _rest.GetAsync($"{_server.Get().NfieldServer}/v1/Surveys", authToken);
            var serverSurveys = JsonConvert.DeserializeObject<List<SurveyDetailsEntity>>(surveysList);
            var newSurveys = serverSurveys.Select(y => y.SurveyId).ToList();
            var nonLocalSurveys = newSurveys.Except(currentSurveys).ToList();

            foreach (var nonLocalSurveyId in nonLocalSurveys)
            {
                var count = await _surveyCounts.SuccessfulCounts(authToken, nonLocalSurveyId);
                var survey = serverSurveys.FirstOrDefault(s => s.SurveyId == nonLocalSurveyId);
                survey.SuccessFulCount = count.ToString();
                survey.Icon = survey.SurveyType == SurveyType.OnlineBasic.ToString()
                    ? AppConst.OnlineSurveyIcon
                    : AppConst.MobileSurveyIcon;
                survey.Image = survey.Image ?? AppConst.UnSelectFavourite;
                _sqlite.Add(survey);
            }       
        }

        public IEnumerable<SurveyDetailsEntity> GetList()
        {
            var surveys = _sqlite.Get();
            return surveys.OrderByDescending(o => o.IsFavourite);
        }

        public async Task<IEnumerable<SurveyDetailsEntity>> RetrieveAsync(string authToken)
        {
            await SaveAsync(authToken);
            var surveysList = GetList();
            return surveysList.OrderByDescending(o=>o.IsFavourite);
        } 
    }
}
