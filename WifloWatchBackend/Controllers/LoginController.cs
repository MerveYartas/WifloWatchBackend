using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using WifloWatchBackend.Data;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using WifloWatchBackend.Models;
using Microsoft.Extensions.Configuration;

[ApiController]
[Route("api/auth")]
public class LoginController : ControllerBase
{
    private readonly WifloWatchDbContext _context;
    private readonly string _secretKey;

    public LoginController(WifloWatchDbContext context, IConfiguration configuration)
    {
        _context = context;
        _secretKey = configuration["JwtSettings:SecretKey"]; // Doğru JSON yolu!

        if (string.IsNullOrEmpty(_secretKey) || _secretKey.Length < 32)
        {
            throw new ArgumentException("JWT Secret Key en az 32 karakter olmalıdır! Lütfen appsettings.json içinde güncelleyin.");
        }
    }


    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == model.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
        {
            return Unauthorized(new { message = "Geçersiz e-posta veya şifre" });
        }

        var token = GenerateJwtToken(user);

        return Ok(new
        {
            token,
            userId = user.Id,  // Kullanıcı ID'yi ekliyoruz
            email = user.Email,
            username = user.Username,
            role = user.Role
        });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("username", user.Username)  // Kullanıcı adını da token içine koyduk
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "WifloWatchAPI",
            audience: "WifloWatchClient",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1), // UTC kullanmalısın
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
