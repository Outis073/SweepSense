using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SweepSenseApp.Services;

namespace SweepSenseApp.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService;

        public HomeViewModel(UserService userService)
        {
            _userService = userService;
            LoadUserData();
        }

        private void LoadUserData()
        {
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(WelcomeMessage));
        }

        public string Username => _userService.CurrentUser?.Username;

        public string WelcomeMessage => $"Welcome, {Username}";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
