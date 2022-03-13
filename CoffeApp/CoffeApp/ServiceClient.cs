using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoffeApp
{
    class ServiceClient
    {
        private  HttpClient httpClient { get; set; }
        public string apiUrl { get; set; }

        public ServiceClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.apiUrl = "https://localhost:5001/Coffe/";
        }

        public async Task<string> PostAsync(object httpBody, string url)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(httpBody), Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync(url, httpContent);

            string result = null;
            if (response.IsSuccessStatusCode)
                result = response.Content.ReadAsStringAsync().Result;

            return result;
        }

        public string BuildUrl(string surffix)
        {
            return this.apiUrl + surffix;
        }
    }
}
