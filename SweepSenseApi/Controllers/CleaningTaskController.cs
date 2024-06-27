using Microsoft.AspNetCore.Mvc;
using SweepSenseApi.Models;
using SweepSenseApi.Services;

namespace SweepSenseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CleaningTaskController : ControllerBase
    {
        private readonly ICleaningTaskService _taskService;

        public CleaningTaskController(ICleaningTaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CleaningTask>>> GetTasks()
        {
            var tasks = await _taskService.GetTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CleaningTask>> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<CleaningTask>>> GetTasksByUserId(int userId)
        {
            var tasks = await _taskService.GetTasksByUserIdAsync(userId);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult> PostTask(CleaningTask task)
        {
            await _taskService.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, CleaningTask task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }
            await _taskService.UpdateTaskAsync(task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
