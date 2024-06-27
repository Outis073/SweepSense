using SweepSenseApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace SweepSenseApp.Pages;

public partial class ProfilePage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public ProfilePage(ProfilePageViewModel viewModel, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _serviceProvider = serviceProvider;
    }

    private async void OnViewReportsClicked(object sender, EventArgs e)
    {
        var userReportsViewModel = _serviceProvider.GetRequiredService<UserReportsViewModel>();
        var userReportsPage = new UserReportsPage(userReportsViewModel, _serviceProvider);
        await Navigation.PushAsync(userReportsPage);
    }

    private async void OnCreateReportClicked(object sender, EventArgs e)
    {
        var createReportViewModel = _serviceProvider.GetRequiredService<CreateReportViewModel>();
        var createReportPage = new CreateReportPage(createReportViewModel);
        await Navigation.PushAsync(createReportPage);
    }
}

