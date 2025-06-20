using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WifloWatchBackend.Data;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly WifloWatchDbContext _context;

    public UsersController(WifloWatchDbContext context)
    {
        _context = context;
    }

    // Kullanıcı ID ile arama (mevcut)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound(new { message = "Kullanıcı bulunamadı." });

        return Ok(new { user.Id, user.Username, user.Email });
    }

    // Kullanıcıları sayfalı şekilde listelemek (mevcut)
    [HttpGet]
    public async Task<IActionResult> GetUsers(int page = 1, int pageSize = 10)
    {
        var users = await _context.Users
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(u => new { u.Id, u.Username })
            .ToListAsync();

        if (!users.Any())
            return NotFound(new { message = "Kullanıcı bulunamadı." });

        return Ok(users);
    }

    // Yeni bir kullanıcı oluşturma (mevcut)
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User newUser)
    {
        if (newUser == null)
            return BadRequest(new { message = "Geçersiz kullanıcı verisi." });

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
    }

    // Kullanıcıyı güncelleme (mevcut)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound(new { message = "Kullanıcı bulunamadı." });

        user.Username = updatedUser.Username;
        user.Email = updatedUser.Email;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // Kullanıcı adı ile arama
    [HttpGet("searchByUsername")]
    public async Task<IActionResult> SearchByUsername([FromQuery] string username)
    {
        if (string.IsNullOrEmpty(username))
            return BadRequest(new { message = "Kullanıcı adı boş olamaz." });

        // Kullanıcı adıyla arama (küçük harf duyarsız)
        var users = await _context.Users
            .Where(u => u.Username.Contains(username, StringComparison.OrdinalIgnoreCase))
            .Select(u => new { u.Id, u.Username, u.Email })
            .ToListAsync();

        if (!users.Any())
            return NotFound(new { message = "Hiçbir kullanıcı bulunamadı." });

        return Ok(users);
    }
}
