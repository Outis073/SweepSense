using SweepSenseApp.Models;
using SweepSenseApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm;

namespace SweepSenseApp.ViewModels
{
    public class UserReportsViewModel : INotifyPropertyChanged
    {
        private readonly ReportService _reportService;
        private readonly UserService _userService;
        private ObservableCollection<Report> _reports;
        private string _errorMessage;
        private string _username;
        private int _userId;

        public UserReportsViewModel(ReportService reportService, UserService userService)
        {
            _reportService = reportService;
            _userService = userService;
            LoadReportsCommand = new Command(async () => await LoadReportsAsync());
            LoadUserDetails();
        }

        public ObservableCollection<Report> Reports
        {
            get => _reports;
            set
            {
                _reports = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public Command LoadReportsCommand { get; }
        public async Task LoadReportsAsync()
        {
            try
            {
                ErrorMessage = string.Empty;
                if (_userId == 0)
                {
                    
                    await LoadUserDetailsAsync();
                }
                System.Diagnostics.Debug.WriteLine($"Loading reports for user ID: {_userId}");
                var reports = await _reportService.GetReportsByUserAsync(_userId.ToString());
                Reports = new ObservableCollection<Report>(reports);
                ErrorMessage = string.Empty;
                System.Diagnostics.Debug.WriteLine($"Reports loaded: {Reports.Count}");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred while loading reports: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"LoadReportsAsync Exception: {ex.Message}");
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadUserDetails()
        {
            _ = LoadUserDetailsAsync();
        }
    }
}