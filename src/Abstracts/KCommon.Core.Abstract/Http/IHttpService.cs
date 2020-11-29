using System.Threading.Tasks;

namespace KCommon.Core.Abstract.Http
{
    public interface IHttpService
    {
        // Get请求
        string Get(string url, bool forceHttp = false);
        Task<string> GetAsync(string url, bool forceHttp = false);
        T GetJson<T>(string url, bool forceHttp = false) where T : class;
        Task<T> GetJsonAsync<T>(string url, bool forceHttp = false) where T : class;
        
        // Post请求
        string Post(string url, object data, bool forceHttp = false);
        Task<string> PostAsync(string url, object data, bool forceHttp = false);
        T PostJson<T>(string url, object data, bool forceHttp = false) where T : class;
        Task<T> PostJsonAsync<T>(string url, object data, bool forceHttp = false) where T : class;

        // Put请求
        string Put(string url, object data, bool forceHttp = false);
        Task<string> PutAsync(string url, object data, bool forceHttp = false);
        T PutJson<T>(string url, object data, bool forceHttp = false) where T : class;
        Task<T> PutJsonAsync<T>(string url, object data, bool forceHttp = false) where T : class;

        // Delete请求
        string Delete(string url, object data, bool forceHttp = false);
        Task<string> DeleteAsync(string url, object data, bool forceHttp = false);
        T DeleteJson<T>(string url, object data, bool forceHttp = false) where T : class;
        Task<T> DeleteJsonAsync<T>(string url, object data, bool forceHttp = false) where T : class;
    }
}