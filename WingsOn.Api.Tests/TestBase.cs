using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace WingsOn.Api.Tests
{
    public class TestBase
    {
        protected HttpClient Client { get; }

        public TestBase()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());

            Client = server.CreateClient();  
        }

        protected HttpRequestMessage CreateRequestMessage(string method, string url, object body = null)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), url);

            if (body != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            }

            return request;
        }
    }
}
