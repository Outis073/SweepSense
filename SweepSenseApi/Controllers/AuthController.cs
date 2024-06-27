using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SweepSenseApi.Models;
using SweepSenseApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SweepSenseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService userService, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _userService = userService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            _logger.LogInformation($"Login poging voor gebruiker: {loginModel.Username}");
            var user = await _userService.AuthenticateAsync(loginModel.Username, loginModel.Password);

            if (user == null)
            {
                _logger.LogWarning($"Authentication mislukt voor gebruiker: {loginModel.Username}");
                return Unauthorized();
            }

            var token = GenerateJwtToken(user);
            _logger.LogInformation($"Authentication gelukt voor gebruiker: {loginModel.Username}");
            return Ok(new { token });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Role, user.Role?.Name ?? "User"),
        new Claim("name", user.Name) // Toevoegen van de naam van de gebruiker als een extra claim
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(7);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
