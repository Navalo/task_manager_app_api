using Microsoft.AspNetCore.Mvc;
using Task_Manager_Application.Interfaces;
using Task_Manager_Application.Models;

namespace Task_Manager_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tasks = await _taskService.GetAll();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var task = await _taskService.Get(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectTask taskObj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTask = await _taskService.Add(taskObj);

            if (createdTask == null)
            {
                return BadRequest("Failed to create task");
            }

            return Ok(new
            {
                message = "Task Created Successfully!!!",
                id = createdTask?.Data?.Id
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] ProjectTask projectTask)
        {
            var taskExists = await _taskService.TaskExists(id);
            if (!taskExists)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedTask = await _taskService.Update(id, projectTask);
            if (updatedTask == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Task Updated Successfully!!!",
                id = updatedTask?.Data?.Id
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var taskExists = await _taskService.TaskExists(id);
            if (!taskExists)
            {
                return NotFound();
            }

            var deleted = await _taskService.Delete(id);
            if (!deleted.Data)
            {
                return BadRequest("Failed to delete task");
            }

            return Ok(new
            {
                message = "Task Deleted Successfully!!!",
                id = id
            });
        }
    }
}
