using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweepSenseApp.Models;
using SweepSenseApp.Services;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Media;

namespace SweepSenseApp.ViewModels
{
    public partial class CreateReportViewModel : BaseViewModel
    {
        private readonly ReportService _reportService;
        private readonly UserService _userService;
        private readonly ImageService _imageService;

        public CreateReportViewModel(ReportService reportService, UserService userService, ImageService imageService)
        {
            _reportService = reportService;
            _userService = userService;
            _imageService = imageService;
            SaveReportCommand = new AsyncRelayCommand(SaveReportAsync);
            SelectImageCommand = new AsyncRelayCommand(SelectImageAsync);
            CaptureImageCommand = new AsyncRelayCommand(CaptureImageAsync);
            Report = new Report();
        }

        [ObservableProperty]
        private Report report;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private string successMessage;

        [ObservableProperty]
        private ImageSource previewImage;

        public IAsyncRelayCommand SaveReportCommand { get; }
        public IAsyncRelayCommand SelectImageCommand { get; }
        public IAsyncRelayCommand CaptureImageCommand { get; }

        private async Task SelectImageAsync()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync();
                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    var filePath = await _imageService.SaveImageAsync(stream, result.FileName);
                    Report.Image = filePath;
                    PreviewImage = ImageSource.FromFile(filePath);
                    Debug.WriteLine($"Image selected and saved: {filePath}");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred while selecting the image: {ex.Message}";
                Debug.WriteLine(ErrorMessage);
            }
        }

        private async Task CaptureImageAsync()
        {
            try
            {
                var result = await MediaPicker.CapturePhotoAsync();
                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    var filePath = await _imageService.SaveImageAsync(stream, result.FileName);
                    Report.Image = filePath;
                    PreviewImage = ImageSource.FromFile(filePath);
                    Debug.WriteLine($"Image captured and saved: {filePath}");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred while capturing the image: {ex.Message}";
                Debug.WriteLine(ErrorMessage);
            }
        }

        private async Task SaveReportAsync()
        {
            try
            {
                var user = await _userService.GetUserDetailsAsync();
                Report.UserId = user.Id;
                Report.Date = DateTime.Now;
                await _reportService.CreateReportAsync(Report);
                ErrorMessage = string.Empty;
                SuccessMessage = "Report successfully created!";
                Debug.WriteLine(SuccessMessage);

                // Reset the fields
                Report = new Report();
                PreviewImage = null;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred while saving the report: {ex.Message}";
                SuccessMessage = string.Empty;
                Debug.WriteLine(ErrorMessage);
            }
        }
    }
}
