using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WifloWatchBackend.Data;

[Route("api")]
[Authorize]
[ApiController]
public class UserController : ControllerBase
{
    private readonly WifloWatchDbContext _context;

    public UserController(WifloWatchDbContext context)
    {
        _context = context;
    }

    // GET api/user/{id}
    [HttpGet("user/bul/{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)   
        {
            return NotFound(new { message = "Kullanıcı bulunamadı." });
        }

        return Ok(user);
    }
}
