using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WifloWatchBackend.Models; // Message sınıfının bulunduğu namespace
using System;
using System.Linq;
using System.Threading.Tasks;
using WifloWatchBackend.Data; // DbContext'in bulunduğu namespace

namespace WifloWatchBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly WifloWatchDbContext _context;

        public MessageController(WifloWatchDbContext context)
        {
            _context = context;
        }

        // Mesaj gönderme
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] Message message)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    // Hata mesajlarını konsola yazdırıyoruz
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return BadRequest(ModelState);
            }

            if (message == null)
            {
                return BadRequest("Mesaj içeriği geçersiz.");
            }

            message.SentAt = DateTime.UtcNow;
            message.IsRead = false;

            try
            {
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(SendMessage), new { id = message.Id }, message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Mesaj kaydedilirken bir hata oluştu: {ex.Message}");
            }
        }



        // İki kullanıcı arasındaki mesajları getir
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

                return Ok(messages); // Mesajları başarılı bir şekilde döndür
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message }); // Hata durumunda mesajı döndür
            }
        }
    }
}
