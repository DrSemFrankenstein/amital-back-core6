using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimonP_amital.Models;
using SimonP_amital.Requests;
using SimonP_amital.Services;

namespace SimonP_amital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly LogicService _service;

        public TasksController(ILogger<TasksController> logger, LogicService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("GetUsers")]
        public IEnumerable<Users> GetUsers()
        {
            return _service.GetUsers();
        }

        [HttpGet("GetTasks")]
        public IEnumerable<Tasks> GetTasks()
        {
            return _service.GetTasks();
        }

        [HttpGet("GetExceedTasksForTomorrow")]
        public IEnumerable<Tasks> GetExceedTasksForTomorrow()
        {
            return _service.GetExceedTasksForTomorrow();
        }

        [HttpPost("AddTask")]
        public IActionResult AddTask([FromBody] CreateTask task)
        {
            if (task == null)
            {
                return BadRequest("Task is null.");
            }

            var result = _service.AddTask(task);

            if (result.Success)
            {
                return Ok("Task added successfully.");
            }
            else
            {
                var errorResponse = new
                {
                    Title = "Error Adding Task",
                    Errors = new string[] { result.ErrorMessage }
                };
                return BadRequest(errorResponse);
            }
        }
    }
}
