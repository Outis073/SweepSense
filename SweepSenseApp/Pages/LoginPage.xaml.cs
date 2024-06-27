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

            var token = await _loginService.LoginAsync(username, password);

            if (token != null)
            {
                MessageLabel.Text = "Login successful!";
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                MessageLabel.Text = "Login failed!";
            }
        }
    }
}
