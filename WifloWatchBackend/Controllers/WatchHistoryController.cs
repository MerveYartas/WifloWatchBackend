using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WifloWatchBackend.Data;
using WifloWatchBackend.Models;

[Route("api/[controller]")]
[ApiController]
public class WatchHistoryController : ControllerBase
{
    private readonly WifloWatchDbContext _context;

    public WatchHistoryController(WifloWatchDbContext context)
    {
        _context = context;
    }

    
    [HttpPost("add")]
    public async Task<IActionResult> AddWatchHistory([FromBody] WatchHistory watchHistory)
    {
        if (watchHistory == null)
            return BadRequest("Geçersiz veri.");

        _context.WatchHistories.Add(watchHistory); 
        await _context.SaveChangesAsync();

        return Ok(new { message = "İzleme geçmişine eklendi." });
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetUserWatchHistory(int userId)
    {
        var history = _context.WatchHistories
            .Where(w => w.UserId == userId)
            .Select(w => new
            {
                w.Movie.Title,
                w.Movie.Genre,
                w.WatchedAt
            })
            .ToList();

        return Ok(history);
    }
}
