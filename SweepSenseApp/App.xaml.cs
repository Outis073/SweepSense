using SweepSenseApp.Pages;
namespace SweepSenseApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(
            ActivatorUtilities.CreateInstance<LoginPage>(
            MauiProgram.CreateMauiApp().Services));
        }
    }
}
