using SweepSenseApp.ViewModels;

namespace SweepSenseApp.Pages
{
    public partial class SchedulesPage : ContentPage
    {
        private readonly SchedulesViewModel _viewModel;

        public SchedulesPage(SchedulesViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.RefreshDataAsync();
        }
    }
}
