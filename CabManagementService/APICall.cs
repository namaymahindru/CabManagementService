
using System.Net.Http.Headers;

namespace CabManagementService.ApiCall
{
    public static class ApiCall
    {
        private static IConfiguration _configuration;

        public static HttpClient Initial(IConfiguration configuration,string token="")
        {
          
            _configuration = configuration;
            var baseurl = _configuration.GetValue<string>("WebAPIBaseUrl");
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //if (token != null)
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //}
            return client;
        }
      
    }
}
