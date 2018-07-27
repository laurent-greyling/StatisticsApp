using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Nfield.Stats.Models;
using Newtonsoft.Json;

namespace Nfield.Stats.Utilities
{
    public class RestCalls : IRest
    {
        readonly HttpClient _httpClient;

        public RestCalls()
        {
            _httpClient = new HttpClient();
        }
        public async Task GetAsync(string request, string authToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
            await _httpClient.GetAsync(request).ConfigureAwait(false);
        }

        public async Task<string> PostAsync(string request, string serialisedData)
        {
            var content = new StringContent(serialisedData);
            var response = await _httpClient.PostAsync(request, content).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return result;
        }

        public async Task<string> PostAsync(string request, string authToken, string serialisedData)
        {
            var content = new StringContent(serialisedData);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
            var response = await _httpClient.PostAsync(request, content).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return result;
        }
    }
}
