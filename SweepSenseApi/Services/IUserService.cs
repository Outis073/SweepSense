﻿using SweepSenseApi.Models;

namespace SweepSenseApi.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<User> AuthenticateAsync(string username, string password);
    }
}
