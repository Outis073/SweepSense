using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweepSenseApp.Models;
using SweepSenseApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SweepSenseApp.ViewModels
{
    public partial class SchedulesViewModel : BaseViewModel
    {
        private readonly UserService _userService;
        private readonly ICleaningTaskService _cleaningTaskService;

        public ObservableCollection<CleaningTask> CleaningTasks { get; } = new ObservableCollection<CleaningTask>();

        public SchedulesViewModel(UserService userService, ICleaningTaskService cleaningTaskService)
        {
            _userService = userService;
            _cleaningTaskService = cleaningTaskService;
            LoadUserDetailsAsync();
            RefreshCommand = new AsyncRelayCommand(RefreshDataAsync);
        }

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string errorMessage;

        private int _userId;

        private async Task LoadUserDetailsAsync()
        {
            try
            {
                var user = await _userService.GetUserDetailsAsync();
                _userId = user.Id;
                Username = user.Username;
                await LoadScheduleAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Er is een fout opgetreden bij het laden van gebruikersdetails: {ex.Message}";
            }
        }

        public async Task LoadScheduleAsync()
        {
            try
            {
                var tasks = await _cleaningTaskService.GetTasksByUserIdAsync(_userId);
                CleaningTasks.Clear();
                foreach (var task in tasks)
                {
                    CleaningTasks.Add(task);
                }
                SortTasks();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Er is een fout opgetreden bij het laden van de planning: {ex.Message}";
            }
        }

        private ICommand _markTaskAsCompleteCommand;
        public ICommand MarkTaskAsCompleteCommand => _markTaskAsCompleteCommand ??= new Command<CleaningTask>(async (task) => await MarkTaskAsCompleteAsync(task));

        private async Task MarkTaskAsCompleteAsync(CleaningTask task)
        {
            try
            {
                task.IsCompleted = true;
                await _cleaningTaskService.UpdateTaskAsync(task);
                await LoadScheduleAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Er is een fout opgetreden bij het markeren van de taak als voltooid: {ex.Message}";
            }
        }

        public async Task RefreshDataAsync()
        {
            await LoadUserDetailsAsync();
        }

        public void SortTasks()
        {
            var sortedTasks = CleaningTasks.OrderBy(task => task.IsCompleted)
                                           .ThenBy(task => task.ScheduledDate)
                                           .ToList();

            CleaningTasks.Clear();

            foreach (var task in sortedTasks)
            {
                CleaningTasks.Add(task);
            }
        }

        public IAsyncRelayCommand RefreshCommand { get; }
    }
}
