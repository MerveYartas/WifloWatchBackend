using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WifloWatchBackend.Models; // Message sınıfının olduğu namespace
using System.Threading.Tasks;
using System.Linq;
using WifloWatchBackend.Data;

namespace WifloWatchBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly WifloWatchDbContext _context; // DbContext'ini kendi ismine göre değiştir

        public MessageController(WifloWatchDbContext context)
        {
            _context = context;
        }

        // Mesaj gönderme
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] Message message)
        {
            if (message == null)
            {
                return BadRequest("Mesaj içeriği geçersiz.");
            }

            // Mesajın gönderilme zamanı ve okundu durumu ayarlanır
            message.SentAt = DateTime.UtcNow;
            message.IsRead = false;

            try
            {
                // Mesaj veritabanına eklenir
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                // Başarılıysa mesajı geri döneriz
                return Ok(message);
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

                return Ok(messages); // Mesajları başarılı bir şekilde döndür
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message }); // Hata durumunda mesajı döndür
            }
        }
    }
}
