using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Models;
using ToDoAPI.Services;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ToDoController : ControllerBase
    {
        IToDoService _ToDoService;
        ILogger<ToDoController> _logger;
        public ToDoController(IToDoService toDoService, ILogger<ToDoController> logger)
        {
            _ToDoService = toDoService;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllToDos")]
        public async Task<IActionResult> GetAllToDos()
        {
            try
            {
                var result = await _ToDoService.GetAllToDos();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                //Only logs to console in prod would use a more permanent datastore e.g database
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet(Name = "GetToDo")]
        public async Task<IActionResult> GetToDo(int id)
        {
            try
            {
                var result = await _ToDoService.GetToDo(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet(Name = "GetToDoSummary")]
        public async Task<IActionResult>  GetToDoSummary()
        {
            try
            {
                var result = await _ToDoService.SummaryToDo();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost(Name = "CreateToDo")]
        public async Task<IActionResult> CreateToDo(ToDo toDo)
        {
            try
            {
                await _ToDoService.CreateToDo(toDo);

                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
