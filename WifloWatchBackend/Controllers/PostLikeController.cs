using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WifloWatchBackend.Data;
using WifloWatchBackend.Models;

[Route("api/post-like")]
[ApiController]
public class LikeController : ControllerBase
{
    private readonly WifloWatchDbContext _context; // DbContext’inizi buraya ekleyin

    public LikeController(WifloWatchDbContext context)
    {
        _context = context;
    }

    [HttpPost("new")]
    public async Task<IActionResult> AddPostLike([FromBody] PostLikeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Kullanıcı bu gönderiyi daha önce beğenmiş mi kontrolü
        var existingLike = await _context.PostLikes
            .FirstOrDefaultAsync(pl => pl.PostId == dto.PostId && pl.UserId == dto.UserId);

        if (existingLike != null)
        {
            // Eğer varsa beğeniyi geri al (unlike)
            _context.PostLikes.Remove(existingLike);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Beğeni kaldırıldı", liked = false });
        }

        // Yoksa yeni beğeni ekle
        var like = new PostLike
        {
            PostId = dto.PostId,
            UserId = dto.UserId,
            LikedAt = dto.LikedAt
        };

        _context.PostLikes.Add(like);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Beğeni eklendi", liked = true });
    }



    [HttpGet("GetLikesByPostId/{postId}")]
    public IActionResult GetTotalLikes(int postId)
    {
        var totalLikes = _context.PostLikes
            .Where(like => like.PostId == postId)  // Burada userId değil postId ile filtrele
            .Count();
        return Ok(new { totalLikes });
    }


    [HttpGet("GetLikeStatus/{postId}/{userId}")]
    public async Task<IActionResult> GetLikeStatus(int postId, int userId)
    {
        var like = await _context.PostLikes
            .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);

        return Ok(new { liked = like != null });
    }



}
