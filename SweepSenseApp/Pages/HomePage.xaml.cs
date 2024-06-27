using SweepSenseApp.ViewModels;
using SweepSenseApp.Pages;

namespace SweepSenseApp.Pages;

public partial class HomePage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public HomePage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
    }
    private async void OnCreateReportClicked(object sender, EventArgs e)
    {
        var createReportViewModel = _serviceProvider.GetRequiredService<CreateReportViewModel>();
        var createReportPage = new CreateReportPage(createReportViewModel);
        await Navigation.PushAsync(createReportPage);
    }
}