using SweepSenseApp.Models;
using SweepSenseApp.Services;
using System.Collections.ObjectModel;
using System.Text.Json;
using Microsoft.Maui.Controls;

namespace SweepSenseApp.Pages
{
    public partial class TaskPage : ContentPage
    {
        private readonly ApiService _apiService;
        public ObservableCollection<CleaningTask> Tasks { get; set; }

        public TaskPage()
        {
            InitializeComponent();
            var apiConfigService = new ApiConfigService();
            _apiService = new ApiService(apiConfigService);
            Tasks = new ObservableCollection<CleaningTask>();
            TaskListView.ItemsSource = Tasks;

            LoadTasks();
        }

        private async void LoadTasks()
        {
            var response = await _apiService.GetAsync("CleaningTask");
            if (response.IsSuccessStatusCode)
            {
                var tasksJson = await response.Content.ReadAsStringAsync();
                var tasks = JsonSerializer.Deserialize<List<CleaningTask>>(tasksJson);
                foreach (var task in tasks)
                {
                    Tasks.Add(task);
                }
            }
        }
    }
}
