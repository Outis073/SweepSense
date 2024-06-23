using SweepSenseApp.ViewModels;

namespace SweepSenseApp.Pages;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}