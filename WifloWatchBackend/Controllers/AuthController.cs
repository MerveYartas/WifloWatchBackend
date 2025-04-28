using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using BCrypt.Net;
using WifloWatchBackend.Data;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly WifloWatchDbContext _context;


    public AuthController(WifloWatchDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(User model)
    {
     
        if (_context.Users.Any(u => u.Email == model.Email))
        {
            return BadRequest(new { message = "Bu e-posta adresi zaten kullanılıyor." });
        }   

        // Şifre hashleme
        model.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.PasswordHash); 

        _context.Users.Add(model);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Kullanıcı başarıyla kaydedildi." });
    }
}