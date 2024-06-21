using SweepSenseApi.Models;

namespace SweepSenseApi.Services
{
    public interface ICleaningTaskService
    {
        Task<IEnumerable<CleaningTask>> GetTasksForUserAsync(int userId);
        Task AddTaskAsync(CleaningTask task);
        Task UpdateTaskAsync(CleaningTask task);
        Task DeleteTaskAsync(int id);
    }
}
