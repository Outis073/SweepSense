using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweepSenseApp.Models;
using SweepSenseApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SweepSenseApp.ViewModels
{
    public partial class UserReportsViewModel : BaseViewModel
    {
        private readonly ReportService _reportService;
        private readonly UserService _userService;

        public UserReportsViewModel(ReportService reportService, UserService userService)
        {
            _reportService = reportService;
            _userService = userService;
            LoadReportsCommand = new AsyncRelayCommand(LoadReportsAsync);
            DeleteReportCommand = new AsyncRelayCommand<Report>(DeleteReportAsync);
            LoadUserDetails();
        }

        [ObservableProperty]
        private ObservableCollection<Report> reports;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private string username;

        private int _userId;

        public IAsyncRelayCommand LoadReportsCommand { get; }
        public IAsyncRelayCommand<Report> DeleteReportCommand { get; }

        public async Task LoadReportsAsync()
        {
            try
            {
                ErrorMessage = string.Empty;
                if (_userId == 0)
                {
                    await LoadUserDetailsAsync();
                }
                var reports = await _reportService.GetReportsByUserAsync(_userId.ToString());
                Reports = new ObservableCollection<Report>(reports);
                ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred while loading reports: {ex.Message}";
            }
        }

        private async Task LoadUserDetailsAsync()
        {
            try
            {
                var user = await _userService.GetUserDetailsAsync();
                _userId = user.Id;
                Username = user.Username;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred while loading user details: {ex.Message}";
            }
        }

        private void LoadUserDetails()
        {
            _ = LoadUserDetailsAsync();
        }

        private async Task DeleteReportAsync(Report report)
        {
            try
            {
                if (report != null)
                {
                    await _reportService.DeleteReportAsync(report.Id);
                    Reports.Remove(report);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred while deleting the report: {ex.Message}";
            }
        }

    }
}
