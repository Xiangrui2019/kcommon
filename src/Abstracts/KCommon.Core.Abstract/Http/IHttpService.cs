using System.Threading.Tasks;

namespace KCommon.Core.Abstract.Http
{
    public interface IHttpService
    {
        // Get请求
        string Get<TRequestData>(string url, TRequestData data, bool forceHttp = false);
        Task<string> GetAsync<TRequestData>(string url, TRequestData data, bool forceHttp = false);
        TResponseData GetToJson<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false);
        Task<TResponseData> GetToJsonAsync<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false);
        
        // Post请求
        string Post<TRequestData>(string url, TRequestData data, bool forceHttp = false);
        Task<string> PostAsync<TRequestData>(string url, TRequestData data, bool forceHttp = false);
        TResponseData PostFormToJson<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false);
        Task<TResponseData> PostFormToJsonAsync<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false);
        TResponseData PostJsonToJson<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false);
        Task<TResponseData> PostJsonToJsonAsync<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false);

        // Put请求
        string Put<TRequestData>(string url, TRequestData data, bool forceHttp = false);
        Task<string> PutAsync<TRequestData>(string url, TRequestData data, bool forceHttp = false);
        TResponseData PutJsonToJson<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false);
        Task<TResponseData> PutJsonToJsonAsync<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false);
        
        // Delete请求
        string Delete<TRequestData>(string url, TRequestData data, bool forceHttp = false);
        Task<string> DeleteAsync<TRequestData>(string url, TRequestData data, bool forceHttp = false);
        TResponseData DeleteJsonToJson<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false);
        Task<TResponseData> DeleteJsonToJsonAsync<TResponseData, TRequestData>(string url, TRequestData data, bool forceHttp = false);
    }
}