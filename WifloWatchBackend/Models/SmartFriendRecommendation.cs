using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WifloWatchBackend.Models
{
    public class SmartFriendRecommendation
    {
        [Key]  // Bu özellik, SmartFriendRecommendation sınıfının birincil anahtarını belirtir
        public int Id { get; set; }

        [Required]  // Öneriyi alan kullanıcı zorunlu
        public int UserId { get; set; }  // Öneriyi alan kullanıcı

        [Required]  // Önerilen kullanıcı zorunlu
        public int RecommendedUserId { get; set; }  // Önerilen kullanıcı

        [MaxLength(1000)]   // Öneri nedeni için maksimum 1000 karakter
        public string Reason { get; set; }  // Öneri nedeni (film/dizi tercihleri vb.)

        [Required]  // Öneri tarihi zorunlu
        public DateTime CreatedAt { get; set; }  // Öneri tarihi

        // İlişkiler
        [ForeignKey("UserId")]  // UserId'yi User ile ilişkilendirir
        public virtual User User { get; set; }  // Öneriyi alan kullanıcı

        [ForeignKey("RecommendedUserId")]  // RecommendedUserId'yi User ile ilişkilendirir
        public virtual User RecommendedUser { get; set; }  // Önerilen kullanıcı
    }
}
