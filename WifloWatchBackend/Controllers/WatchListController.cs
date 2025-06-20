using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WifloWatchBackend.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using WifloWatchBackend.Data;

namespace WifloWatchBackend.Controllers
{
    [Route("api/watchlist")]
    [ApiController]
    public class WatchListController : ControllerBase
    {
        private readonly WifloWatchDbContext _context;

        public WatchListController(WifloWatchDbContext context)
        {
            _context = context;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMovies(string query)
        {
            var movies = await _context.Movies
                .Where(m => m.Title.Contains(query))
                .ToListAsync();
            return Ok(movies);
        }

        [HttpPost("{userId}/{movieId}")]
        public async Task<IActionResult> AddToWatchlist(int userId, int movieId)
        {
            try
            {
                var existingWatchlist = await _context.WatchLists
                    .FirstOrDefaultAsync(w => w.UserId == userId && w.MovieId == movieId);
                if (existingWatchlist != null)
                {
                    return Conflict(new { message = "Bu film zaten izleme listesinde." });
                }

                var movieExists = await _context.Movies.AnyAsync(m => m.Id == movieId);
                if (!movieExists)
                {
                    return BadRequest(new { message = "Geçersiz film ID'si." });
                }

                var watchlist = new WatchList
                {
                    UserId = userId,
                    MovieId = movieId,
                    IsWatched = false
                };
                _context.WatchLists.Add(watchlist);
                await _context.SaveChangesAsync();

                // Önerileri güncelle
                await UpdateSmartFriendRecommendations(userId);

                return Ok(new { message = "Film izleme listesine eklendi." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddToWatchlist Hatası: {ex.Message}");
                return StatusCode(500, new { message = "Film eklenirken bir hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWatchlist(int userId)
        {
            var watchlist = await _context.WatchLists
                .Where(w => w.UserId == userId)
                .Select(w => new { movieId = w.MovieId, name = w.Movie.Title })
                .ToListAsync();
            return Ok(watchlist);
        }

        [HttpDelete("{userId}/{movieId}")]
        public async Task<IActionResult> DeleteFromWatchlist(int userId, int movieId)
        {
            var watchlistItem = await _context.WatchLists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.MovieId == movieId);
            if (watchlistItem == null)
                return NotFound();
            _context.WatchLists.Remove(watchlistItem);
            await _context.SaveChangesAsync();

            // Önerileri güncelle
            await UpdateSmartFriendRecommendations(userId);

            return Ok(new { message = "Film izleme listesinden kaldırıldı." });
        }

        [HttpGet("friend-suggestions/{userId}")]
        public async Task<IActionResult> GetFriendSuggestions(int userId)
        {
            try
            {
                var suggestions = await _context.SmartFriendsList
                    .Where(s => s.UserId == userId)
                    .Select(s => new
                    {
                        userId = s.RecommendedUserId,
                        name = s.RecommendedUser.Username,
                        reason = s.Reason
                    })
                    .ToListAsync();
                Console.WriteLine($"Öneriler ({userId}): {suggestions.Count} bulundu.");
                return Ok(suggestions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetFriendSuggestions Hatası: {ex.Message}");
                return StatusCode(500, new { message = "Öneriler alınırken hata oluştu.", error = ex.Message });
            }
        }

        private async Task UpdateSmartFriendRecommendations(int userId)
        {
            try
            {
                Console.WriteLine($"Öneriler güncelleniyor: UserId={userId}");

                // Mevcut önerileri temizle
                var existingRecommendations = await _context.SmartFriendsList
                    .Where(s => s.UserId == userId)
                    .ToListAsync();
                _context.SmartFriendsList.RemoveRange(existingRecommendations);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Eski öneriler silindi: {existingRecommendations.Count}");

                // Kullanıcının izleme listesini al
                var userWatchlist = await _context.WatchLists
                    .Where(w => w.UserId == userId)
                    .Select(w => w.MovieId)
                    .ToListAsync();
                Console.WriteLine($"Kullanıcı izleme listesi: {userWatchlist.Count} film");

                // Diğer kullanıcıları al
                var otherUsers = await _context.Users
                    .Where(u => u.Id != userId)
                    .ToListAsync();
                Console.WriteLine($"Diğer kullanıcılar: {otherUsers.Count}");

                foreach (var otherUser in otherUsers)
                {
                    var otherWatchlist = await _context.WatchLists
                        .Where(w => w.UserId == otherUser.Id)
                        .Select(w => w.MovieId)
                        .ToListAsync();
                    Console.WriteLine($"Kullanıcı {otherUser.Id} izleme listesi: {otherWatchlist.Count} film");

                    // Ortak filmleri bul
                    var commonMovies = userWatchlist.Intersect(otherWatchlist).ToList();
                    Console.WriteLine($"Kullanıcı {userId} ile {otherUser.Id} arasında {commonMovies.Count} ortak film");

                    if (commonMovies.Count > 5)
                    {
                        var recommendation = new SmartFriendRecommendation
                        {
                            UserId = userId,
                            RecommendedUserId = otherUser.Id,
                            Reason = $"{commonMovies.Count} ortak film izleme listenizde bulunuyor.",
                            CreatedAt = DateTime.UtcNow
                        };
                        _context.SmartFriendsList.Add(recommendation);
                        Console.WriteLine($"Öneri eklendi: {otherUser.Id} için {commonMovies.Count} ortak film");
                    }
                }

                await _context.SaveChangesAsync();
                Console.WriteLine("Öneriler kaydedildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateSmartFriendRecommendations Hatası: {ex.Message}");
            }
        }
    }
}