using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RichardSzalay.MockHttp;
using System.Text.Json;
using ToDoAPI.Controllers;
using ToDoAPI.Models;
using ToDoAPI.Services;


namespace ToDoUnitTest
{
    public class ToDoUnitTest
    {
        MockHttpMessageHandler mockHttp;
        HttpClient client;
        Mock<IHttpClientFactory> mockHttpClientFactory;

        public ToDoUnitTest()
        {
            mockHttp = new MockHttpMessageHandler();
            client = mockHttp.ToHttpClient();
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);
        }


        [Fact]
        public async Task SummaryToDo_Correct_Summary_Returned()
        {
            //Arrange
            var expected = new ToDoSummary
            {
                TotalTodos = 5,
                CompletedTodos = 2,
                UncompletedTodos = 3
            };

            List<ToDo> toDos = new List<ToDo>();

            for (int i = 1; i <= 5; i++)
            {
                toDos.Add(new ToDo
                {
                    Title = "Title",
                    UserId = 1,
                    Completed = i % 2 == 0 ? true : false,
                });
            }

            mockHttp.When($"https://jsonplaceholder.typicode.com/ToDos")
                .Respond("application/json", JsonSerializer.Serialize(toDos));

            ToDoService sut = new ToDoService(mockHttpClientFactory.Object);

            //Act
            var actual = await sut.SummaryToDo();

            //Assert
            Assert.Equivalent(expected, actual);

        }


        [Fact]
        public async Task GetToDo_InvalidResponse_InternalServerError()
        {
            //Arrange
            var ToDoService = new Mock<ToDoService>(mockHttpClientFactory.Object);
            var logger = new Mock<ILogger<ToDoController>>();
            var expected = StatusCodes.Status500InternalServerError;

            mockHttp.When($"https://jsonplaceholder.typicode.com/ToDos/1")
              .Respond("application/json", "{Test : \"5\"}");

            ToDoController sut = new ToDoController(ToDoService.Object, logger.Object);

            //Act
            StatusCodeResult actual = (StatusCodeResult)await sut.GetToDo(1);

            //Assert
            Assert.Equal(expected, actual.StatusCode);

        }
    }
}