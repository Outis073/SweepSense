using SweepSenseApi.Data;
using SweepSenseApi.Models;

namespace SweepSenseApi.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateReportAsync(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Report>> GetReportsByUserAsync(int userId)
        {
            return await _context.Reports
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }
    }
}
