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
            System.Diagnostics.Debug.WriteLine($"GetTasksAsync: Requesting {apiUrl}");

            var response = await _apiConfigService.HttpClient.GetAsync(apiUrl);
            System.Diagnostics.Debug.WriteLine($"GetTasksAsync: Response Status Code {response.StatusCode}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"GetTasksAsync JSON: {json}");
            return JsonSerializer.Deserialize<IEnumerable<CleaningTask>>(json);
        }

        public async Task<CleaningTask> GetTaskByIdAsync(int id)
        {
            var apiUrl = $"{_apiConfigService.BaseUrl}/cleaningtask/{id}";
            System.Diagnostics.Debug.WriteLine($"GetTaskByIdAsync: Requesting {apiUrl}");

            var response = await _apiConfigService.HttpClient.GetAsync(apiUrl);
            System.Diagnostics.Debug.WriteLine($"GetTaskByIdAsync: Response Status Code {response.StatusCode}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"GetTaskByIdAsync JSON: {json}");
            return JsonSerializer.Deserialize<CleaningTask>(json);
        }

        public async Task AddTaskAsync(CleaningTask task)
        {
            var json = JsonSerializer.Serialize(task);
            System.Diagnostics.Debug.WriteLine($"AddTaskAsync JSON: {json}");
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var apiUrl = $"{_apiConfigService.BaseUrl}/cleaningtask";
            System.Diagnostics.Debug.WriteLine($"AddTaskAsync: Requesting {apiUrl}");

            var response = await _apiConfigService.HttpClient.PostAsync(apiUrl, content);
            System.Diagnostics.Debug.WriteLine($"AddTaskAsync: Response Status Code {response.StatusCode}");
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateTaskAsync(CleaningTask task)
        {
            var json = JsonSerializer.Serialize(task);
            System.Diagnostics.Debug.WriteLine($"UpdateTaskAsync JSON: {json}");
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var apiUrl = $"{_apiConfigService.BaseUrl}/cleaningtask/{task.Id}";
            System.Diagnostics.Debug.WriteLine($"UpdateTaskAsync: Requesting {apiUrl}");

            var response = await _apiConfigService.HttpClient.PutAsync(apiUrl, content);
            System.Diagnostics.Debug.WriteLine($"UpdateTaskAsync: Response Status Code {response.StatusCode}");
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var apiUrl = $"{_apiConfigService.BaseUrl}/cleaningtask/{id}";
            System.Diagnostics.Debug.WriteLine($"DeleteTaskAsync: Requesting {apiUrl}");

            var response = await _apiConfigService.HttpClient.DeleteAsync(apiUrl);
            System.Diagnostics.Debug.WriteLine($"DeleteTaskAsync: Response Status Code {response.StatusCode}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<CleaningTask>> GetTasksByUserIdAsync(int userId)
        {
            var apiUrl = $"{_apiConfigService.BaseUrl}/cleaningtask/user/{userId}";
            System.Diagnostics.Debug.WriteLine($"GetTasksByUserIdAsync: Requesting {apiUrl}");

            var response = await _apiConfigService.HttpClient.GetAsync(apiUrl);
            System.Diagnostics.Debug.WriteLine($"GetTasksByUserIdAsync: Response Status Code {response.StatusCode}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"GetTasksByUserIdAsync JSON: {json}");
            return JsonSerializer.Deserialize<IEnumerable<CleaningTask>>(json);
        }
    }
}
