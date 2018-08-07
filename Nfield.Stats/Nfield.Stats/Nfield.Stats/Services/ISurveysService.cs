using Nfield.Stats.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nfield.Stats.Services
{
    interface ISurveysService
    {
        SurveyDetails Get();

        IEnumerable<SurveyDetails> GetList();

        Task SaveAsync(string authToken);

        Task<IEnumerable<SurveyDetails>> RetrieveAsync(string authToken);
    }
}
