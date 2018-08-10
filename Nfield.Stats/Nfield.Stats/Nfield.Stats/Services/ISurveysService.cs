using Nfield.Stats.Entities;
using Nfield.Stats.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nfield.Stats.Services
{
    interface ISurveysService
    {
        SurveyDetailsEntity Get();

        IEnumerable<SurveyDetailsEntity> GetList();

        Task SaveAsync(string authToken);

        Task<IEnumerable<SurveyDetailsEntity>> RetrieveAsync(string authToken);
    }
}
