using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var adminUser = _configuration.GetSection("AuthUsers:Admin");
        var viewerUser = _configuration.GetSection("AuthUsers:Viewer");

        string? role = null;

        if (request.Username == adminUser["Username"] && request.Password == adminUser["Password"])
        {
            role = "Admin";
        }
        else if (request.Username == viewerUser["Username"] && request.Password == viewerUser["Password"])
        {
            role = "Viewer";
        }

        if (role == null)
            return Unauthorized("Invalid credentials");

        var token = GenerateJwtToken(request.Username, role);
        return Ok(new { token, role });
    }

    private string GenerateJwtToken(string username, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public record LoginRequest(string Username, string Password);
