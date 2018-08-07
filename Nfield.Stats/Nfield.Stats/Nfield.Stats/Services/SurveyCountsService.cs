using Newtonsoft.Json;
using Nfield.Stats.Models;
using Nfield.Stats.Utilities;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nfield.Stats.Services
{
    public class SurveyCountsService : ISurveyCountsService
    {
        readonly IRest _rest;
        readonly INfieldServer _server;

        public SurveyCountsService()
        {
            _rest = DependencyService
           .Get<IRest>();

            _server = DependencyService
                .Get<INfieldServer>();
        }

        public async Task<SurveyCountsModel> Counts(string authToken, string surveyId)
        {
            var counts = await _rest.GetAsync($"{_server.Get().NfieldServer}/v1/Surveys/{surveyId}/Counts", authToken);
            return JsonConvert.DeserializeObject<SurveyCountsModel>(counts);
        }

        public async Task<int?> SuccessfulCounts(string authToken, string surveyId)
        {
            var counts = await Counts(authToken, surveyId);
            return counts.SuccessfulCount ?? 0;
        }
    }
}
