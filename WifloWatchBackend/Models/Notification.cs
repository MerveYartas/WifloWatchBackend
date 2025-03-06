using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WifloWatchBackend.Models
{
    public class Notification
    {
        [Key]  // Bu özellik, Notification sınıfının birincil anahtarını belirtir
        public int Id { get; set; }

        [Required]  // Kullanıcı ID'si zorunlu
        public int UserId { get; set; }  // Bildirimi alan kullanıcı

        [Required]  // Mesaj zorunlu
        [StringLength(1000)]  // Mesaj maksimum 1000 karakter olmalı
        public string Message { get; set; }  // Bildirimin mesajı

        [Required]  // Bildirim tarihi zorunlu
        public DateTime CreatedAt { get; set; }  // Bildirim tarihi

        // Bildirimi alan kullanıcı ile ilişkilendirir
        [ForeignKey("UserId")]  // UserId'yi User ile ilişkilendirir
        public virtual User User { get; set; }  // Bildirimi alan kullanıcı
    }
}
