using SweepSenseApi.Models;

namespace SweepSenseApi.Services
{
    public interface IReportService
    {
        Task CreateReportAsync(Report report);
        Task<IEnumerable<Report>> GetReportsByUserAsync(int userId);
    }
}
