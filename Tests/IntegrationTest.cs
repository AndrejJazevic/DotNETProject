using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Persistence;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApi;

namespace IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient Client;
        public IntegrationTest()
        {
            var Factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.Remove(services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(ItemDbContext)));
                    services.AddDbContext<ItemDbContext>(options => options.UseInMemoryDatabase("testDb"));
                });
            });
            Client = Factory.CreateClient();
        }

        protected async Task AuthenticateClient()
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer" , await GetTestJwt());
        }

        private async Task<string> GetTestJwt()
        {
            var client = new RestClient("https://kristupas.eu.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"41tMZ23CXDWVBe4t44zdPNAy3XVvhIMl\",\"client_secret\":\"XwaYlJHWM0behanBc-Y5CDE7sdb-mDK0OvZUrE1u6nTS6KncBOxzFZW9-1v9NYut\",\"audience\":\"https://karma\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);
            dynamic token = JsonConvert.DeserializeObject(response.Content);
            return token.access_token;
        }
    }
}
