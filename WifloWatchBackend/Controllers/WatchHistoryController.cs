using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WifloWatchBackend.Data;
using WifloWatchBackend.Models;

namespace WifloWatchBackend.Controllers
{
    [Route("api/watch-history")]
    [ApiController]
    public class WatchHistoryController : ControllerBase
    {
        private readonly WifloWatchDbContext _context;

        public WatchHistoryController(WifloWatchDbContext context)
        {
            _context = context;
        }

        // Kullanıcının izleme geçmişini getirme
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWatchHistory(int userId)
        {
            var watchHistory = await _context.WatchHistories
                .Where(w => w.UserId == userId)
                .Include(w => w.Movie) // Film bilgilerini de ekleyelim
                .ToListAsync();

            if (watchHistory == null || !watchHistory.Any())
            {
                return NotFound("İzleme geçmişi bulunamadı.");
            }

            return Ok(watchHistory);
        }


        // İzleme geçmişine film ekleme
        [HttpPost("add")]
        public async Task<IActionResult> AddToWatchHistory([FromBody] WatchHistoryDTO watchHistoryDTO)
        {
            if (watchHistoryDTO == null)
            {
                return BadRequest("Geçersiz veri.");
            }

            // Geçerli bir userId ve movieId kontrolü
            var userExists = await _context.Users.AnyAsync(u => u.Id == watchHistoryDTO.UserId);
            var movieExists = await _context.Movies.AnyAsync(m => m.Id == watchHistoryDTO.MovieId);

            if (!userExists || !movieExists)
            {
                return NotFound("Kullanıcı veya film bulunamadı.");
            }

            // DTO'yu kullanarak WatchHistory objesi oluştur
            var watchHistory = new WatchHistory
            {
                UserId = watchHistoryDTO.UserId,
                MovieId = watchHistoryDTO.MovieId,
                WatchedAt = DateTime.UtcNow
            };

            _context.WatchHistories.Add(watchHistory);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Film izleme geçmişine eklendi." });
        }

    }
}
