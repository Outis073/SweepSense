using SweepSenseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SweepSenseApp.Services
{
    public class ReportService
    {
        private readonly ApiConfigService _apiConfigService;

        public ReportService(ApiConfigService apiConfigService)
        {
            _apiConfigService = apiConfigService;
        }

        public async Task CreateReportAsync(Report report)
        {
            var json = JsonSerializer.Serialize(report);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var apiUrl = $"{_apiConfigService.BaseUrl}/report";
            var response = await _apiConfigService.HttpClient.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Report>> GetReportsByUserAsync(string userId)
        {
            var apiUrl = $"{_apiConfigService.BaseUrl}/report/user/{userId}";
            var response = await _apiConfigService.HttpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"Reports JSON: {json}");
            return JsonSerializer.Deserialize<IEnumerable<Report>>(json);
        }
    }
}
