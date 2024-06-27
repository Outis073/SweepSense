using SweepSenseApp.Services;
using SweepSenseApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace SweepSenseApp.ViewModels
{
    public class ProfilePageViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService;
        private User _user;

        public ProfilePageViewModel(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            LoadUserDetails();
        }

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private async void LoadUserDetails()
        {
            try
            {
                User = await _userService.GetUserDetailsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading user details: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
