using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WifloWatchBackend.Models
{
    public class Message
    {
        [Key]  // Bu özellik, Message sınıfının birincil anahtarını belirtir
        public int Id { get; set; }

        [Required]  // Gönderen kullanıcı ID'si zorunlu
        public int SenderId { get; set; }

        [Required]  // Alıcı kullanıcı ID'si zorunlu
        public int ReceiverId { get; set; }

        [Required]  // Mesaj metni zorunlu
        [StringLength(1000)]  // Mesajın maksimum uzunluğu 1000 karakter olmalı
        public string MessageText { get; set; }

        [Required]  // Mesaj okundu mu zorunlu
        public bool IsRead { get; set; }

        [Required]  // Mesajın gönderilme zamanı zorunlu
        public DateTime SentAt { get; set; }

        // Gönderen kullanıcı ile ilişkilendirir
        [ForeignKey("SenderId")]  // SenderId'yi Sender ile ilişkilendirir
        public virtual User Sender { get; set; }

        // Alıcı kullanıcı ile ilişkilendirir
        [ForeignKey("ReceiverId")]  // ReceiverId'yi Receiver ile ilişkilendirir
        public virtual User Receiver { get; set; }
    }
}
