using SweepSenseApi.Data;
using SweepSenseApi.Models;

namespace SweepSenseApi.Services
{
    public class CleaningTaskService : ICleaningTaskService
    {
        private readonly AppDbContext _context;

        public CleaningTaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CleaningTask>> GetTasksForUserAsync(int userId)
        {
            return await _context.CleaningTasks
                .Where(t => t.UserId == userId)
                .Include(t => t.Location)
                .ToListAsync();
        }

        public async Task AddTaskAsync(CleaningTask task)
        {
            await _context.CleaningTasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(CleaningTask task)
        {
            _context.CleaningTasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.CleaningTasks.FindAsync(id);
            if (task != null)
            {
                _context.CleaningTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
