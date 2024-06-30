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
