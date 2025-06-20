// CommentController.cs
using Microsoft.AspNetCore.Mvc;
using WifloWatchBackend.Models;
using WifloWatchBackend.Data;
using System.Linq;

namespace WifloWatchBackend.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly WifloWatchDbContext _context;

        public CommentController(WifloWatchDbContext context)
        {
            _context = context;
        }

        [HttpPost("new")]
        public IActionResult AddComment([FromBody] Comment comment)
        {
            if (comment == null)
            {
                return BadRequest("Geçersiz yorum verisi.");
            }

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Ok(comment);
        }

        [HttpGet("{userId}")]
        public IActionResult GetComments(int contentId)
        {
            var comments = _context.Comments
                .Where(c => c.ContentId == contentId)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new
                {
                    c.CommentText,
                    c.CreatedAt,
                    UserName = c.User.Username
                })
                .ToList();

            return Ok(comments);
        }
    }
}
