using SweepSenseApp.Pages;
namespace SweepSenseApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CreateReportPage), typeof(CreateReportPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(BranchesPage), typeof(BranchesPage));
            Routing.RegisterRoute(nameof(ClientsPage), typeof(ClientsPage));
            Routing.RegisterRoute(nameof(LocationsPage), typeof(LocationsPage));
            Routing.RegisterRoute(nameof(NotificationsPage), typeof(NotificationsPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(ReportsPage), typeof(ReportsPage));
            Routing.RegisterRoute(nameof(SchedulesPage), typeof(SchedulesPage));
            Routing.RegisterRoute(nameof(TaskDetailPage), typeof(TaskDetailPage));
            Routing.RegisterRoute(nameof(TaskPage), typeof(TaskPage));
            Routing.RegisterRoute(nameof(UserReportsPage), typeof(UserReportsPage));
        }

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);
           
        }
    }
}
