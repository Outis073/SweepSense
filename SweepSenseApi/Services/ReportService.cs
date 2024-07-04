using SweepSenseApi.Data;
using SweepSenseApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SweepSenseApi.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;
        private readonly ICleaningTaskService _cleaningTaskService;
        private readonly IUserService _userService;

        public ReportService(AppDbContext context, ICleaningTaskService cleaningTaskService, IUserService userService)
        {
            _context = context;
            _cleaningTaskService = cleaningTaskService;
            _userService = userService;
        }

        public async Task CreateReportAsync(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            await CreateCleaningTaskForReport(report);
        }

        private async Task CreateCleaningTaskForReport(Report report)
        {
            try
            {
                var cleaners = (await _userService.GetUsersByRoleAsync("cleaner")).ToList();
                if (cleaners == null || !cleaners.Any())
                {
                    Console.WriteLine("No cleaners available.");
                    return;
                }

                var random = new Random();
                var assignedCleaner = cleaners[random.Next(cleaners.Count)];

                var cleaningTask = new CleaningTask
                {
                    Name = "Cleaning Task for Report " + report.Id,
                    Description = report.Description,
                    ScheduledDate = DateTime.Now,
                    RoomId = report.RoomId,
                    UserId = assignedCleaner.Id,
                    IsCompleted = false
                };

                await _cleaningTaskService.AddTaskAsync(cleaningTask);
                Console.WriteLine($"Cleaning task for report {report.Id} assigned to cleaner {assignedCleaner.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating cleaning task for report {report.Id}: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Report>> GetReportsByUserAsync(int userId)
        {
            return await _context.Reports
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Report> GetReportByIdAsync(int id)
        {
            return await _context.Reports.FindAsync(id);
        }

        public async Task DeleteReportAsync(Report report)
        {
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
        }
    }
}
