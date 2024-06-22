using SweepSenseApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SweepSenseApp.Services
{
    public class ApiService
    {
        private readonly ApiConfigService _apiConfigService;

        public ApiService(ApiConfigService apiConfigService)
        {
            _apiConfigService = apiConfigService;
        }

        private async Task AddAuthorizationHeaderAsync()
        {
            var token = await SecureStorage.GetAsync("auth_token");

            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("No token found");
            }

            _apiConfigService.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            await AddAuthorizationHeaderAsync();
            var apiUrl = $"{_apiConfigService.BaseUrl}/{endpoint}";
            return await _apiConfigService.HttpClient.GetAsync(apiUrl);
        }

        public async Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent content)
        {
            await AddAuthorizationHeaderAsync();
            var apiUrl = $"{_apiConfigService.BaseUrl}/{endpoint}";
            return await _apiConfigService.HttpClient.PostAsync(apiUrl, content);
        }
    }
}
