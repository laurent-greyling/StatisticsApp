using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;

namespace Nfield.Stats.Utilities
{
    public class RestCalls : IRest
    {
        readonly HttpClient _httpClient;

        public RestCalls()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> GetAsync(string request, string authToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
            var response = await _httpClient.GetAsync(request).ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        public async Task<string> PostAsync(string request, string serialisedData)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(serialisedData, Encoding.Unicode, "application/json");
                var response = await _httpClient.PostAsync(request, content).ConfigureAwait(false);
                
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return result;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            
        }

        public async Task<string> PostAsync(string request, string authToken, string serialisedData)
        {
            var content = new StringContent(serialisedData);
            _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
            var response = await _httpClient.PostAsync(request, content).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return result;
        }
    }
}
