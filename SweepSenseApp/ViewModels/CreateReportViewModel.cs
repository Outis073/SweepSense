using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SweepSenseApp.Models;
using SweepSenseApp.Services;

namespace SweepSenseApp.ViewModels
{
    public class CreateReportViewModel : INotifyPropertyChanged
    {
        private readonly ReportService _reportService;
        private readonly UserService _userService;
        private Report _report;
        private string _errorMessage;

        public CreateReportViewModel(ReportService reportService, UserService userService)
        {
            _reportService = reportService;
            _userService = userService;
            SaveReportCommand = new Command(async () => await SaveReportAsync());
            Report = new Report();
        }

        public Report Report
        {
            get => _report;
            set
            {
                _report = value;
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

        public ICommand SaveReportCommand { get; }

        private async Task SaveReportAsync()
        {
            try
            {
                var user = await _userService.GetUserDetailsAsync();
                Report.UserId = user.Id;
                await _reportService.CreateReportAsync(Report);
                ErrorMessage = string.Empty;
               
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred while saving the report: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
