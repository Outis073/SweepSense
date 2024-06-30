using SweepSenseApp.Models;
using SweepSenseApp.ViewModels;
using Microsoft.Maui.Controls;

namespace SweepSenseApp.Pages;

public partial class UserReportsPage : ContentPage
{
    private readonly UserReportsViewModel _viewModel;
    private readonly IServiceProvider _serviceProvider;

    public UserReportsPage(UserReportsViewModel viewModel, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _serviceProvider = serviceProvider;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadReportsAsync();
    }

    private async void OnCreateReportClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CreateReportPage));
    }
}
