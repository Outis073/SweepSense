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
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            try
            {
                var loginModel = new
                {
                    Username = username,
                    Password = password
                };

                var json = JsonSerializer.Serialize(loginModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                // emulator of local
                var apiUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5276/api/Auth/login" : "https://localhost:7210/api/Auth/login";


                if (string.IsNullOrEmpty(apiUrl))
                {
                    return null;
                }

                var response = await _httpClient.PostAsync(apiUrl, content);

                if (response != null && response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);
                    await SecureStorage.SetAsync("auth_token", tokenResponse.Token);
                    return tokenResponse.Token;
                }
                else if (response != null)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HttpResponseMessage> GetSecureDataAsync()
        {
            var token = await SecureStorage.GetAsync("auth_token");

            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("No token found");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5276/api/SecureEndpoint" : "https://localhost:7210/api/SecureEndpoint";

            return await _httpClient.GetAsync(apiUrl);
        }
    }

    public class TokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}