using Microsoft.AspNetCore.Mvc;
using SweepSenseApi.Models;
using SweepSenseApi.Services;

namespace SweepSenseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CleaningTasksController : ControllerBase
    {
        private readonly ICleaningTaskService _taskService;

        public CleaningTasksController(ICleaningTaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTasksForUser(int userId)
        {
            var tasks = await _taskService.GetTasksForUserAsync(userId);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] CleaningTask task)
        {
            await _taskService.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTasksForUser), new { userId = task.UserId }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] CleaningTask task)
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
