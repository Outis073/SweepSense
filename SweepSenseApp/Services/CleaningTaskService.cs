using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SweepSenseApp.Models;

namespace SweepSenseApp.Services
{
    public class CleaningTaskService : ICleaningTaskService
    {
        private readonly List<CleaningTask> _tasks = new List<CleaningTask>();

        public async Task<IEnumerable<CleaningTask>> GetTasksAsync()
        {
            return await Task.FromResult(_tasks);
        }

        public async Task<CleaningTask> GetTaskByIdAsync(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            return await Task.FromResult(task);
        }

        public async Task AddTaskAsync(CleaningTask task)
        {
            _tasks.Add(task);
            await Task.CompletedTask;
        }

        public async Task UpdateTaskAsync(CleaningTask task)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.Name = task.Name;
                existingTask.Description = task.Description;
                // Update other properties as needed
            }
            await Task.CompletedTask;
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
            }
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<CleaningTask>> GetTasksByUserIdAsync(int userId) 
        {
            var tasks = _tasks.Where(t => t.UserId == userId).ToList();
            return await Task.FromResult(tasks);
        }
    }
}
