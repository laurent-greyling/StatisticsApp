using Nfield.Stats.Models;
using System.Threading.Tasks;

namespace Nfield.Stats.Services
{
    interface ISurveyCountsService
    {
        Task<SurveyCountsModel> Counts(string authToken, string surveyId);

        Task<int?> SuccessfulCounts(string authToken, string surveyId);
    }
}
