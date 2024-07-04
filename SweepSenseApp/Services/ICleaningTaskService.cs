using System.Collections.Generic;
using System.Threading.Tasks;
using SweepSenseApp.Models;

namespace SweepSenseApp.Services
{
    public interface ICleaningTaskService
    {
        Task<IEnumerable<CleaningTask>> GetTasksAsync();
        Task<CleaningTask> GetTaskByIdAsync(int id);
        Task AddTaskAsync(CleaningTask task);
        Task UpdateTaskAsync(CleaningTask task);
        Task DeleteTaskAsync(int id);
        Task<IEnumerable<CleaningTask>> GetTasksByUserIdAsync(int userId);
    }
}
