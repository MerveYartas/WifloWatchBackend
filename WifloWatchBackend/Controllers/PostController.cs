using Microsoft.AspNetCore.Mvc;
using WifloWatchBackend.Models;
using WifloWatchBackend.Data; // DbContext'in bulunduğu namespace
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        [Authorize]
        public async Task<ActionResult<IEnumerable<Post>>> GetUserPosts(int userId)
        {
            try
            {
            //    // Token'dan kullanıcı ID'sini alalım
            //    var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //    if (userIdFromToken == null || userIdFromToken != userId.ToString())
            //    {
            //        return Unauthorized(new { message = "Geçersiz kullanıcı kimliği." });
            //    }

                // Kullanıcıya ait paylaşımları sorgulayalım
                var userPosts = await _context.Posts
                    .Where(p => p.UserId == userId)
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
        [HttpPost("post")]
        [Authorize]
        public async Task<IActionResult> CreatePost(Post post)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdFromToken == null || post.UserId != int.Parse(userIdFromToken))
            {
                return Unauthorized(new { message = "Geçersiz kullanıcı kimliği." });
            }

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }





    }
}
