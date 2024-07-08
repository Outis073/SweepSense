using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SweepSenseApp.Models;

namespace SweepSenseApp.Services
{
    public class CleaningTaskService : ICleaningTaskService
    {
        private readonly ApiConfigService _apiConfigService;

        public CleaningTaskService(ApiConfigService apiConfigService)
        {
            _apiConfigService = apiConfigService;
        }

        public async Task<IEnumerable<CleaningTask>> GetTasksAsync()
        {
            var apiUrl = $"{_apiConfigService.BaseUrl}/cleaningtask";

            var response = await _apiConfigService.HttpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<CleaningTask>>(json);
        }

        public async Task<CleaningTask> GetTaskByIdAsync(int id)
        {
            var apiUrl = $"{_apiConfigService.BaseUrl}/cleaningtask/{id}";

            var response = await _apiConfigService.HttpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CleaningTask>(json);
        }

        public async Task AddTaskAsync(CleaningTask task)
        {
            var json = JsonSerializer.Serialize(task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var apiUrl = $"{_apiConfigService.BaseUrl}/cleaningtask";

            var response = await _apiConfigService.HttpClient.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateTaskAsync(CleaningTask task)
        {
            var json = JsonSerializer.Serialize(task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var apiUrl = $"{_apiConfigService.BaseUrl}/cleaningtask/{task.Id}";

            var response = await _apiConfigService.HttpClient.PutAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var apiUrl = $"{_apiConfigService.BaseUrl}/cleaningtask/{id}";

            var response = await _apiConfigService.HttpClient.DeleteAsync(apiUrl);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<CleaningTask>> GetTasksByUserIdAsync(int userId)
        {
            var apiUrl = $"{_apiConfigService.BaseUrl}/cleaningtask/user/{userId}";
            var response = await _apiConfigService.HttpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<CleaningTask>>(json);
        }
    }
}
