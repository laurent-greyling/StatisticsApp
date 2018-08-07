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
    public class SurveyService : ISurveyService
    {
        IRest _rest;
        readonly INfieldServer _server;
        readonly ISqliteService<SurveyDetails> _sqlite;

        public SurveyService()
        {
            _rest = DependencyService
            .Get<IRest>();

            _server = DependencyService
                .Get<INfieldServer>();

            _sqlite = DependencyService
                .Get<ISqliteService<SurveyDetails>>();
        }

        public SurveyDetails Get()
        {
            //to come soon
            throw new NotImplementedException();
        }

        public async Task SaveAsync(string authToken)
        {
            var currentSurveys = GetList();
            var surveysList = await _rest.GetAsync($"{_server.Get().NfieldServer}/v1/Surveys", authToken);
            var serverSurveys = JsonConvert.DeserializeObject<List<SurveyDetails>>(surveysList);
            var surveys = serverSurveys.Except(currentSurveys).ToList();
            _sqlite.AddRange(surveys);            
        }

        public IEnumerable<SurveyDetails> GetList()
        {
            return _sqlite.Get();
        }

        public async Task<IEnumerable<SurveyDetails>> RetrieveAsync(string authToken)
        {
            await SaveAsync(authToken);
            return GetList();
        }
    }
}
