using Microsoft.AspNetCore.Mvc;
using SweepSenseApi.Models;
using SweepSenseApi.Services;

namespace SweepSenseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateReport([FromBody] Report report)
        {
            await _reportService.CreateReportAsync(report);
            return Ok();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Report>>> GetReportsByUser(int userId)
        {
            var reports = await _reportService.GetReportsByUserAsync(userId);
            return Ok(reports);
        }
    }
}