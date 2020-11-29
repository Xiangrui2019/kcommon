using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KCommon.Core.Abstract.Http;
using KCommon.Core.Abstract.Serializing;

namespace KCommon.Core.Http
{
    public class EmptyHttpService : IHttpService
    {
        public string Delete(string url, object data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> DeleteAsync(string url, object data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public T DeleteJson<T>(string url, object data, bool forceHttp = false) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task<T> DeleteJsonAsync<T>(string url, object data, bool forceHttp = false) where T : class
        {
            throw new System.NotImplementedException();
        }

        public string Get(string url, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetAsync(string url, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public T GetJson<T>(string url, bool forceHttp = false) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetJsonAsync<T>(string url, bool forceHttp = false) where T : class
        {
            throw new System.NotImplementedException();
        }

        public string Post(string url, object data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostAsync(string url, object data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public T PostJson<T>(string url, object data, bool forceHttp = false) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task<T> PostJsonAsync<T>(string url, object data, bool forceHttp = false) where T : class
        {
            throw new System.NotImplementedException();
        }

        public string Put(string url, object data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PutAsync(string url, object data, bool forceHttp = false)
        {
            throw new System.NotImplementedException();
        }

        public T PutJson<T>(string url, object data, bool forceHttp = false) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task<T> PutJsonAsync<T>(string url, object data, bool forceHttp = false) where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}