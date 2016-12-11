using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlickrSearch.Logic.DataAccessLayer.Providers
{
    public abstract class BaseApiProvider
    {
        protected readonly string _endpoint;
        protected readonly string _apiKey;
        protected readonly HttpClient _httpClient;


        public BaseApiProvider(string endpoint, string apiKey)
        {
            _endpoint = endpoint;
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        protected Task<T> ExecuteAsync<T>(string url)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var response = await _httpClient.GetAsync(new Uri(url))
                        .ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync()
                        .ConfigureAwait(false);
                    var deserialized = JsonConvert.DeserializeObject<T>(stringResult);
                    return deserialized;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    throw;
                }
            });
        }
    }
}