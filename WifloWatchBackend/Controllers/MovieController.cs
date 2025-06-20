using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WifloWatchBackend.Data;


namespace WifloWatchBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Token kontrolü için
    public class MoviesController : ControllerBase
    {
        private readonly WifloWatchDbContext _context;

        public MoviesController(WifloWatchDbContext context)
        {
            _context = context;
        }

        // GET: api/movies
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            try
            {
                var movies = await _context.Movies
                    .Select(m => new
                    {
                        m.Id,
                        m.Title,
                        m.Genre,
                        m.Description,
                        m.Duration,
                        m.Poster
                    })
                    .ToListAsync();

                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }
    }
}
