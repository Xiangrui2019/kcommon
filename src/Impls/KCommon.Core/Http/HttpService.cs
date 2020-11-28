using System.Net.Http;
using System.Threading.Tasks;
using KCommon.Core.Abstract.Http;

namespace KCommon.Core.Http
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _client;

        public HttpService(HttpClient client)
        {
            _client = client;
        }
        
        public string Get<TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetAsync<TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public TResponseData GetToJson<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<TResponseData> GetToJsonAsync<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public string Post<TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostAsync<TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public TResponseData PostFormToJson<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<TResponseData> PostFormToJsonAsync<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public TResponseData PostJsonToJson<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<TResponseData> PostJsonToJsonAsync<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public string Put<TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PutAsync<TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public TResponseData PutJsonToJson<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<TResponseData> PutJsonToJsonAsync<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public string Delete<TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> DeleteAsync<TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public TResponseData DeleteJsonToJson<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<TResponseData> DeleteJsonToJsonAsync<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }
    }
}