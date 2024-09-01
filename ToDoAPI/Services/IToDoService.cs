using ToDoAPI.Models;

namespace ToDoAPI.Services
{
    public interface IToDoService
    {
        Task<List<ToDo>?> GetAllToDos();

        Task<ToDo?> GetToDo(int id);

        Task<ToDoSummary> SummaryToDo();

        Task CreateToDo(ToDo toDo);
    }
}
