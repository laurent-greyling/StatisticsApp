using Nfield.Stats.Models;
using System.Threading.Tasks;

namespace Nfield.Stats.Utilities
{
    public interface IRest
    {
        Task<string> GetAsync(string request, string authToken);
        Task<string> PostAsync(string request, string serialisedData);

        Task<string> PostAsync(string request, string authToken, string serialisedData);
    }
}
