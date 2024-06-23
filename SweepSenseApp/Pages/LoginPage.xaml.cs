using SweepSenseApp.Services;
using SweepSenseApp.ViewModels;

namespace SweepSenseApp.Pages
{
    public partial class LoginPage : ContentPage
    {
        private readonly LoginService _loginService;
        private readonly UserService _userService;

        public LoginPage(LoginService loginService, UserService userService)
        {
            InitializeComponent();
            _loginService = loginService;
            _userService = userService;
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            var userId = await _loginService.LoginAsync(username, password);

            if (userId != null)
            {
                MessageLabel.Text = "Login successful!";
                await _userService.LoadUserDataAsync(userId);
                var homePageViewModel = new HomeViewModel(_userService);
                await Navigation.PushAsync(new HomePage(homePageViewModel));
            }
            else
            {
                MessageLabel.Text = "Login failed!";
            }
        }
    }
}
