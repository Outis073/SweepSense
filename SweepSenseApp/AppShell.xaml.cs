using SweepSenseApp.Pages;
using SweepSenseApp.Services;
namespace SweepSenseApp
{
    public partial class AppShell : Shell
    {
        private readonly UserService _userService;
        public AppShell(UserService userService)
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

            _userService = userService;
            ManageShellItems();
        }

        private async void ManageShellItems()
        {
            var user = await _userService.GetUserDetailsAsync();
            if (user.Role == "Cleaner")
            {
                var schedulesPage = new FlyoutItem
                {
                    Title = "Schoonmaakrooster",
                    Items =
                    {
                        new ShellContent
                        {
                            ContentTemplate = new DataTemplate(typeof(SchedulesPage)),
                            Route = "SchedulesPage"
                        }
                    }
                };
                Items.Add(schedulesPage);
            }
        }

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);
           
        }

    }
}
