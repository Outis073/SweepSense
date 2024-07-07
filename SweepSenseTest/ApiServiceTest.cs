using Moq;
using RichardSzalay.MockHttp;
using SweepSenseApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SweepSenseTest
{
    public class ApiServiceTests
    {
        private readonly ApiService _apiService;
        private readonly MockHttpMessageHandler _mockHttpMessageHandler;
        private readonly Mock<ISecureStorageService> _mockSecureStorageService;
        private readonly ApiConfigService _apiConfigService;

        public ApiServiceTests()
        {
            _mockHttpMessageHandler = new MockHttpMessageHandler();
            _mockSecureStorageService = new Mock<ISecureStorageService>();
            var httpClient = new HttpClient(_mockHttpMessageHandler)
            {
                BaseAddress = new System.Uri("https://localhost:7210/api")
            };

            _apiConfigService = new ApiConfigService(httpClient, "https://localhost:7210/api");
            _mockSecureStorageService.Setup(s => s.GetAsync(It.IsAny<string>())).ReturnsAsync("dummy_token");
            _apiService = new ApiService(_apiConfigService, _mockSecureStorageService.Object);
        }

        [Fact]
        public async Task GetAsync_ReturnsSuccessStatusCode()
        {
            // Arrange
            _mockHttpMessageHandler.When("https://localhost:7210/api/test")
                                   .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var response = await _apiService.GetAsync("test");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
