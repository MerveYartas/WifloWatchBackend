using Microsoft.AspNetCore.Mvc;
using WifloWatchBackend.Models;  // Ensure MessageDto is in this namespace
using WifloWatchBackend.Data;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WifloWatchBackend.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
        private readonly WifloWatchDbContext _context;

        public MessageController(WifloWatchDbContext context)
        {
            _context = context;
        }
        [HttpGet("AllMessage")]
        public async Task<IActionResult> GetAllMessages()
        {
            try
            {
                var messages = await _context.Messages
                    .Include(m => m.Sender)
                    .Include(m => m.Receiver)
                    .Select(m => new
                    {
                        m.Id,
                        Sender = new { m.Sender.Id, m.Sender.Username },
                        Receiver = new { m.Receiver.Id, m.Receiver.Username },
                        MessageText = m.MessageText,
                        SentAt = m.SentAt
                    })
                    .ToListAsync();

                return Ok(messages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }
        [HttpPost("gonder")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDTO messageDto)
        {
            try
            {
                if (messageDto == null)
                    return BadRequest("Mesaj verisi geçersiz.");

                var message = new Message
                {
                    SenderId = messageDto.SenderId,
                    ReceiverId = messageDto.ReceiverId,
                    MessageText = messageDto.MessageText,
                    SentAt = DateTime.UtcNow
                };

                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Sunucu hatası oluştu.");
            }
        }

        [HttpGet("between/{userId1}/{userId2}")]
        public async Task<IActionResult> GetMessagesBetween(int userId1, int userId2)
        {
            try
            {
                // İki kullanıcı arasındaki mesajları al, sıralama yap
                var messages = await _context.Messages
                    .Where(m =>
                        (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                        (m.SenderId == userId2 && m.ReceiverId == userId1))
                    .OrderBy(m => m.SentAt) // Gönderim sırasına göre sıralama
                    .ToListAsync();

                if (messages == null || !messages.Any()) // Eğer mesaj yoksa
                {
                    return NotFound("Hiçbir mesaj bulunamadı.");
                }

                // Mesajları başarılı bir şekilde döndür
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message }); // Hata durumunda mesajı döndür
            }
        }

        // Okunmamış mesajları almak için API
        [HttpGet("unread/{userId}")]
        public async Task<IActionResult> GetUnreadMessages(int userId)
        {
            var messages = await _context.Messages
                .Where(m => m.ReceiverId == userId && m.IsRead != true)
                .OrderByDescending(m => m.SentAt)
                .Select(m => new
                {
                    m.Id,
                    m.SenderId,
                    SenderName = m.Sender.Username, // Kullanıcı adı
                    m.MessageText,
                    m.SentAt
                })
                .ToListAsync();

            if (messages.Count == 0)
            {
                return NotFound("No unread messages found.");
            }

            return Ok(messages);
        }
        [HttpPut("markasread/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
                return NotFound();

            message.IsRead = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
