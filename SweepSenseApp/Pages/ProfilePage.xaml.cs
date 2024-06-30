using SweepSenseApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace SweepSenseApp.Pages;

public partial class ProfilePage : ContentPage
{
    private readonly ProfilePageViewModel _viewModel;
    private readonly IServiceProvider _serviceProvider;

    public ProfilePage(ProfilePageViewModel viewModel, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _serviceProvider = serviceProvider;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Console.WriteLine("ProfilePage OnAppearing called");
        _viewModel.LoadUserDetails();
    }

    private async void OnViewReportsClicked(object sender, EventArgs e)
    {
        var userReportsViewModel = _serviceProvider.GetRequiredService<UserReportsViewModel>();
        var userReportsPage = new UserReportsPage(userReportsViewModel, _serviceProvider);
        await Navigation.PushAsync(userReportsPage);
    }

    private async void OnCreateReportClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CreateReportPage));
    }
}

