using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SweepSenseApp.Services
{
    public class UserService
    {
        private readonly ApiService _apiService;

        public User CurrentUser { get; private set; }

        public UserService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task LoadUserDataAsync(string userId)
        {
            var response = await _apiService.GetAsync($"Users/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                CurrentUser = JsonSerializer.Deserialize<User>(json);
            }
        }
    }

    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
