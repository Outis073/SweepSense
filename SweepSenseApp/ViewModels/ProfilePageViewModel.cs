using SweepSenseApp.Services;
using SweepSenseApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SweepSenseApp.ViewModels
{
    public partial class ProfilePageViewModel : BaseViewModel
    {
        private readonly UserService _userService;

        public ProfilePageViewModel(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            LoadUserDetails();
        }

        [ObservableProperty]
        User user;

        public async void LoadUserDetails()
        {
            try
            {
                User = await _userService.GetUserDetailsAsync();
                Console.WriteLine($"User loaded: {User.Username}, {User.Name}, {User.Role}");
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
