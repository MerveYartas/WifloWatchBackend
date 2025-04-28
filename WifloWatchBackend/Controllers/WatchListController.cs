using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WifloWatchBackend.Data;
using WifloWatchBackend.Models;

[Route("api/[controller]")]
[ApiController]
public class WatchListController : ControllerBase
{
    private readonly WifloWatchDbContext _context;

    public WatchListController(WifloWatchDbContext context)
    {
        _context = context;
    }

    [HttpPost("add")]
    public IActionResult AddToWatchList([FromBody] WatchList watchList)
    {
        if (watchList == null || watchList.UserId == 0 || watchList.MovieId == 0)
        {
            return BadRequest("Geçersiz veri.");
        }

        var existingRecord = _context.WatchLists
            .FirstOrDefault(w => w.UserId == watchList.UserId && w.MovieId == watchList.MovieId);

        if (existingRecord != null)
        {
            return BadRequest("Bu film zaten izleme listesinde.");
        }

        _context.WatchLists.Add(watchList);
        _context.SaveChanges();

        return Ok("Film izleme listesine eklendi.");
    }

   
    [HttpGet("{userId}")]
    public IActionResult GetWatchList(int userId)
    {
        var watchList = _context.WatchLists
            .Where(w => w.UserId == userId)
            .Select(w => new
            {
                w.Movie.Id,
                w.Movie.Title,
                w.Movie.PosterUrl
            })
            .ToList();

        return Ok(watchList);
    }

  
    [HttpDelete("remove/{userId}/{movieId}")]
    public IActionResult RemoveFromWatchList(int userId, int movieId)
    {
        var watchListItem = _context.WatchLists
            .FirstOrDefault(w => w.UserId == userId && w.MovieId == movieId);

        if (watchListItem == null)
        {
            return NotFound("Film izleme listesinde bulunamadı.");
        }

        _context.WatchLists.Remove(watchListItem);
        _context.SaveChanges();

        return Ok("Film izleme listesinden kaldırıldı.");
    }
}
