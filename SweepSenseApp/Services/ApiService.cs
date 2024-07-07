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
        private readonly ISecureStorageService _secureStorageService;

        public ApiService(ApiConfigService apiConfigService, ISecureStorageService secureStorageService)
        {
            _apiConfigService = apiConfigService;
            _secureStorageService = secureStorageService;
        }

        private async Task AddAuthorizationHeaderAsync()
        {
            var token = await _secureStorageService.GetAsync("auth_token");

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
