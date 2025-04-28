using Microsoft.AspNetCore.Mvc;
using WifloWatchBackend.Models;
using WifloWatchBackend.Data; // DbContext'in bulunduğu namespace
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WifloWatchBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly WifloWatchDbContext _context;

        public PostController(WifloWatchDbContext context)
        {
            _context = context;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetUserPosts(int userId)
        {
            try
            {
                var userPosts = await _context.Posts
                    .Where(p => p.UserId == userId)
                    .Include(p => p.User)
                    .ToListAsync();

                if (!userPosts.Any())
                {
                    return NotFound("Bu kullanıcıya ait paylaşım bulunamadı.");
                }

                return Ok(userPosts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // Yeni bir paylaşım eklemek için POST isteği
        [HttpPost("post")]  // This handles POST requests
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            if (post == null)
            {
                return BadRequest("Geçersiz Veri.");
            }

            // Kullanıcının ID’sini session veya token'dan alıyorsan bu şekilde çekebilirsin
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
            {
                return Unauthorized("Kullanıcı kimliği bulunamadı.");
            }

            int userId = int.Parse(userIdClaim.Value);
            post.UserId = userId; // Kullanıcı ID'sini manuel olarak ekleme ihtiyacını kaldırıyoruz

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

         
            return Ok(post); 
        }

    }
}
