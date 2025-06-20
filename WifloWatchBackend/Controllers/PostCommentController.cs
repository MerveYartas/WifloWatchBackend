using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WifloWatchBackend.Data;
using WifloWatchBackend.Models;


[Route("api")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly WifloWatchDbContext _context;

    public CommentController(WifloWatchDbContext context)
    {
        _context = context;
    }
    [HttpPost("new")]
    [Authorize]
    public async Task<IActionResult> AddComment([FromBody] PostCommentDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.CommentText))
            return BadRequest("Yorum boş olamaz.");

        var comment = new PostComment
        {
            PostId = dto.PostId,
            UserId = dto.UserId,
            CommentText = dto.CommentText,
            CreatedAt = DateTime.Now
        };

        _context.PostComments.Add(comment);
        await _context.SaveChangesAsync();

        return Ok(comment);
    }



    [HttpGet("PostComment/{postId}")]
    public async Task<IActionResult> GetComments(int postId)
    {
        var comments = await _context.PostComments
            .Where(c => c.PostId == postId)
            .Include(c => c.User)
            .Select(c => new
            {
                c.Id,
                c.CommentText,
                c.CreatedAt,
                c.UserId,
                username = c.User.Username
            })
            .ToListAsync();

        return Ok(comments);
    }

}
