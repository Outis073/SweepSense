using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SweepSenseApp.Services
{
    public class LoginService
    {
        private readonly ApiConfigService _apiConfigService;

        public LoginService(ApiConfigService apiConfigService)
        {
            _apiConfigService = apiConfigService;
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

                var apiUrl = $"{_apiConfigService.BaseUrl}/Auth/login";

                var response = await _apiConfigService.HttpClient.PostAsync(apiUrl, content);

                if (response != null && response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);
                    await SecureStorage.SetAsync("auth_token", tokenResponse.Token);

                    var userId = ExtractUserIdFromToken(tokenResponse.Token);
                    await SecureStorage.SetAsync("user_id", userId);
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

        private string ExtractUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
        }
    }

    public class TokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
