using SweepSenseApp.Services;
using Moq;
using RichardSzalay.MockHttp;
using SweepSenseApp.Models;
using System.Net;
using System.Text.Json;

namespace SweepSenseTest
{
    public class ReportServiceTests
    {
        private readonly ReportService _reportService;
        private readonly MockHttpMessageHandler _mockHttpMessageHandler;
        private readonly ApiConfigService _apiConfigService;

        public ReportServiceTests()
        {
            _mockHttpMessageHandler = new MockHttpMessageHandler();
            var httpClient = new HttpClient(_mockHttpMessageHandler)
            {
                BaseAddress = new System.Uri("https://localhost:7210/api")
            };

            _apiConfigService = new ApiConfigService(httpClient, "https://localhost:7210/api");
            _reportService = new ReportService(_apiConfigService);
        }

        [Fact]
        public async Task CreateReportAsync_Successful()
        {
            // Arrange
            var report = new Report
            {
                Id = 1,
                UserId = 123,
                RoomId = "RoomA",
                Description = "Test Description",
                Image = "test.jpg",
                Date = DateTime.Now
            };
            _mockHttpMessageHandler.When("https://localhost:7210/api/report")
                                   .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            await _reportService.CreateReportAsync(report);

            // Assert
            // Geen exception = success
        }

        [Fact]
        public async Task GetReportsByUserAsync_ReturnsReports()
        {
            // Arrange
            var reports = new List<Report>
            {
                new Report
                {
                    Id = 1,
                    UserId = 123,
                    RoomId = "RoomA",
                    Description = "Test Description",
                    Image = "test.jpg",
                    Date = DateTime.Now
                }
            };
            _mockHttpMessageHandler.When("https://localhost:7210/api/report/user/123")
                                   .Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(reports));

            // Act
            var result = await _reportService.GetReportsByUserAsync("123");

            // Assert
            Assert.NotEmpty(result);
            var firstReport = result.First();
            Assert.Equal(123, firstReport.UserId);
            Assert.Equal("RoomA", firstReport.RoomId);
            Assert.Equal("Test Description", firstReport.Description);
        }

        [Fact]
        public async Task DeleteReportAsync_Successful()
        {
            // Arrange
            _mockHttpMessageHandler.When("https://localhost:7210/api/report/1")
                                   .Respond(HttpStatusCode.OK);

            // Act
            await _reportService.DeleteReportAsync(1);

            // Assert
            // Geen exception = success
        }
    }
}
