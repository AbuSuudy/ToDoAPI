using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using ToDoAPI.Models;

namespace ToDoIntegrationTest
{
    public class ToDoAPIIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ToDoAPIIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("ToDo/GetAllToDos")]
        [InlineData("ToDo/GetToDoSummary")]
        [InlineData("ToDo/GetToDo?id=1")]
        public async Task Get_EndpointsReturnSuccess(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert   
            Assert.Equivalent(response.StatusCode, 200);

        }

        [Fact]
        public async Task Post_EndpointsReturnSuccess()
        {
            //For request that modify data you could use testcontainers to create 
            //throwaway, lightweight instances of databases.

            // Arrange
            var client = _factory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "ToDo/CreateToDo");

            request.Content = JsonContent.Create(new ToDo
            {
                Title = "Test",
            });


            // Act
            var response = await client.SendAsync(request);

            // Assert   
            Assert.Equivalent(response.StatusCode, 200);

        }


        [Fact]
        public async Task Post_ReturnBadRequest_MissingRequiredTitleField()
        {
            //For request that modify data you could use testcontainers to create 
            //throwaway, lightweight instances of databases.

            // Arrange
            var client = _factory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "ToDo/CreateToDo");

            request.Content = JsonContent.Create(new ToDo
            {
 
            });


            // Act
            var response = await client.SendAsync(request);

            // Assert   
            Assert.Equivalent(response.StatusCode, 400);

        }

    }
}