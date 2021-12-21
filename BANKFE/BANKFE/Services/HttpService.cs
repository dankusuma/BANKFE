using BANKFE.Interface;
using BANKFE.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BANKFE.Services
{
    public class HttpService : IHttpServices
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetData(string url)
        {
           
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var ress = await _httpClient.SendAsync(request);
            Console.WriteLine("test");
            return await ress.Content.ReadAsStringAsync();
        }
        public async Task<HttpResponseMessage> PostData(string url, object data)
        {

            var httpress = await _httpClient.PostAsync(url, new StringContent(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            Application.Json));

            return httpress;



        }
    }
}
