using SweepSenseApp.ViewModels;

namespace SweepSenseApp.Pages;

public partial class CreateReportPage : ContentPage
{
    public CreateReportPage(CreateReportViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}