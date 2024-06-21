using SweepSenseApp.Services;
namespace SweepSenseApp.Pages;

public partial class LoginPage : ContentPage
{
    private readonly ApiService _apiService;

    public LoginPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text;
        var password = PasswordEntry.Text;

        var token = await _apiService.LoginAsync(username, password);

        if (token != null)
        {
            MessageLabel.Text = "Login successful!";
            // Navigate to main page or other secure page
           await Navigation.PushAsync(new MainPage());
        }
        else
        {
            MessageLabel.Text = "Login failed. Please check your credentials.";
        }
    }
}