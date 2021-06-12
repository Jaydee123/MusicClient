using Music.ConsoleApp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Music.ConsoleApp.Services
{
    public class HttpClientService<T> : IHttpClientService<T> where T : class
    {
        private static int TIME_OUT = 3000;

        public async Task<T> Get(string url)
        {
            T result = default(T);
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMilliseconds(TIME_OUT);
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var headerValue = new ProductInfoHeaderValue("MusicConsoleApp", "1.0");
                request.Headers.UserAgent.Add(headerValue);

                try
                {
                    var response = httpClient.SendAsync(request).Result;

                    response.EnsureSuccessStatusCode();
                    await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                    {
                        if (x.IsFaulted)
                            throw x.Exception;

                        result = JsonConvert.DeserializeObject<T>(x.Result);
                    });
                }
                catch
                {
                    // would usually provide logs, but this has just been added to skip if request is too long.
                    return null;
                }
            };

            return result;
        }

        public async Task<T> Post(string apiUrl, T postObject)
        {
            T result = default(T);
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(apiUrl, postObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);

                });
            }

            return result;
        }

        public async Task Put(string apiUrl, T putObject)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PutAsync(apiUrl, putObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
