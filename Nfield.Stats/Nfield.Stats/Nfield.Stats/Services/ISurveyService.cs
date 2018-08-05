using Nfield.Stats.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nfield.Stats.Services
{
    interface ISurveyService
    {
        SurveyDetails Get();

        IEnumerable<SurveyDetails> GetList();

        Task SaveAsync(string authToken);
    }
}
