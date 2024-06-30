using SweepSenseApp.Models;
using System.Collections.Generic;
using System.Net.Http;
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
            return JsonSerializer.Deserialize<IEnumerable<Report>>(json);
        }

        public async Task DeleteReportAsync(int reportId)
        {
            try
            {
                var apiUrl = $"{_apiConfigService.BaseUrl}/report/{reportId}";
                var response = await _apiConfigService.HttpClient.DeleteAsync(apiUrl);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Request error: {ex.Message}");
                throw;
            }
        }
    }
}
