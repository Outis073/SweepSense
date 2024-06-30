using SweepSenseApp.Pages;
namespace SweepSenseApp
{
    public partial class App : Application
    {
        public static string BaseImagePath { get; private set; }

        public App()
        {
            InitializeComponent();

            BaseImagePath = DeviceInfo.Platform == DevicePlatform.WinUI
            ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Resources", "Images", "Uploads")
            : "Resources/Images/Uploads/";

            MainPage = new NavigationPage(
            ActivatorUtilities.CreateInstance<LoginPage>(
            MauiProgram.CreateMauiApp().Services));
        }
    }
}
