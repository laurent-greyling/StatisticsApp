using Nfield.Stats.Models;
using System.Threading.Tasks;

namespace Nfield.Stats.Utilities
{
    public interface IRest
    {
        Task GetAsync(string request, string authToken);
        Task<string> GetAuthTokenAsync(string request, SignInModel content);

        Task<string> PostAsync(string request, string authToken, string serialisedData);
    }
}
