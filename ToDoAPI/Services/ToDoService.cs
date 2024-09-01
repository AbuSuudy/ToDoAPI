using System.Text.Json;
using ToDoAPI.Models;

namespace ToDoAPI.Services
{
    public class ToDoService : IToDoService
    {
        private HttpClient client;
        public ToDoService(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("ToDoAPI");
        }

        public async Task CreateToDo(ToDo toDo)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "ToDos");

            request.Content = JsonContent.Create(toDo);

            var response = await client.SendAsync(request);
        }

        public async Task<List<ToDo>?> GetAllToDos()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "ToDos");

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();


            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };


            List<ToDo>? toDos = JsonSerializer.Deserialize<List<ToDo>>(result, options);

            return toDos;
        }

        public async Task<ToDo?> GetToDo(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"ToDos/{id}");

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };


            ToDo? toDo = JsonSerializer.Deserialize<ToDo>(result, options);

            return toDo;
        }

        public async Task<ToDoSummary?> SummaryToDo()
        {
            var data = await GetAllToDos();

            if(data == null)
            {
                return null;
            }

            return new ToDoSummary
            {
                TotalTodos = data.Count,
                CompletedTodos = data.Where(x => x.Completed == true).Count(),
                UncompletedTodos = data.Where(x => x.Completed == false).Count()
            };
        }
    }
}
