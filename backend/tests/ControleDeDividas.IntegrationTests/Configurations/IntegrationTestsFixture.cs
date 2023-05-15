using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using System.Text;
using Xunit;

namespace ControleDeDividas.IntegrationTests.Configurations
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Program>> { }
    public class IntegrationTestsFixture<TProgram> : IDisposable where TProgram : class
    {
        public readonly DesafioAppFactory<TProgram> Factory;
        public HttpClient Client;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7
            };

            Factory = new DesafioAppFactory<TProgram>();
            Client = Factory.CreateClient(clientOptions);
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }

        public StringContent MontarContent(object dado)
        {
            return new StringContent(
                JsonSerializer.Serialize(dado),
                Encoding.UTF8,
                "application/json");
        }

        public async Task<T> DeserializeResponse<T>(HttpResponseMessage ResponseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await ResponseMessage.Content.ReadAsStringAsync(), options);
        }
    }
}
