using SweepSenseApp.Services;
namespace SweepSenseApp.Pages;

public partial class LoginPage : ContentPage
{
    private readonly LoginService _loginService;

    public LoginPage()
    {
        InitializeComponent();
        var apiConfigService = new ApiConfigService(); 
        _loginService = new LoginService(apiConfigService); 
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text;
        var password = PasswordEntry.Text;

        var token = await _loginService.LoginAsync(username, password);

        if (token != null)
        {
            MessageLabel.Text = "Login successful!";
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            MessageLabel.Text = "Login failed!";
        }
    }
}
