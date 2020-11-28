using System.Threading.Tasks;

namespace KCommon.Core.Abstract.Http
{
    public interface IHttpService
    {
        // Get请求
        string Get<TRequestData>(string url, TRequestData data, bool forceHttp = false);
        Task<string> GetAsync<TRequestData>(string url, TRequestData data, bool forceHttp = false);
        T GetToJson<T, TRequestData>(string url, TRequestData data, bool forceHttp = false);
        Task<T> GetToJsonAsync<T, TRequestData>(string url, TRequestData data, bool forceHttp = false);
        
        // Post请求
        string PostForm(HttpUrl url, HttpUrl postData, bool forceHttp = false);
        Task<string> PostFormAsync(HttpUrl url, HttpUrl postData, bool forceHttp = false);
        T PostFormToJson<T>(HttpUrl url, HttpUrl postData, bool forceHttp = false);
        Task<T> PostFormToJsonAsync<T>(HttpUrl url, HttpUrl postData, bool forceHttp = false);
        
        string PostJson(HttpUrl url, HttpUrl postData, bool forceHttp = false);
        Task<string> PostJsonAsync(HttpUrl url, HttpUrl postData, bool forceHttp = false);
        T PostJsonToJson<T>(HttpUrl url, HttpUrl postData, bool forceHttp = false);
        Task<T> PostJsonToJsonAsync<T>(HttpUrl url, HttpUrl postData, bool forceHttp = false);

        // Put请求
        string PutJson(HttpUrl url, HttpUrl putData, bool forceHttp = false);
        Task<string> PutJsonAsync(HttpUrl url, HttpUrl putData, bool forceHttp = false);
        T PutJsonToJson<T>(HttpUrl url, HttpUrl putData, bool forceHttp = false);
        Task<T> PutJsonToJsonAsync<T>(HttpUrl url, HttpUrl putData, bool forceHttp = false);
        
        // Delete请求
        string Delete(HttpUrl url, HttpUrl deleteData, bool forceHttp = false);
        Task<string> DeleteAsync(HttpUrl url, HttpUrl deleteData, bool forceHttp = false);
        T DeleteToJson<T>(HttpUrl url, HttpUrl deleteData, bool forceHttp = false);
        Task<T> DeleteToJsonAsync<T>(HttpUrl url, HttpUrl deleteData, bool forceHttp = false);
    }
}