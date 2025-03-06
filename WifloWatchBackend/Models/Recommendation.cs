using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WifloWatchBackend.Models
{
    public class Recommendation
    {
        [Key]  // Bu özellik, Recommendation sınıfının birincil anahtarını belirtir
        public int Id { get; set; }

        [Required]  // Kullanıcı ID'si zorunlu
        public int UserId { get; set; }  // Öneriyi yapan kullanıcı

        [Required]  // Film ID'si zorunlu
        public int MovieId { get; set; }  // Önerilen film

        [Range(0, 10)]  // Rating 0 ile 10 arasında olmalı
        public double Rating { get; set; }  // Öneri puanı

        // İlişkiler
        [ForeignKey("UserId")]  // UserId'yi User ile ilişkilendirir
        public virtual User User { get; set; }  // Öneriyi yapan kullanıcı

        [ForeignKey("MovieId")]  // MovieId'yi Movie ile ilişkilendirir
        public virtual Movie Movie { get; set; }  // Önerilen film
    }
}
