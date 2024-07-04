using SweepSenseApi.Data;
using SweepSenseApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweepSenseApi.Services
{
    public class CleaningTaskService : ICleaningTaskService
    {
        private readonly AppDbContext _context;

        public CleaningTaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CleaningTask>> GetTasksAsync()
        {
            return await _context.CleaningTasks.ToListAsync();
        }

        public async Task<CleaningTask> GetTaskByIdAsync(int id)
        {
            return await _context.CleaningTasks.FirstOrDefaultAsync(ct => ct.Id == id);
        }

        public async Task AddTaskAsync(CleaningTask task)
        {
            _context.CleaningTasks.Add(task);
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

        public async Task<IEnumerable<CleaningTask>> GetTasksByUserIdAsync(int userId)
        {
            return await _context.CleaningTasks.Where(t => t.UserId == userId).ToListAsync();
        }
    }
}
