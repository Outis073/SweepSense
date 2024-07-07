using Moq;
using RichardSzalay.MockHttp;
using SweepSenseApp.Services;
using SweepSenseApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net;
using System.Text.Json;


namespace SweepSenseTest
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly MockHttpMessageHandler _mockHttpMessageHandler;
        private readonly Mock<ISecureStorageService> _mockSecureStorageService;
        private readonly ApiConfigService _apiConfigService;

        public UserServiceTests()
        {
            _mockHttpMessageHandler = new MockHttpMessageHandler();
            _mockSecureStorageService = new Mock<ISecureStorageService>();
            var httpClient = new HttpClient(_mockHttpMessageHandler)
            {
                BaseAddress = new System.Uri("https://localhost:7210/api")
            };

            _apiConfigService = new ApiConfigService(httpClient, "https://localhost:7210/api");
            _userService = new UserService(_apiConfigService, _mockSecureStorageService.Object);
        }

        [Fact]
        public async Task GetUserDetailsAsync_ReturnsUser()
        {
            // Arrange
            var user = new User { Id = 1, Username = "testuser", Name = "Test User", Role = "Admin" };
            var jwtToken = new JwtSecurityToken(claims: new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "testuser"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim("name", "Test User"),
                new Claim(ClaimTypes.Role, "Admin")
            });
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            _mockSecureStorageService.Setup(s => s.GetAsync("auth_token")).ReturnsAsync(token);
            _mockHttpMessageHandler.When("https://localhost:7210/api/user/details")
                                   .Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(user));

            // Act
            var result = await _userService.GetUserDetailsAsync();

            // Assert
            Assert.Equal("testuser", result.Username);
            Assert.Equal("Test User", result.Name);
            Assert.Equal("Admin", result.Role);
        }

        [Fact]
        public async Task GetCleaningTasksByUserIdAsync_ReturnsTasks()
        {
            // Arrange
            var tasks = new List<CleaningTask>
            {
                new CleaningTask { Id = 1, Description = "Test Task" }
            };
            _mockSecureStorageService.Setup(s => s.GetAsync("auth_token")).ReturnsAsync("dummy_token");
            _mockHttpMessageHandler.When("https://localhost:7210/api/cleaningtask/user/1")
                                   .Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(tasks));

            // Act
            var result = await _userService.GetCleaningTasksByUserIdAsync(1);

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal("Test Task", result.First().Description);
        }
    }
}
