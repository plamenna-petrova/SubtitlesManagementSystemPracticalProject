using EmployeesApp.IntegrationTests;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Tests.IntegrationTests
{
    public class ActorsControllerIntegrationTests : IClassFixture<TestingWebApplicationFactory<Program>>
    {
        private HttpClient _httpClient;

        public ActorsControllerIntegrationTests(TestingWebApplicationFactory<Program> testingWebApplicationFactory)
        {
            _httpClient = testingWebApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task Test_CalledActorsControllerIndexActionAfterLogin_ShouldReturnIndexView()
        {
            var loginPostRequest = new HttpRequestMessage(HttpMethod.Post, "/Identity/Account/Login");

            var loginCredentialsDictionary = new Dictionary<string, string>()
            {
                { "LoginBinding.Email", "admin@admin.com" },
                { "LoginBinding.Password", "Admin123/" }
            };

            loginPostRequest.Content = new FormUrlEncodedContent(loginCredentialsDictionary);

            var loginResponse = await _httpClient.SendAsync(loginPostRequest);

            loginResponse.EnsureSuccessStatusCode();

            var getAllActorsHttpResponseMessage = await _httpClient.GetAsync("/Actors");

            getAllActorsHttpResponseMessage.EnsureSuccessStatusCode();

            var actorsIndexViewPageResponseString = await getAllActorsHttpResponseMessage.Content.ReadAsStringAsync();

            Assert.True(!string.IsNullOrWhiteSpace(actorsIndexViewPageResponseString));
        }

        [Fact]
        public async Task Test_ExecuteCreateActorPOSTRequestAfterLogin_ReturnShouldReturnIndexViewContainingCreatedActor()
        {
            var loginPostRequest = new HttpRequestMessage(HttpMethod.Post, "/Identity/Account/Login");

            var loginCredentialsDictionary = new Dictionary<string, string>()
            {
                { "LoginBinding.Email", "admin@admin.com" },
                { "LoginBinding.Password", "Admin123/" }
            };

            loginPostRequest.Content = new FormUrlEncodedContent(loginCredentialsDictionary);

            var loginResponse = await _httpClient.SendAsync(loginPostRequest);

            loginResponse.EnsureSuccessStatusCode();

            var createNewActorPostRequest = new HttpRequestMessage(HttpMethod.Post, "/Actors/Create");

            var createNewActorDataDictionary = new Dictionary<string, string>()
            {
                { "FirstName", "Hugh" },
                { "LastName", "Jackman" }
            };

            createNewActorPostRequest.Content = new FormUrlEncodedContent(createNewActorDataDictionary);

            var createNewActorResponseMessage = await _httpClient.SendAsync(createNewActorPostRequest);

            createNewActorResponseMessage.EnsureSuccessStatusCode();

            var createdActorResponseString = await createNewActorResponseMessage.Content.ReadAsStringAsync();

            Assert.Multiple(() =>
            {
                Assert.Contains("Hugh", createdActorResponseString);
                Assert.Contains("Jackman", createdActorResponseString);
            });
        }
    }
}
