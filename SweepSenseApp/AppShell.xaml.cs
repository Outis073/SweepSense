using SweepSenseApp.Pages;
namespace SweepSenseApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(Pages.MainPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(Pages.LoginPage));
        }
    }
}
