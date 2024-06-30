using Microsoft.Extensions.Logging;
using SweepSenseApp.Services;
using SweepSenseApp.ViewModels;
using SweepSenseApp.Pages;

namespace SweepSenseApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Services
            builder.Services.AddSingleton<ApiConfigService>();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<LoginService>();
            builder.Services.AddTransient<UserService>();
            builder.Services.AddSingleton<CleaningTaskService>();
            builder.Services.AddSingleton<ReportService>();
            builder.Services.AddSingleton<ImageService>();

            // ViewModels
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<BranchesViewModel>();
            builder.Services.AddTransient<ClientsViewModel>();
            builder.Services.AddTransient<LocationsViewModel>();
            builder.Services.AddTransient<NotificationsViewModel>();
            builder.Services.AddTransient<ProfilePageViewModel>();
            builder.Services.AddTransient<RegistrationViewModel>();
            builder.Services.AddTransient<ReportsViewModel>();
            builder.Services.AddTransient<SchedulesViewModel>();
            builder.Services.AddTransient<TaskDetailViewModel>();
            builder.Services.AddTransient<TaskViewModel>();
            builder.Services.AddTransient<UserReportsViewModel>();
            builder.Services.AddTransient<CreateReportViewModel>();

            // Pages
            builder.Services.AddTransient<BranchesPage>();
            builder.Services.AddTransient<ClientsPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<LocationsPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<NotificationsPage>();
            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<RegistrationPage>();
            builder.Services.AddTransient<ReportsPage>();
            builder.Services.AddTransient<SchedulesPage>();
            builder.Services.AddTransient<TaskDetailPage>();
            builder.Services.AddTransient<TaskPage>();
            builder.Services.AddTransient<UserReportsPage>();
            builder.Services.AddTransient<CreateReportPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
