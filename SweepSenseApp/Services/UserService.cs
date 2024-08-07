﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SweepSenseApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;

namespace SweepSenseApp.Services
{
    public class UserService
    {
        private readonly ApiConfigService _apiConfigService;
        private readonly ISecureStorageService _secureStorageService;

        public UserService(ApiConfigService apiConfigService, ISecureStorageService secureStorageService)
        {
            _apiConfigService = apiConfigService;
            _secureStorageService = secureStorageService;
        }

        public async Task<User> GetUserDetailsAsync()
        {
            try
            {
                var token = await _secureStorageService.GetAsync("auth_token");
                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("No token found");
                }

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var userId = int.Parse(jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
                var username = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
                var name = jwtToken.Claims.First(claim => claim.Type == "name").Value;
                var role = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;

                var user = new User
                {
                    Id = userId,
                    Username = username,
                    Name = name,
                    Role = role
                };

                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<CleaningTask>> GetCleaningTasksByUserIdAsync(int userId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{_apiConfigService.BaseUrl}/cleaningtask/user/{userId}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _secureStorageService.GetAsync("auth_token"));

                var response = await _apiConfigService.HttpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ICollection<CleaningTask>>(json);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Failed to fetch cleaning tasks: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
