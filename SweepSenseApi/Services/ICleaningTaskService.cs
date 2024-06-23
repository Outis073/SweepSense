using SweepSenseApi.Models;

namespace SweepSenseApi.Services
{
    public interface ICleaningTaskService
    {
        Task<IEnumerable<CleaningTask>> GetTasksAsync();
        Task<CleaningTask> GetTaskByIdAsync(int id);
        Task AddTaskAsync(CleaningTask task);
        Task UpdateTaskAsync(CleaningTask task);
        Task DeleteTaskAsync(int id);
    }
}
