using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KCommon.Core.Abstract.Http;
using KCommon.Core.Abstract.Serializing;

namespace KCommon.Core.Http
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _client;
        private readonly IJsonSerializer _jsonSerializer;

        public HttpService(HttpClient client, IJsonSerializer jsonSerializer)
        {
            _client = client;
            _jsonSerializer = jsonSerializer;
        }

        public string Get(string url, bool forceHttp = false)
            => GetAsync(url, forceHttp).GetAwaiter().GetResult();

        public async Task<string> GetAsync(string url, bool forceHttp = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url.ToString())
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>())
            };

            request.Headers.Add("accept", "application/json, text/html");
            request.Headers.Add("user-agent", "KCommon Http Client");

            using var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new WebException($"The remote server returned unexpected status code: {response.StatusCode} - {response.ReasonPhrase}.");
            }
        }

        public T GetJson<T>(string url, bool forceHttp = false) where T : class
            => GetJsonAsync<T>(url, forceHttp).GetAwaiter().GetResult();

        public async Task<T> GetJsonAsync<T>(string url, bool forceHttp = false) where T : class
        {
            var response = await GetAsync(url, forceHttp);

            return _jsonSerializer.Deserialize<T>(response);
        }

        public string Post(string url, object data, bool forceHttp = false)
            => PostAsync(url, data, forceHttp).GetAwaiter().GetResult();

        public async Task<string> PostAsync(string url, object data, bool forceHttp = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url.ToString())
            {
                Content = new StringContent(_jsonSerializer.Serialize(data))
            };

            request.Headers.Add("content-type", "application/json");
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("user-agent", "KCommon Http Client");

            using var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new WebException($"The remote server returned unexpected status code: {response.StatusCode} - {response.ReasonPhrase}.");
            }
        }

        public T PostJson<T>(string url, object data, bool forceHttp = false) where T : class
            => PostJsonAsync<T>(url, data, forceHttp).GetAwaiter().GetResult();

        public async Task<T> PostJsonAsync<T>(string url, object data, bool forceHttp = false) where T : class
        {
            var response = await PostAsync(url, data, forceHttp);

            return _jsonSerializer.Deserialize<T>(response);
        }

        public string Put(string url, object data, bool forceHttp = false)
            => PutAsync(url, data, forceHttp).GetAwaiter().GetResult();

        public async Task<string> PutAsync(string url, object data, bool forceHttp = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url.ToString())
            {
                Content = new StringContent(_jsonSerializer.Serialize(data))
            };

            request.Headers.Add("content-type", "application/json");
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("user-agent", "KCommon Http Client");

            using var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new WebException($"The remote server returned unexpected status code: {response.StatusCode} - {response.ReasonPhrase}.");
            }
        }

        public T PutJson<T>(string url, object data, bool forceHttp = false) where T : class
           => PutJsonAsync<T>(url, data, forceHttp).GetAwaiter().GetResult();

        public async Task<T> PutJsonAsync<T>(string url, object data, bool forceHttp = false) where T : class
        {
            var response = await PutAsync(url, data, forceHttp);

            return _jsonSerializer.Deserialize<T>(response);
        }

        public string Delete(string url, object data, bool forceHttp = false)
            => DeleteAsync(url, data, forceHttp).GetAwaiter().GetResult();

        public async Task<string> DeleteAsync(string url, object data, bool forceHttp = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url.ToString())
            {
                Content = new StringContent(_jsonSerializer.Serialize(data))
            };

            request.Headers.Add("content-type", "application/json");
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("user-agent", "KCommon Http Client");

            using var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new WebException($"The remote server returned unexpected status code: {response.StatusCode} - {response.ReasonPhrase}.");
            }
        }

        public T DeleteJson<T>(string url, object data, bool forceHttp = false) where T : class
           => DeleteJsonAsync<T>(url, data, forceHttp).GetAwaiter().GetResult();

        public async Task<T> DeleteJsonAsync<T>(string url, object data, bool forceHttp = false) where T : class
        {
            var response = await DeleteAsync(url, data, forceHttp);

            return _jsonSerializer.Deserialize<T>(response);
        }
    }
}